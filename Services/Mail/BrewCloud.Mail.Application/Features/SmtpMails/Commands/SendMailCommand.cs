using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Mail.Domain.Common;
using BrewCloud.Mail.Domain.Contracts;
using BrewCloud.Mail.Domain.Entities;
using BrewCloud.Shared.Dtos.MailKit;
using BrewCloud.Shared.Service.MailKit;

namespace BrewCloud.Mail.Application.Features.SmtpMails.Commands
{
    public class SendMailCommand : IRequest<Response<string>>
    {
        public string EmailToId { get; set; }
        public string EmailToName { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string ConnectionString { get; set; }
        public Guid RecId { get; set; }
        public int MailType { get; set; }

    }

    public class SendMailCommandHandler : IRequestHandler<SendMailCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<SendMailCommandHandler> _logger;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<SmtpSetting> _smtpSettingRepository;
        private readonly IMapper _mapper;

        public SendMailCommandHandler(IUnitOfWork uow,IRepository<SmtpSetting> smtpSettingRepository, ILogger<SendMailCommandHandler> logger, IMapper mapper, IIdentityRepository identityRepository)
        {
            _uow = uow;
            _logger = logger;
            _mapper = mapper;
            _smtpSettingRepository = smtpSettingRepository;
            _identityRepository = identityRepository;
        }


        public  async Task<Response<string>> Handle(SendMailCommand request, CancellationToken cancellationToken)
        {
            var response = Response<string>.Success(200);

            try
            {
                SmtpSetting smtpSetting = await _smtpSettingRepository.FirstOrDefaultAsync(x => x.Defaults == true && x.Deleted == false);
                if (smtpSetting != null)
                {
                    MailDetailDto mail = _mapper.Map<MailDetailDto>(request);
                    SendMailService mailService = new SendMailService(_mapper.Map<MailSenderDto>(smtpSetting));
                    var senderResponse = mailService.SendMailWelcome(mail);

                    _logger.LogInformation("smtpmail succesfully saved");
                    response.IsSuccessful = true;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
