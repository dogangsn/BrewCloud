using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Mail.Application.Models.SmtpSettings;
using BrewCloud.Mail.Domain.Contracts;
using BrewCloud.Mail.Domain.Entities;
using BrewCloud.Shared.Dtos;

namespace BrewCloud.Mail.Application.Features.SmtpSettings.Queries
{
    public class GetSmtpSettingsQuery : IRequest<Response<List<SmtpSettingsDto>>>
    {
    }

    public class GetSmtpSettingsQueryHandler : IRequestHandler<GetSmtpSettingsQuery, Response<List<SmtpSettingsDto>>>
    {
        private readonly IRepository<SmtpSetting> _smtpSettingRepository;
        private readonly IMapper _mapper;
        public GetSmtpSettingsQueryHandler(IRepository<SmtpSetting> smtpSettingRepository, IMapper mapper)
        {
            _smtpSettingRepository = smtpSettingRepository;
            _mapper = mapper;
        }

        public async Task<Response<List<SmtpSettingsDto>>> Handle(GetSmtpSettingsQuery request, CancellationToken cancellationToken)
        {
            Response<List<SmtpSettingsDto>> response = Response<List<SmtpSettingsDto>>.Success(200);
            var keys = _smtpSettingRepository.Get(x => x.Deleted == false).ToList();
            response.Data = _mapper.Map<List<SmtpSettingsDto>>(keys);
            return response;
        }
    }
}
