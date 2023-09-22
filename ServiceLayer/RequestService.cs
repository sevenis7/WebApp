using DataLayer.Entities;
using DataLayer.Repositories;
using ServiceLayer.Interfaces;

namespace ServiceLayer
{
    public class RequestService : IRequestService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IUserRepository _userRepository;

        public RequestService(IRequestRepository requestRepository, 
            IUserRepository userRepository)
        {
            _requestRepository = requestRepository;
            _userRepository = userRepository;
        }

        public Request? Post(Request request, Guid userId)
        {
            var existingUserById = _userRepository.GetById(userId);

            if (existingUserById == null) return null;

            request.Status = RequestStatus.Receieved;
            request.User = existingUserById;
            request.DateTime = DateTime.UtcNow;

            _requestRepository.Add(request);

            return request;
        }

        public Request? ChangeStatus(int id, RequestStatus status)
        {
            var existingRequestByid = _requestRepository.GetById(id);

            if (existingRequestByid == null) return null;

            existingRequestByid.Status = status;
            _requestRepository.Update(existingRequestByid);

            return existingRequestByid;
        }

        public IQueryable<Request> GetByStatus(RequestStatus status)
        {
            return _requestRepository.GetWithStatus(status);
        }

        public IQueryable<Request> All()
        {
            return _requestRepository.GetAll();
        }

        public IQueryable<Request> GetByTime(DateTime from, DateTime to)
        {
            return _requestRepository.GetByTime(from, to);
        }
    }
}
