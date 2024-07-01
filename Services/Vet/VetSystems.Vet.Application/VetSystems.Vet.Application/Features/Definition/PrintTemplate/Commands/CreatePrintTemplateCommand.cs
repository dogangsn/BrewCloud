using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Domain.Entities;
using VetSystems.Shared.Service;
using VetSystems.Vet.Domain.Contracts;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace VetSystems.Vet.Application.Features.Definition.PrintTemplate.Commands
{
    public class CreatePrintTemplateCommand : IRequest<Response<bool>>
    {
        public string TemplateName { get; set; } = string.Empty;
        public PrintType Type { get; set; }
        public string HtmlContent { get; set; } = string.Empty;
    }

    public class CreatePrintTemplateCommandHandler : IRequestHandler<CreatePrintTemplateCommand, Response<bool>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePrintTemplateCommandHandler> _logger;
        private readonly IRepository<VetPrintTemplate> _vetPrintTemplateRepository;

        public CreatePrintTemplateCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreatePrintTemplateCommandHandler> logger, IRepository<VetPrintTemplate> vetPrintTemplateRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetPrintTemplateRepository = vetPrintTemplateRepository;
        }

        public async Task<Response<bool>> Handle(CreatePrintTemplateCommand request, CancellationToken cancellationToken)
        {

            var response = Response<bool>.Success(200);
            try
            {




            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
