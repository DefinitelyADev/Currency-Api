using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CurrencyApi.Application.Helpers;
using CurrencyApi.Application.Interfaces.Data;
using CurrencyApi.Application.Interfaces.Services;
using CurrencyApi.Application.Requests.Currency;
using CurrencyApi.Application.Results;
using CurrencyApi.Domain.Entities;

namespace CurrencyApi.Infrastructure.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CurrencyService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public PagedResult<Currency> Get(GetCurrencyRequest request) => _unitOfWork.CurrencyRepository.Find(GetExpressionFromRequest(request));

        public async Task<PagedResult<Currency>> GetAsync(GetCurrencyRequest request) => await _unitOfWork.CurrencyRepository.FindAsync(GetExpressionFromRequest(request));

        public Currency? GetById(int id) => _unitOfWork.CurrencyRepository.GetById(id);

        public async Task<Currency?> GetByIdAsync(int id) => await _unitOfWork.CurrencyRepository.GetByIdAsync(id);

        public CreateCurrencyResult Create(CreateCurrencyRequest request)
        {
            ValidationResult validationResult = ValidateCreationRequest(request);

            if (validationResult.HasErrors)
                return new CreateCurrencyResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            Currency newCurrency = new(request.Name, request.AlphabeticCode, request.NumericCode, request.DecimalDigits);

            Currency createdCurrency = _unitOfWork.CurrencyRepository.Add(newCurrency);

            return new CreateCurrencyResult {Data = createdCurrency};
        }

        public async Task<CreateCurrencyResult> CreateAsync(CreateCurrencyRequest request)
        {
            ValidationResult validationResult = ValidateCreationRequest(request);

            if (validationResult.HasErrors)
                return new CreateCurrencyResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            Currency newCurrency = new(request.Name, request.AlphabeticCode, request.NumericCode, request.DecimalDigits);

            Currency createdCurrency = await _unitOfWork.CurrencyRepository.AddAsync(newCurrency);

            return new CreateCurrencyResult {Data = createdCurrency};
        }

        public UpdateCurrencyResult Update(UpdateCurrencyRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<UpdateCurrencyResult> UpdateAsync(UpdateCurrencyRequest request)
        {
            throw new System.NotImplementedException();
        }

        public DeleteCurrencyResult Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<DeleteCurrencyResult> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        #region Helpers

        private Expression<Func<Currency, bool>> GetExpressionFromRequest(GetCurrencyRequest request)
        {
            Expression<Func<Currency, bool>> expression = currency => (ObjectHelper.IsNull(request.Name) || currency.Name.Contains(request.Name))
                                                                  && (ObjectHelper.IsNull(request.AlphabeticCode) || currency.AlphabeticCode.Contains(request.AlphabeticCode))
                                                                  && (ObjectHelper.IsNull(request.DecimalDigits) || currency.DecimalDigits == request.DecimalDigits)
                                                                  && (ObjectHelper.IsNull(request.NumericCode) || currency.NumericCode == request.NumericCode);
            return expression;
        }

        private ValidationResult ValidateCreationRequest(CreateCurrencyRequest request)
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
