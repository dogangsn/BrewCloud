using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Patient.Commands
{
    public class DeleteExaminationCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteExaminationCommandHandler : IRequestHandler<DeleteExaminationCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteExaminationCommandHandler> _logger; 
        private readonly IIdentityRepository _identityRepository;

        private readonly IRepository<Vet.Domain.Entities.VetExamination> _vetExaminationRepository;

        public DeleteExaminationCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteExaminationCommandHandler> logger,
           IIdentityRepository identityRepository, IRepository<Domain.Entities.VetExamination> vetExaminationRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _vetExaminationRepository = vetExaminationRepository;
        }

        public async Task<Response<bool>> Handle(DeleteExaminationCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var examination = await _vetExaminationRepository.GetByIdAsync(request.Id);
                if (examination == null)
                {
                    _logger.LogWarning($"Examination update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Examination update failed", 404);
                }
                examination.Deleted = true;
                examination.DeletedDate = DateTime.Now;
                examination.DeletedUsers = _identityRepository.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;

            }

            return response;

        }
    }


}
