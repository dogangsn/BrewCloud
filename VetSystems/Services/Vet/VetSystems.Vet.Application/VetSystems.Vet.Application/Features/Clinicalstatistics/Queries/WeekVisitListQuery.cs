using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Clinicalstatistics;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Clinicalstatistics.Queries
{

        public class WeekVisitListQuery : IRequest<Response<List<WeekVisitListDto>>>
        {
            public int? thisAndLastType { get; set; }

        }

        public class WeekVisitListQueryHandler : IRequestHandler<WeekVisitListQuery, Response<List<WeekVisitListDto>>>
        {
            private readonly IIdentityRepository _identityRepository;
            private readonly IUnitOfWork _uow;
            private readonly IMapper _mapper;

            public WeekVisitListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
            {
                _identityRepository = identityRepository;
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<Response<List<WeekVisitListDto>>> Handle(WeekVisitListQuery request, CancellationToken cancellationToken)
            {
                var response = new Response<List<WeekVisitListDto>>();
                    try
                    {
                        string query = @"select 
                                            CASE 
                                                    WHEN DATEPART(dw, begindate) = 1 THEN 'Pazar'
                                                    WHEN DATEPART(dw, begindate) = 2 THEN 'Pazartesi'
                                                    WHEN DATEPART(dw, begindate) = 3 THEN 'Salı'
                                                    WHEN DATEPART(dw, begindate) = 4 THEN 'Çarşamba'
                                                    WHEN DATEPART(dw, begindate) = 5 THEN 'Perşembe'
                                                    WHEN DATEPART(dw, begindate) = 6 THEN 'Cuma'
                                                    WHEN DATEPART(dw, begindate) = 7 THEN 'Cumartesi'
                                                END AS DayName,
                                            begindate as BeginDate,
                                            Count(begindate) as AllVisitcount 
                                            ,Sum(Case when deleted = 0 then 1 else 0 end ) as VisitCountSum ,
                                            Sum(Case when deleted = 1 then 1 else 0 end ) as UnVisitCountSum 
                                            from  vetappointments where begindate < (SELECT DATEADD(wk, DATEDIFF(wk, 0, GetDate())-@Type, 7)) 
                                            and begindate > (SELECT DATEADD(wk, DATEDIFF(wk, 0, GetDate())-@Type , 0)) 
                                            group by begindate";
                        var _data = _uow.Query<WeekVisitListDto>(query, new { Type = request.thisAndLastType }).ToList();


                        response = new Response<List<WeekVisitListDto>>
                        {
                            Data = _data,
                            IsSuccessful = true,
                        };
                    }
                    catch (Exception ex)
                    {
                        response.IsSuccessful = false;
                        //response.Errors = ex.ToString();
                    }

                    return response;
            }
        }
    
}
