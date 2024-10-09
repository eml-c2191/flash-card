using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FlashCard.API.Models.Validations
{
    public class ValidationError
    {
        public string Field { get; }
        public string Message { get; }

        public ValidationError(string field, string message)
        {
            Field = field;
            Message = message;
        }
    }
    public class ValidationResultModel
    {
        public string Message { get; }
        public IEnumerable<ValidationError> Errors { get; }

        public ValidationResultModel(ModelStateDictionary modelState)
        {
            Errors = modelState?.Keys.SelectMany(selector: key
                        => modelState.GetValueOrDefault(key)?
                            .Errors.Select(x => new ValidationError(key, x.ErrorMessage))
                            ?? Enumerable.Empty<ValidationError>())
                    ?? Enumerable.Empty<ValidationError>();

            Message = Errors.Any() ?
                Errors.First().Message
                : "Validation Request Input Failed";
        }
        public ValidationResultModel(string message, IDictionary<string, string> errors)
        {
            Message = message;

            Errors = errors?.Select(x => new ValidationError(x.Key, x.Value))
                            ?? Enumerable.Empty<ValidationError>();
        }
    }
}
