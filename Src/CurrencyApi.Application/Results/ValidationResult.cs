using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CurrencyApi.Application.Results
{
    public class ValidationResult
    {
        private List<string>? errors;
        public ReadOnlyCollection<string>? Errors => errors?.AsReadOnly();

        public bool HasErrors;

        public void AddError(string message)
        {
            errors ??= new List<string>();
            errors.Add(message);
            HasErrors = true;
        }
    }
}
