using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Vet.Domain.Entities;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Domain.Contracts;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace BrewCloud.Vet.Application.Features.Definition.PrintTemplate.Commands
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
                VetPrintTemplate vetPrintTemplate = new()
                {
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName,
                    HtmlContent = request.HtmlContent,
                    Type = request.Type,
                    TemplateName = request.TemplateName,
                    Id = Guid.NewGuid(),
                };
                await _vetPrintTemplateRepository.AddAsync(vetPrintTemplate);
                await _uow.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
