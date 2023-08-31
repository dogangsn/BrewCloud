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
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Agenda.Commands
{

    public class CreateAgendaCommand : IRequest<Response<bool>>
    {
        public AgendaDto CreateAgenda { get; set; }
    }

    public class CreateAgendaHandler : IRequestHandler<CreateAgendaCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAgendaHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetAgenda> _agendaRepository;
        private readonly IRepository<Vet.Domain.Entities.VetAgendaTags> _agendaTagsRepository;

        public CreateAgendaHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateAgendaHandler> logger, IRepository<Domain.Entities.VetAgenda> agendaRepository, IRepository<Domain.Entities.VetAgendaTags> agendaTagsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _agendaRepository = agendaRepository ?? throw new ArgumentNullException(nameof(agendaRepository));
            _agendaTagsRepository = agendaTagsRepository ?? throw new ArgumentNullException(nameof(agendaTagsRepository));
        }

        public async Task<Response<bool>> Handle(CreateAgendaCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {



                Vet.Domain.Entities.VetAgenda agenda = new()
                {
                    Id = Guid.NewGuid(),
                    AgendaTitle = request.CreateAgenda.AgendaTitle,
                    AgendaType = request.CreateAgenda.AgendaType,
                    AgendaNo = request.CreateAgenda.AgendaNo,
                    IsActive = request.CreateAgenda.IsActive,
                    Priority = request.CreateAgenda.Priority,
                    DueDate = request.CreateAgenda.DueDate,
                    Notes = request.CreateAgenda.Notes,
                    //AgendaTags = request.CreateAgenda.AgendaTags.Select(dto => new VetAgendaTags{
                    //    AgendaId=dto.Id,
                    //    Tags = dto.Tags
                        
                    //}).ToList(),


                    Deleted = false,
                    CreateDate = DateTime.UtcNow,

                };
                foreach (var item in request.CreateAgenda.AgendaTags)
                {
                    Vet.Domain.Entities.VetAgendaTags agendatags = new()
                    {
                        Id = Guid.NewGuid(),
                        AgendaId = agenda.Id,
                        Tags = item.Tags,
                        Deleted = false,
                        CreateDate = DateTime.UtcNow,

                    };
                    await _agendaTagsRepository.AddAsync(agendatags);
                }

                await _agendaRepository.AddAsync(agenda);
                await _uow.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {

            }
            return response;

        }
    }
}
