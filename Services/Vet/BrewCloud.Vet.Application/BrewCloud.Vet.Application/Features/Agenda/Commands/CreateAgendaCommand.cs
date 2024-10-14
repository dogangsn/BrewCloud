using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Agenda;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Agenda.Commands
{

    public class CreateAgendaCommand : IRequest<Response<bool>>
    {
        public int? AgendaNo { get; set; }
        public int? AgendaType { get; set; }
        public int? IsActive { get; set; }
        public string AgendaTitle { get; set; } = string.Empty;
        public int? Priority { get; set; } 
        public DateTime? DueDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public virtual List<AgendaTagsDto> AgendaTags { get; set; }
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
                    AgendaTitle = request.AgendaTitle,
                    AgendaType = request.AgendaType,
                    AgendaNo = request.AgendaNo,
                    IsActive = request.IsActive,
                    Priority = request.Priority,
                    DueDate = request.DueDate,
                    Notes = request.Notes,
                    //AgendaTags = request.CreateAgenda.AgendaTags.Select(dto => new VetAgendaTags{
                    //    AgendaId=dto.Id,
                    //    Tags = dto.Tags
                        
                    //}).ToList(),


                    Deleted = false,
                    CreateDate = DateTime.UtcNow,

                };
                foreach (var item in request.AgendaTags)
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
