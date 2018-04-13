using FluentValidation.Results;
using Newtonsoft.Json;

namespace CoolBlueTask.Api.Core
{
	public class InvalidField
	{
		[JsonProperty("property_name")]
		public string PropertyName { get; set; }

		[JsonProperty("error_code")]
		public string ErrorCode { get; set; }

		[JsonProperty("error_message")]
		public string ErrorMessage { get; set; }

		//public string ValidationErrorSource { get; set; }

		[JsonConstructor]
		public InvalidField() {}

		public InvalidField(
			ValidationFailure error)
		{
			ErrorMessage = error.ErrorMessage;
			ErrorCode = error.ErrorCode;
			PropertyName = error.PropertyName;
		}
	}
}