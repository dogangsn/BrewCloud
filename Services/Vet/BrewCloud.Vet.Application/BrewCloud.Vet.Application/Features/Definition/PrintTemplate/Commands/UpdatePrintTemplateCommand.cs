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
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace BrewCloud.Vet.Application.Features.Definition.PrintTemplate.Commands
{
    public class UpdatePrintTemplateCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string TemplateName { get; set; } = string.Empty;
        public PrintType Type { get; set; }
        public string HtmlContent { get; set; } = string.Empty;
    }

    public class UpdatePrintTemplateCommandHandler : IRequestHandler<UpdatePrintTemplateCommand, Response<bool>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePrintTemplateCommandHandler> _logger;
        private readonly IRepository<VetPrintTemplate> _vetPrintTemplateRepository;

        public UpdatePrintTemplateCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdatePrintTemplateCommandHandler> logger, IRepository<VetPrintTemplate> vetPrintTemplateRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetPrintTemplateRepository = vetPrintTemplateRepository;
        }

        public async Task<Response<bool>> Handle(UpdatePrintTemplateCommand request, CancellationToken cancellationToken)
        {

            var response = Response<bool>.Success(200);
            try
            {
                var printTemplate = await _vetPrintTemplateRepository.GetByIdAsync(request.Id);
                if (printTemplate == null)
                {
                    _logger.LogWarning($"printTemplate deleted failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Çıktı Şablonu Bulunamadı", 404);
                } 
                printTemplate.UpdateDate = DateTime.Now;
                printTemplate.UpdateUsers = _identity.Account.UserName;
                printTemplate.Type = request.Type;
                printTemplate.TemplateName = request.TemplateName;
                printTemplate.HtmlContent = request.HtmlContent;

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
