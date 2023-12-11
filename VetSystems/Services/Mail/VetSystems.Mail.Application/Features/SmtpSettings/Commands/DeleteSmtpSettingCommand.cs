using AutoMapper;
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
    public class DeleteSmtpSettingCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteSmtpSettingCommandHandler : IRequestHandler<DeleteSmtpSettingCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<DeleteSmtpSettingCommandHandler> _logger;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<SmtpSetting> _smtpSettingRepository;
        private readonly IMapper _mapper;
        public DeleteSmtpSettingCommandHandler(IUnitOfWork uow, IRepository<SmtpSetting> smtpSettingRepository,
             IIdentityRepository identityRepository,
            ILogger<DeleteSmtpSettingCommandHandler> logger, IMapper mapper)
        {
            _uow = uow;
            _identityRepository = identityRepository;
            _logger = logger;
            _smtpSettingRepository = smtpSettingRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(DeleteSmtpSettingCommand request, CancellationToken cancellationToken)
        {
       
            var response = Response<string>.Success(200);
            SmtpSetting smtpSetting = _smtpSettingRepository.GetAsync(p => p.Id == request.Id).Result.FirstOrDefault();
            if (smtpSetting == null)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                response.Data = "Not Found";
                return response;
            }

            smtpSetting.Deleted = true;
            smtpSetting.DeletedUsers = _identityRepository.Account.UserName;
            smtpSetting.DeletedDate = DateTime.Now;
            try
            {
                _smtpSettingRepository.Update(smtpSetting);
                await _uow.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("smtpsetting succesfully updated");
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
