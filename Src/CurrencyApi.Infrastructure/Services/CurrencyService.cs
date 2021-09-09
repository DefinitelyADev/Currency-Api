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

        public PagedResult<Currency> Get(GetCurrencyRequest request) => _unitOfWork.Currencies.Find(GetExpressionFromRequest(request));

        public async Task<PagedResult<Currency>> GetAsync(GetCurrencyRequest request) => await _unitOfWork.Currencies.FindAsync(GetExpressionFromRequest(request));

        public Currency? GetById(int id) => _unitOfWork.Currencies.GetById(id);

        public async Task<Currency?> GetByIdAsync(int id) => await _unitOfWork.Currencies.GetByIdAsync(id);

        public CreateCurrencyResult Create(CreateCurrencyRequest request)
        {
            ValidationResult validationResult = ValidateRequest(request);

            if (validationResult.HasErrors)
                return new CreateCurrencyResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            Currency newCurrency = new(request.Name, request.AlphabeticCode, request.NumericCode, request.DecimalDigits);

            Currency createdCurrency = _unitOfWork.Currencies.Add(newCurrency);

            _unitOfWork.Commit();

            return new CreateCurrencyResult {Data = createdCurrency};
        }

        public async Task<CreateCurrencyResult> CreateAsync(CreateCurrencyRequest request)
        {
            ValidationResult validationResult = ValidateRequest(request);

            if (validationResult.HasErrors)
                return new CreateCurrencyResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            Currency newCurrency = new(request.Name, request.AlphabeticCode, request.NumericCode, request.DecimalDigits);

            Currency createdCurrency = await _unitOfWork.Currencies.AddAsync(newCurrency);

            await _unitOfWork.CommitAsync();

            return new CreateCurrencyResult {Data = createdCurrency};
        }

        public UpdateCurrencyResult Update(UpdateCurrencyRequest request)
        {
            ValidationResult validationResult = ValidateRequest(request);

            if (validationResult.HasErrors)
                return new UpdateCurrencyResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            Currency currencyToUpdate = new(request.Id, request.Name, request.AlphabeticCode, request.NumericCode, request.DecimalDigits);

            Currency createdCurrency = _unitOfWork.Currencies.Update(currencyToUpdate);

            _unitOfWork.Commit();

            return new UpdateCurrencyResult {Data = createdCurrency};
        }

        public async Task<UpdateCurrencyResult> UpdateAsync(UpdateCurrencyRequest request)
        {
            ValidationResult validationResult = ValidateRequest(request);

            if (validationResult.HasErrors)
                return new UpdateCurrencyResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            Currency currencyToUpdate = new(request.Id, request.Name, request.AlphabeticCode, request.NumericCode, request.DecimalDigits);

            Currency createdCurrency = await _unitOfWork.Currencies.UpdateAsync(currencyToUpdate);

            await _unitOfWork.CommitAsync();

            return new UpdateCurrencyResult {Data = createdCurrency};
        }

        public DeleteCurrencyResult Delete(int id)
        {
            ValidationResult validationResult = ValidateDeleteRequest(id);

            if (validationResult.HasErrors)
                return new DeleteCurrencyResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            bool result = _unitOfWork.Currencies.Remove(id);

            _unitOfWork.Commit();

            return new DeleteCurrencyResult {Succeeded = result};
        }

        public async Task<DeleteCurrencyResult> DeleteAsync(int id)
        {
            ValidationResult validationResult = ValidateDeleteRequest(id);

            if (validationResult.HasErrors)
                return new DeleteCurrencyResult {Errors = validationResult.Errors!.ToList(), Succeeded = false};

            bool result = await _unitOfWork.Currencies.RemoveAsync(id);

            await _unitOfWork.CommitAsync();

            return new DeleteCurrencyResult {Succeeded = result};
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

        private ValidationResult ValidateDeleteRequest(int id)
        {
            ValidationResult result = new();

            if (id < 0)
                result.AddError("Decimal digits cannot be less than 0.");

            return result;
        }

        #endregion
    }
}
