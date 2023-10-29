using DataLayer.Entities;
using ServiceLayer.Interfaces;
using DataLayer.Interfaces;

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

        public async Task<Request?> Post(Request request, Guid userId)
        {
            var existingUserById = await _userRepository.GetById(userId)!;

            if (existingUserById == null || request == null) return null;

            request.Status = RequestStatus.Received;
            request.User = existingUserById;
            request.Date = DateTime.UtcNow;

            await _requestRepository.Add(request);

            return request;
        }

        public async Task<Request?> Edit(int id, Request request)
        {
            var existingRequestByid = await _requestRepository.GetById(id);

            if (existingRequestByid == null) return null;

            existingRequestByid.Status = request.Status;

            await _requestRepository.Update(existingRequestByid);

            return existingRequestByid;
        }

        public async Task<IEnumerable<Request?>> All()
        {
            var requests = await _requestRepository.GetAll();

            return requests;
        }

        public async Task<Request?> Get(int id)
        {
            return await _requestRepository.GetById(id);
        }

    }
}
