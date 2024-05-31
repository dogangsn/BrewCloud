using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using VetSystems.Shared.Service;

namespace VetSystems.Vet.Application.Features.Definition.SmsTemplate.Commands
{
    public class UpdateSmsTemplateCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public string TemplateName { get; set; } = string.Empty;
        public string TemplateContent { get; set; } = string.Empty;
        public bool? EnableSMS { get; set; }
        public bool? EnableAppNotification { get; set; }
        public bool? EnableEmail { get; set; }
        public bool? EnableWhatsapp { get; set; }
    }

    public class UpdateSmsTemplateCommandHandler : IRequestHandler<UpdateSmsTemplateCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateSmsTemplateCommandHandler> _logger;
        private readonly IRepository<VetSmsTemplate> _vetSmsTemplateRepository;

        public UpdateSmsTemplateCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateSmsTemplateCommandHandler> logger, IRepository<VetSmsTemplate> vetSmsTemplateRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetSmsTemplateRepository = vetSmsTemplateRepository;
        }

        public async Task<Response<bool>> Handle(UpdateSmsTemplateCommand request, CancellationToken cancellationToken)
        {
            var response = Response<bool>.Success(200);
            try
            {
                var smstemplate = await _vetSmsTemplateRepository.GetByIdAsync(request.Id);
                if (smstemplate == null)
                {
                    _logger.LogWarning($"smstemplate update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("smstemplate update failed", 404);
                } 
                smstemplate.UpdateDate = DateTime.Now;
                smstemplate.UpdateUsers = _identity.Account.UserName;
                smstemplate.Active = request.Active;
                smstemplate.Content = request.TemplateContent;
                smstemplate.Name = request.TemplateName;
                smstemplate.EnableAppNotification = request.EnableAppNotification;
                smstemplate.EnableWhatsapp = request.EnableWhatsapp;
                smstemplate.EnableSMS = request.EnableSMS;
                smstemplate.EnableEmail = request.EnableEmail;



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
