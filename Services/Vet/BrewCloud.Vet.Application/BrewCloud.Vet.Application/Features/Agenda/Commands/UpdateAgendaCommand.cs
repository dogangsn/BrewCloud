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
    public class UpdateAgendaCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public int? AgendaNo { get; set; }
        public int? AgendaType { get; set; }
        public int? IsActive { get; set; }
        public string AgendaTitle { get; set; } = string.Empty;
        public int? Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public virtual List<AgendaTagsDto> AgendaTags { get; set; }
    }

    public class UpdateAgendaCommandHandler : IRequestHandler<UpdateAgendaCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateAgendaCommandHandler> _logger;
        private readonly IRepository<Domain.Entities.VetAgenda> _agendaRepository;
        private readonly IRepository<Domain.Entities.VetAgendaTags> _agendaTagsRepository;

        public UpdateAgendaCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateAgendaCommandHandler> logger, IRepository<Domain.Entities.VetAgenda> agendaRepository, IRepository<Domain.Entities.VetAgendaTags> agendaTagsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _agendaRepository = agendaRepository ?? throw new ArgumentNullException(nameof(agendaRepository));
            _agendaTagsRepository = agendaTagsRepository ?? throw new ArgumentNullException(nameof(agendaTagsRepository));
        }

        public async Task<Response<bool>> Handle(UpdateAgendaCommand request, CancellationToken cancellationToken)
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
                var agendaTagsAllList = await _agendaTagsRepository.GetAllAsync();
                var agendaTags = agendaTagsAllList.Where(x => x.AgendaId == agenda.Id).ToList();
                if (agenda == null)
                {
                    _logger.LogWarning($"Casing update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Property update failed", 404);
                }

                agenda.AgendaNo = request.AgendaNo;
                agenda.AgendaType = request.AgendaType;
                agenda.IsActive = request.IsActive;
                agenda.AgendaTitle = request.AgendaTitle;
                agenda.Priority = request.Priority;
                agenda.DueDate = request.DueDate;
                agenda.Notes = request.Notes;
                agenda.UpdateDate = DateTime.Now;
                VetAgendaTags vetAgendaTags = new VetAgendaTags();
                List<VetAgendaTags> listvetAgendaTags = new List<VetAgendaTags>();
                foreach (var tag in request.AgendaTags)
                {
                    vetAgendaTags.AgendaId = tag.Id;
                    vetAgendaTags.Tags = tag.Tags;
                    vetAgendaTags.UpdateDate = DateTime.Now;
                    listvetAgendaTags.Add(vetAgendaTags);
                }
                agendaTags = listvetAgendaTags;


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
