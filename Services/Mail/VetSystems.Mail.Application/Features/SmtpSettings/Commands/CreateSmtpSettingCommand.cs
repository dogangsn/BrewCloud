using AutoMapper;
using MassTransit.Futures.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Mail.Domain.Contracts;
using VetSystems.Mail.Domain.Entities;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;

namespace VetSystems.Mail.Application.Features.SmtpSettings.Commands
{
    public class CreateSmtpSettingCommand : IRequest<Response<string>>
    {
        public bool Defaults { get; set; } = false;
        public string DisplayName { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 0;
        public bool UseSSL { get; set; } = false;
    }

    public class CreateSmtpSettingCommandHandler : IRequestHandler<CreateSmtpSettingCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<CreateSmtpSettingCommandHandler> _logger;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<SmtpSetting> _smtpSettingRepository;
        private readonly IMapper _mapper;

        public CreateSmtpSettingCommandHandler(IUnitOfWork uow, IRepository<SmtpSetting> smtpSettingRepository,
            IIdentityRepository identityRepository,
           ILogger<CreateSmtpSettingCommandHandler> logger, IMapper mapper)
        {
            _uow = uow;
            _identityRepository = identityRepository;
            _logger = logger;
            _smtpSettingRepository = smtpSettingRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(CreateSmtpSettingCommand request, CancellationToken cancellationToken)
        {
            var response = Response<string>.Success(200);

            SmtpSetting smtpSetting = _smtpSettingRepository.GetAsync(p => p.EmailId == request.EmailId && p.Deleted == false).Result.FirstOrDefault();
            if (smtpSetting != null)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Warning;
                response.Data = "Aynı E-Mail Adresi Tanımlıdır.";
                return response;
            }

            List<SmtpSetting> trans = (await _smtpSettingRepository.GetAsync(x => x.Deleted == false && x.Defaults == true)).ToList();
            if (trans.Count() > 0 && request.Defaults)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Warning;
                response.Data = "Varsayılan mail adresi seçimi birden fazla olamaz.";
                return response;
            }

            smtpSetting = _mapper.Map<SmtpSetting>(request);
            smtpSetting.Id = Guid.NewGuid();
            smtpSetting.CreateUsers = _identityRepository.Account.UserName;
            smtpSetting.CreateDate = DateTime.Now;
            try
            {

                await _smtpSettingRepository.AddAsync(smtpSetting);
                await _uow.SaveChangesAsync(cancellationToken);
                response.Data = smtpSetting.Id.ToString();
            }
            catch (Exception ex)
            {
                response = Response<string>.Fail("smtpsetting kayıtyapılamadı: " + ex.Message, 501);
                _logger.LogError("smtpsetting not saved: " + ex.Message);
            }

            return response;
        }
    }
}
