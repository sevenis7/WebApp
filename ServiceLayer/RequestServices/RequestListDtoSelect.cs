using ServiceLayer.DTO;
using DataLayer.Entities;

namespace ServiceLayer.RequestServices
{
    public static class RequestListDtoSelect
    {
        public static IEnumerable<RequestDto>? MapRequestToDto(this IEnumerable<Request?>? requests)
        {
            if (requests == null) return null;
            return requests.Select(x => new RequestDto
            {
                RequestId = x.RequestId,
                Text = x.Text,
                Status = x.Status.ToString(),
                Date = x.Date,
                Username = x.User.FullName
            });
        }

        public static RequestDto? MapRequestToDto(this Request? request)
        {
            if (request == null) return null;
            return new RequestDto
            {
                RequestId = request.RequestId,
                Text = request.Text,
                Status = request.Status.ToString(),
                Date = request.Date,
                Username = request.User.FullName
            };
        }
    }
}
