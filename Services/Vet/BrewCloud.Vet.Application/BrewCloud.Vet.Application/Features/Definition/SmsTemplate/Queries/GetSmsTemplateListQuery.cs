﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Definition.SmsTemplate;
using BrewCloud.Vet.Application.Models.Definition.StockTracking;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Definition.SmsTemplate.Queries
{
    public class GetSmsTemplateListQuery : IRequest<Response<List<SmsTemplateListDto>>>
    {
    }

    public class GetSmsTemplateListQueryHandler : IRequestHandler<GetSmsTemplateListQuery, Response<List<SmsTemplateListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetSmsTemplateListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<SmsTemplateListDto>>> Handle(GetSmsTemplateListQuery request, CancellationToken cancellationToken)
        {

            var response = new Response<List<SmsTemplateListDto>>();
            try
            {
                string query = "SELECT        id, name as TemplateName, active, recid, [content] as TemplateContent, createdate, updatedate, deleteddate, "
                                + " deleted, deletedusers, updateusers, createusers, enableappnotification, enableemail, enablesms, enablewhatsapp, type as smsType"
                                + " FROM            vetsmstemplate where deleted = 0";

                var _data = _uow.Query<SmsTemplateListDto>(query).ToList();
                response = new Response<List<SmsTemplateListDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };


            }
            catch (Exception ex)
            {
                return Response<List<SmsTemplateListDto>>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
