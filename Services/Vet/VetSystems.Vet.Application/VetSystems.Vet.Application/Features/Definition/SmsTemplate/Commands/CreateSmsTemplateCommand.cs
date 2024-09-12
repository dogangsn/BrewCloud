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
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Definition.SmsTemplate.Commands
{
    public class CreateSmsTemplateCommand : IRequest<Response<bool>>
    {
        public bool Active { get; set; }
        public string TemplateName { get; set; } = string.Empty;
        public string TemplateContent { get; set; } = string.Empty;
        public SmsType Type { get; set; }
        public bool? EnableSMS { get; set; }
        public bool? EnableAppNotification { get; set; }
        public bool? EnableEmail { get; set; }
        public bool? EnableWhatsapp { get; set; }
    }

    public class CreateSmsTemplateCommandHandler : IRequestHandler<CreateSmsTemplateCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateSmsTemplateCommandHandler> _logger;
        private readonly IRepository<VetSmsTemplate> _vetSmsTemplateRepository;

        public CreateSmsTemplateCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateSmsTemplateCommandHandler> logger, IRepository<VetSmsTemplate> vetSmsTemplateRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetSmsTemplateRepository = vetSmsTemplateRepository;
        }

        public async Task<Response<bool>> Handle(CreateSmsTemplateCommand request, CancellationToken cancellationToken)
        {
            var response = Response<bool>.Success(200);
            try
            {
                VetSmsTemplate _smsthemplate = new()
                {
                    Id = Guid.NewGuid(),
                    Active = request.Active,
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName,
                    Content = request.TemplateContent,
                    Name = request.TemplateName,
                    EnableAppNotification = request.EnableAppNotification,
                    EnableEmail = request.EnableEmail,
                    EnableSMS = request.EnableSMS,
                    EnableWhatsapp = request.EnableWhatsapp,
                    Type = request.Type
                };
                await _vetSmsTemplateRepository.AddAsync(_smsthemplate);
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
