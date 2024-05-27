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
using VetSystems.Vet.Application.Features.Demands.Demand.Commands;
using VetSystems.Vet.Application.Models.Agenda;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Agenda.Queries
{

    public class AgendaListByIdQuery : IRequest<Response<List<AgendaDto>>>
    {
        public Guid Id { get; set; }
        //public int? AgendaNo { get; set; }
        //public int? AgendaType { get; set; }
        //public int? IsActive { get; set; }
        //public string AgendaTitle { get; set; } = string.Empty;
        //public int? Priority { get; set; }
        //public DateTime? DueDate { get; set; }
        //public string Notes { get; set; } = string.Empty;
        //public virtual List<AgendaTagsDto> AgendaTags { get; set; }
    }

    public class AgendaListByIdQueryHandler : IRequestHandler<AgendaListByIdQuery, Response<List<AgendaDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

      


        public AgendaListByIdQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Response<List<AgendaDto>>> Handle(AgendaListByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<AgendaDto>>();
            try
            {


                string query = "Select * from vetAgenda where id = @id and Deleted = 0";
                var _data = _uow.Query<AgendaDto>(query, new { id = request.Id }).ToList();
                string tagsQuery = "Select * from vetAgendaTags where Deleted = 0";
                var _datatags = _uow.Query<AgendaTagsDto>(tagsQuery).ToList();
                foreach (var item in _data)
                {
                    item.AgendaTags = _datatags.Where(x => x.AgendaId == item.id).ToList();
                }
                response = new Response<List<AgendaDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                //response.Errors = ex.ToString();
            }

            return response;


        }
    }
}
