using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CurrencyApi.Application.Exceptions;
using CurrencyApi.Application.Interfaces.Data;
using CurrencyApi.Application.Interfaces.Services;
using CurrencyApi.Application.Requests.Authentication;
using CurrencyApi.Application.Results;
using CurrencyApi.Application.Settings;
using CurrencyApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace CurrencyApi.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly JwtConfiguration _jwtSettings;

        public AuthenticationService(IUnitOfWork unitOfWork, UserManager<User> userManager, TokenValidationParameters tokenValidationParameters, JwtConfiguration jwtSettings)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _tokenValidationParameters = tokenValidationParameters;
            _jwtSettings = jwtSettings;
        }

        public AuthenticationResult Login(LoginRequest request)
        {
            User? user = _userManager.FindByNameAsync(request.Username).Result;

            if (user == null)
            {
                return new AuthenticationResult()
                {
                    Errors = new List<string>() { "Username or password is incorrect." } // Prevent from checking if a user exists by returning the same message
                };
            }

            bool userHasValidPassword = _userManager.CheckPasswordAsync(user, request.Password).Result;

            if (userHasValidPassword == false)
            {
                return new AuthenticationResult
                {
                    Errors = new List<string> {"Username or password is incorrect."}
                };
            }

            //Generate new access token with user credentials
            return GenerateJwtAccessToken(user);
        }

        public async Task<AuthenticationResult> LoginAsync(LoginRequest request)
        {
            User? user = await _userManager.FindByNameAsync(request.Username);

            if (user == null)
            {
                return new AuthenticationResult()
                {
                    Errors = new List<string>() { "Username or password is incorrect." } // Prevent from checking if a user exists by returning the same message
                };
            }

            bool userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (userHasValidPassword == false)
            {
                return new AuthenticationResult
                {
                    Errors = new List<string> {"Username or password is incorrect."}
                };
            }

            //Generate new access token with user credentials
            return await GenerateJwtAccessTokenAsync(user);
        }

        public AuthenticationResult RefreshToken(RefreshTokenRequest request)
        {
            //Manually validate access token
            ClaimsPrincipal? validatedToken = GetPrincipalFromToken(request.AccessToken);

            if (validatedToken == null)
            {
                return new AuthenticationResult()
                {
                    Errors = new List<string> { "Invalid access token" }
                };
            }

            //Check if the access token has already expired or not
            long dateEndUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            DateTime expirationDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(dateEndUnix);

            if (expirationDateUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult
                {
                    Errors = new List<string> { "The access token hasn't expired yet" }
                };
            }

            //Get the refresh token from the database
            RefreshToken storedRefreshToken = _unitOfWork.Tokens.Get(request.RefreshToken);

            //Check if refresh token has expired
            if (DateTime.UtcNow > storedRefreshToken.DateEnd) {
                return new AuthenticationResult
                {
                    Errors = new List<string> { "This refresh token has expired" }
                };
            }

            //Check if token has been manually invalidated
            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult
                {
                    Errors = new List<string> { "This refresh token has been invalidated" }
                };
            }

            //Check if token has been manually used
            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult
                {
                    Errors = new List<string> { "This refresh token has already been used" }
                };
            }

            //Validate if the access token and refresh token are the same pair
            string jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (!storedRefreshToken.JwtId.Equals(jti, StringComparison.Ordinal))
            {
                return new AuthenticationResult
                {
                    Errors = new List<string> { "This refresh token does not match this JWT" }
                };
            }

            //The refresh token is valid, update that it has been used
            storedRefreshToken.Used = true;

            _ = _unitOfWork.Tokens.Update(storedRefreshToken);
            _unitOfWork.Commit();

            //Generate new access token with user credentials
            User user = _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Sub).Value).Result;

            return GenerateJwtAccessToken(user);
        }

        public async Task<AuthenticationResult> RefreshTokenAsync(RefreshTokenRequest request)
        {
            //Manually validate access token
            ClaimsPrincipal? validatedToken = GetPrincipalFromToken(request.AccessToken);

            if (validatedToken == null)
            {
                return new AuthenticationResult()
                {
                    Errors = new List<string> { "Invalid access token" }
                };
            }

            //Check if the access token has already expired or not
            long dateEndUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            DateTime expirationDateUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(dateEndUnix);

            if (expirationDateUtc > DateTime.UtcNow)
            {
                return new AuthenticationResult
                {
                    Errors = new List<string> { "The access token hasn't expired yet" }
                };
            }

            //Get the refresh token from the database
            RefreshToken storedRefreshToken = await _unitOfWork.Tokens.GetAsync(request.RefreshToken);

            //Check if refresh token has expired
            if (DateTime.UtcNow > storedRefreshToken.DateEnd) {
                return new AuthenticationResult
                {
                    Errors = new List<string> { "This refresh token has expired" }
                };
            }

            //Check if token has been manually invalidated
            if (storedRefreshToken.Invalidated)
            {
                return new AuthenticationResult
                {
                    Errors = new List<string> { "This refresh token has been invalidated" }
                };
            }

            //Check if token has been manually used
            if (storedRefreshToken.Used)
            {
                return new AuthenticationResult
                {
                    Errors = new List<string> { "This refresh token has already been used" }
                };
            }

            //Validate if the access token and refresh token are the same pair
            string jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (!storedRefreshToken.JwtId.Equals(jti, StringComparison.Ordinal))
            {
                return new AuthenticationResult
                {
                    Errors = new List<string> { "This refresh token does not match this JWT" }
                };
            }

            //The refresh token is valid, update that it has been used
            storedRefreshToken.Used = true;

            _ = await _unitOfWork.Tokens.UpdateAsync(storedRefreshToken);
            await _unitOfWork.CommitAsync();

            //Generate new access token with user credentials
            User user = await _userManager.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Sub).Value);

            return await GenerateJwtAccessTokenAsync(user);
        }

        private AuthenticationResult GenerateJwtAccessToken(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            //Get secret key
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            //Signing credentials
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Information about the user
            Claim[] claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() )
            };

            //Generate access token
            JwtSecurityToken accessToken = new(
                _jwtSettings.Issuer,
                _jwtSettings.Issuer,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddSeconds(_jwtSettings.AccessTokenExpiration),
                credentials
            );

            //Generate refresh token
            RefreshToken newRefreshToken = new(accessToken.Id, user.Id, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpiration));

            //Save refresh token
            RefreshToken refreshToken = _unitOfWork.Tokens.Add(newRefreshToken);
            _unitOfWork.Commit();

            //Encode the access token and return
            return new AuthenticationResult()
            {
                Succeeded = true,
                AccessToken = tokenHandler.WriteToken(accessToken),
                RefreshToken = refreshToken.Token
            };
        }

        private async Task<AuthenticationResult> GenerateJwtAccessTokenAsync(User user)
        {
            JwtSecurityTokenHandler tokenHandler = new();

            //Get secret key
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            //Signing credentials
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Information about the user
            Claim[] claims = {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Name, user.UserName),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() )
            };

            //Generate access token
            JwtSecurityToken accessToken = new(
                _jwtSettings.Issuer,
                _jwtSettings.Issuer,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddSeconds(_jwtSettings.AccessTokenExpiration),
                credentials
            );

            //Generate refresh token
            RefreshToken newRefreshToken = new(accessToken.Id, user.Id, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(_jwtSettings.RefreshTokenExpiration));

            //Save refresh token
            RefreshToken refreshToken = await _unitOfWork.Tokens.AddAsync(newRefreshToken);
            await _unitOfWork.CommitAsync();

            //Encode the access token and return
            return new AuthenticationResult()
            {
                Succeeded = true,
                AccessToken = tokenHandler.WriteToken(accessToken),
                RefreshToken = refreshToken.Token
            };
        }

        private ClaimsPrincipal? GetPrincipalFromToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            //Override Microsoft defaults explained here https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/issues/415
            tokenHandler.InboundClaimFilter.Clear();
            tokenHandler.InboundClaimTypeMap.Clear();
            tokenHandler.OutboundClaimTypeMap.Clear();

            try
            {
                var tokenValidationParameters = _tokenValidationParameters.Clone();
                tokenValidationParameters.ValidateLifetime = false;
                var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out var validatedToken);
                return IsJwtWithValidSecurityAlgorithm(validatedToken) ? principal : null;
            }
            catch (Exception ex)
            {
                throw new WebAppException("Something went wrong while trying to validate the expired access token.", ex);
            }
        }

        private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken) => (validatedToken is JwtSecurityToken jwtSecurityToken) && jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);
    }
}
