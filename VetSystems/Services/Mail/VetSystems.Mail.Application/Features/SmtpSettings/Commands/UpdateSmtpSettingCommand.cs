using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Mail.Domain.Contracts;
using VetSystems.Shared.Service;
using VetSystems.Mail.Domain.Entities;

namespace VetSystems.Mail.Application.Features.SmtpSettings.Commands
{
    public class UpdateSmtpSettingCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public bool Defaults { get; set; } = false;
        public string DisplayName { get; set; } = string.Empty;
        public string EmailId { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 0;
        public bool UseSSL { get; set; } = false;
    }

    public class UpdateSmtpSettingCommandHandler : IRequestHandler<UpdateSmtpSettingCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UpdateSmtpSettingCommandHandler> _logger;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<SmtpSetting> _smtpSettingRepository;
        private readonly IMapper _mapper;
        public UpdateSmtpSettingCommandHandler(IUnitOfWork uow, IRepository<SmtpSetting> smtpSettingRepository, IIdentityRepository identityRepository, ILogger<UpdateSmtpSettingCommandHandler> logger, IMapper mapper)
        {
            _uow = uow;
            _identityRepository = identityRepository;
            _logger = logger;
            _smtpSettingRepository = smtpSettingRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(UpdateSmtpSettingCommand request, CancellationToken cancellationToken)
        {
 
            var response = Response<string>.Success(200);
            SmtpSetting smtpSetting = _smtpSettingRepository.GetAsync(p => p.EmailId == request.EmailId && p.Id != request.Id && p.Deleted == false).Result.FirstOrDefault();
            if (smtpSetting != null)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Warning;
                response.Data = "this email allready exists";
                return response;

            }
            smtpSetting = _smtpSettingRepository.GetByIdAsync(request.Id).Result;
            smtpSetting.UpdateUsers = _identityRepository.Account.UserName;
            smtpSetting.UpdateDate = DateTime.Now;
            smtpSetting.DisplayName = request.DisplayName;
            smtpSetting.Password = request.Password;
            smtpSetting.EmailId = request.EmailId;
            smtpSetting.UseSSL = request.UseSSL;
            smtpSetting.Defaults = request.Defaults;
            smtpSetting.Host = request.Host;
            smtpSetting.Port = request.Port;
            try
            {
                _smtpSettingRepository.Update(smtpSetting);


                await _uow.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("smtpsetting succesfully saved");
                response.IsSuccessful = true;
                response.Data = smtpSetting.Id.ToString();
            }
            catch (Exception ex)
            {
                response = Response<string>.Fail("smtpsetting not saved: " + ex.Message, 501);
                _logger.LogError("smtpsetting not saved: " + ex.Message);
            }

            return response;
        }
    }
}
