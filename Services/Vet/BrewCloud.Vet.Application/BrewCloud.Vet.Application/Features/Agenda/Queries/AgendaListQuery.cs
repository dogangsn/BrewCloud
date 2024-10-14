using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Agenda;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Agenda.Queries
{
    //internal class AgendaListQuery
    //{
    //}
    public class AgendaListQuery : IRequest<Response<List<AgendaDto>>>
    {
    }

    public class AgendaListQueryHandler : IRequestHandler<AgendaListQuery, Response<List<AgendaDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AgendaListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<Response<List<AgendaDto>>> Handle(AgendaListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<AgendaDto>>();
            try
            {
                string query = "Select * from vetAgenda where Deleted = 0 order by agendano asc";
                var _data = _uow.Query<AgendaDto>(query).ToList();
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
