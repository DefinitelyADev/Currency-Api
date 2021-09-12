using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CurrencyApi.Application.Helpers;
using CurrencyApi.Application.Interfaces.Data;
using CurrencyApi.Application.Interfaces.Services;
using CurrencyApi.Application.Requests.Currency;
using CurrencyApi.Application.Results;
using CurrencyApi.Application.Results.CurrencyResults;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Infrastructure.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrencyService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<PagedResult<Currency>> GetAsync(GetCurrencyRequest request) => await _unitOfWork.Currencies.FindAsync(request, GetExpressionFromRequest(request));

        public async Task<Currency> GetByIdAsync(int id) => await _unitOfWork.Currencies.GetByIdAsync(id);

        public async Task<CreateCurrencyResult> CreateAsync(CreateCurrencyRequest request)
        {
            ValidationResult validationResult = ValidateRequest(request);

            if (validationResult.HasErrors)
                return new CreateCurrencyResult { Errors = validationResult.Errors!.ToList(), Succeeded = false };

            Currency newCurrency = new(request.Name!, request.AlphabeticCode!, request.NumericCode, request.DecimalDigits);

            Currency createdCurrency = await _unitOfWork.Currencies.AddAsync(newCurrency);

            await _unitOfWork.CommitAsync();

            return new CreateCurrencyResult { Data = createdCurrency, Succeeded = true };
        }

        public async Task<UpdateCurrencyResult> UpdateAsync(int id, UpdateCurrencyRequest request)
        {
            ValidationResult validationResult = ValidateRequest(request);

            if (validationResult.HasErrors)
                return new UpdateCurrencyResult { Errors = validationResult.Errors!.ToList(), Succeeded = false };

            Currency currencyToUpdate = new(id, request.Name!, request.AlphabeticCode!, request.NumericCode, request.DecimalDigits);

            Currency updatedCurrency = await _unitOfWork.Currencies.UpdateAsync(currencyToUpdate);

            await _unitOfWork.CommitAsync();

            return new UpdateCurrencyResult { Data = updatedCurrency, Succeeded = true };
        }

        public async Task<DeleteCurrencyResult> DeleteAsync(int id)
        {
            bool result = await _unitOfWork.Currencies.RemoveAsync(id);

            await _unitOfWork.CommitAsync();

            return new DeleteCurrencyResult { Succeeded = result };
        }

        #region Helpers

        private Expression<Func<Currency, bool>> GetExpressionFromRequest(GetCurrencyRequest request)
        {
            Expression<Func<Currency, bool>> expression = currency => (ObjectHelper.IsNull(request.Name) || currency.Name.ToUpper().Contains(request.Name.ToUpper()))
                                                                  && (ObjectHelper.IsNull(request.AlphabeticCode) || currency.AlphabeticCode.ToUpper().Contains(request.AlphabeticCode.ToUpper()))
                                                                  && (ObjectHelper.IsNull(request.DecimalDigits) || currency.DecimalDigits == request.DecimalDigits)
                                                                  && (ObjectHelper.IsNull(request.NumericCode) || currency.NumericCode == request.NumericCode);
            return expression;
        }

        private ValidationResult ValidateRequest(CreateCurrencyRequest request)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(request.Name))
                result.AddError("Name cannot be empty.");

            if (string.IsNullOrWhiteSpace(request.AlphabeticCode))
                result.AddError("Alphabetic code cannot be empty.");

            if (request.DecimalDigits < 0)
                result.AddError("Decimal digits cannot be less than 0.");

            if (request.NumericCode < 0)
                result.AddError("Numeric code cannot be less than 0.");

            return result;
        }

        private ValidationResult ValidateRequest(UpdateCurrencyRequest request)
        {
            ValidationResult result = new();

            if (string.IsNullOrWhiteSpace(request.Name))
                result.AddError("Name cannot be empty.");

            if (string.IsNullOrWhiteSpace(request.AlphabeticCode))
                result.AddError("Alphabetic code cannot be empty.");

            if (request.DecimalDigits < 0)
                result.AddError("Decimal digits cannot be less than 0.");

            if (request.NumericCode < 0)
                result.AddError("Numeric code cannot be less than 0.");

            return result;
        }

        #endregion
    }
}
