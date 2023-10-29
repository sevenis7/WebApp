using System.ComponentModel.DataAnnotations;
using WebAppApi.Requests;

namespace WebAppApi.Attributes
{
    public class IntervalValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is IntervalRequest request)
                if (request.From < request.To) return true;
            
            ErrorMessage = "Wrong interval.";

            return false;
        }
    }
}
