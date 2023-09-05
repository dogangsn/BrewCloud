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
using VetSystems.Vet.Application.Models.Agenda;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Agenda.Commands
{
    public class DeleteAgendaCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteAgendaCommandHandler : IRequestHandler<DeleteAgendaCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteAgendaCommandHandler> _logger;
        private readonly IRepository<Domain.Entities.VetAgenda> _agendaRepository;
        private readonly IRepository<Domain.Entities.VetAgendaTags> _agendaTagsRepository;
        private readonly IIdentityRepository _identityRepository;

        public DeleteAgendaCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteAgendaCommandHandler> logger, IRepository<Domain.Entities.VetAgenda> agendaRepository, IRepository<Domain.Entities.VetAgendaTags> agendaTagsRepository, IIdentityRepository identityRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _agendaRepository = agendaRepository ?? throw new ArgumentNullException(nameof(agendaRepository));
            _agendaTagsRepository = agendaTagsRepository ?? throw new ArgumentNullException(nameof(agendaTagsRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
        }

        public async Task<Response<bool>> Handle(DeleteAgendaCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var agenda = await _agendaRepository.GetByIdAsync(request.Id);
                var agendaTagsAllList= await _agendaTagsRepository.GetAllAsync();
                var agendaTags = agendaTagsAllList.Where(x => x.AgendaId == agenda.Id).ToList();
                if (agenda == null)
                {
                    _logger.LogWarning($"Agenda delete failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                agenda.Deleted = true;
                agenda.DeletedDate = DateTime.Now;
                agenda.DeletedUsers = _identityRepository.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);
                try
                {
                    if (agendaTags != null)
                    {

                        if ((int)agendaTags.Count > 0)
                        {
                          foreach (var item in agendaTags)
                          {
                          
                              item.Deleted = true;
                              item.DeletedDate = DateTime.Now;
                              item.DeletedUsers = _identityRepository.Account.UserName;
                          
                          }

                        }

                    }
                }
                catch (Exception)
                {

                    _logger.LogWarning($"AgendaTags delete failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }
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
