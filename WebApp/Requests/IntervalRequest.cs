using System.ComponentModel.DataAnnotations;
using WebAppApi.Attributes;

namespace WebAppApi.Requests
{
    [IntervalValidation]
    public class IntervalRequest
    {
        public DateTime From { get; set; }

        public DateTime To { get; set; }

    }
}
