using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Application.Models.Definition.PrintTemplate;
using VetSystems.Vet.Domain.Entities;
using VetSystems.Vet.Domain.Entities;
using VetSystems.Shared.Service;
using VetSystems.Vet.Domain.Contracts;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace VetSystems.Vet.Application.Features.Definition.PrintTemplate.Queries
{
    public class GetPrintTemplateFilterByTypeQuery : IRequest<Response<List<PrintTemplateListDto>>>
    {
        public PrintType Type { get; set; }
    }

    public class GetPrintTemplateFilterByTypeQueryHandler : IRequestHandler<GetPrintTemplateFilterByTypeQuery, Response<List<PrintTemplateListDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPrintTemplateFilterByTypeQueryHandler> _logger;
        private readonly IRepository<VetPrintTemplate> _vetPrintTemplateRepository;

        public GetPrintTemplateFilterByTypeQueryHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<GetPrintTemplateFilterByTypeQueryHandler> logger, IRepository<VetPrintTemplate> vetPrintTemplateRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetPrintTemplateRepository = vetPrintTemplateRepository;
        }

        public async Task<Response<List<PrintTemplateListDto>>> Handle(GetPrintTemplateFilterByTypeQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<PrintTemplateListDto>>();
            try
            {
                List<VetPrintTemplate> _list = (await _vetPrintTemplateRepository.GetAsync(x => x.Deleted == false && x.Type == request.Type)).OrderBy(x => x.Type).ToList();
                var result = _mapper.Map<List<PrintTemplateListDto>>(_list);
                response.Data = result;
            }
            catch (Exception ex)
            {
                return Response<List<PrintTemplateListDto>>.Fail(ex.Message, 404);
            }
            return response;


        }
    }
}
