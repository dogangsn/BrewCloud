using AutoMapper;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Clinicalstatistics;
using BrewCloud.Vet.Domain.Contracts;


namespace BrewCloud.Vet.Application.Features.Clinicalstatistics.Queries
{
    public class ClinicalstatisticsListQuery : IRequest<Response<List<ClinicalstatisticsListDto>>>
    {
        public ClinicalstatisticsListDto ThisWeekCustomerTotal { get; set; }
        public ClinicalstatisticsListDto PaymentTypeTotal { get; set; }
        public ClinicalstatisticsListDto PaymentTypeYearsTotal { get; set; }
        //public string Total { get; set; }
        //public int? PaymentType { get; set; }
        //public int? Year { get; set; }
        //public bool? IsMonth { get; set; }
        //public int? Type { get; set; }
        //public int? RequestType { get; set; }
    }

    public class ClinicalstatisticsListQueryHandler : IRequestHandler<ClinicalstatisticsListQuery, Response<List<ClinicalstatisticsListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ClinicalstatisticsListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<ClinicalstatisticsListDto>>> Handle(ClinicalstatisticsListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<ClinicalstatisticsListDto>>();
            response.Data = new List<ClinicalstatisticsListDto>();
            try
            {
                //response = new Response<List<ClinicalstatisticsListResponseDto>>();
                if (request.ThisWeekCustomerTotal != null)
                {
                    if(request.ThisWeekCustomerTotal.Type == 1)
                    {
                        string query = @"select customerid,paymenttype as paymentType,sum(total) AS total, 2 as RequestType   from vetsalebuyowner where type = 1 and deleted = 0 
                                    and date > DateAdd(day,-7,GetDate()) group by customerid,paymenttype  ";

                        var _data = _uow.Query<ClinicalstatisticsListDto>(query).ToList();
                        response.Data.AddRange(_data);
                    }
                    
                    
                    //response = new Response<List<ClinicalstatisticsListResponseDto>>
                    //{
                    //    Data = _data,
                    //    IsSuccessful = true,
                    //};
                }
                if(request.PaymentTypeTotal != null)
                {
                    if(request.PaymentTypeTotal.Type == 1)
                    {
                        string query = @"select sum(total) as total,paymenttype as paymentType,YEAR(date) as year , 3 as RequestType  from VetSaleBuyOwner 
                                 where type =@Type and deleted = 0 
                                 group by paymenttype,YEAR(date)";
                        var _data = _uow.Query<ClinicalstatisticsListDto>(query, new { type = request.PaymentTypeTotal.Type }).ToList();
                        response.Data.AddRange(_data);
                    }
                    
                }
                if(request.PaymentTypeYearsTotal != null)
                {
                    if(request.PaymentTypeYearsTotal.Type == 2)
                    {
                        string query = @" select sum(total) as total,paymenttype as paymentType,YEAR(date) as year, 4 as RequestType , (CASE
                                           WHEN MONTH(date) = 1 THEN 'Ocak'
                                           WHEN MONTH(date) = 2 THEN 'Şubat'
                                           WHEN MONTH(date) = 3 THEN 'Mart'
                                           WHEN MONTH(date) = 4 THEN 'Nisan'
                                           WHEN MONTH(date) = 5 THEN 'Mayıs'
                                           WHEN MONTH(date) = 6 THEN 'Haziran'
                                           WHEN MONTH(date) = 7 THEN 'Temmuz'
                                           WHEN MONTH(date) = 8 THEN 'Ağustos'
                                           WHEN MONTH(date) = 9 THEN 'Eylül'
                                           WHEN MONTH(date) = 10 THEN 'Ekim'
                                           WHEN MONTH(date) = 11 THEN 'Kasım'
                                           WHEN MONTH(date) = 12 THEN 'Aralık'
                                       END) as [month] from VetSaleBuyOwner 
                                 where paymenttype = @paymentType and YEAR(date)=@Year and type =@Type and deleted = 0 
                                 group by paymenttype,YEAR(date), MONTH(date) ";
                        var _data = _uow.Query<ClinicalstatisticsListDto>(query, new { paymentType = request.PaymentTypeYearsTotal.paymentType, year = request.PaymentTypeYearsTotal.Year, type = request.PaymentTypeYearsTotal.Type }).ToList();
                        response.Data.AddRange(_data);
                    }
                   
                }

                response.IsSuccessful = true;
                //if (request.PaymentType != null && request.Year != null)
                //{
                //    //string query = @"select sum(total) as total,paymenttype,YEAR(date) as year 
                //    //             from VetSaleBuyOwner 
                //    //             where YEAR(date)= @year and paymentType =@paymentType and type = 1 and deleted = 0 
                //    //             group by paymenttype,YEAR(date)";
                //    string query = @"select sum(total) as total,paymenttype,YEAR(date) as year ";
                //                    if(request.IsMonth == true)
                //    {
                //        query += @", (CASE
                //                           WHEN MONTH(date) = 1 THEN 'Ocak'
                //                           WHEN MONTH(date) = 2 THEN 'Şubat'
                //                           WHEN MONTH(date) = 3 THEN 'Mart'
                //                           WHEN MONTH(date) = 4 THEN 'Nisan'
                //                           WHEN MONTH(date) = 5 THEN 'Mayıs'
                //                           WHEN MONTH(date) = 6 THEN 'Haziran'
                //                           WHEN MONTH(date) = 7 THEN 'Temmuz'
                //                           WHEN MONTH(date) = 8 THEN 'Ağustos'
                //                           WHEN MONTH(date) = 9 THEN 'Eylül'
                //                           WHEN MONTH(date) = 10 THEN 'Ekim'
                //                           WHEN MONTH(date) = 11 THEN 'Kasım'
                //                           WHEN MONTH(date) = 12 THEN 'Aralık'
                //                       END) as [month]";
                //    }

                //    query += @" from VetSaleBuyOwner 
                //                 where ";
                //    if(request.IsMonth == true)
                //    {
                //        query += @"paymenttype = @paymentType and YEAR(date)=@Year and ";
                //    }
                //    query += @"type =@Type and deleted = 0 
                //                 group by paymenttype,YEAR(date)";
                //    if(request.IsMonth == true)
                //    {
                //        query += ", MONTH(date)";
                //    }

                //    var _data = _uow.Query<ClinicalstatisticsListResponseDto>(query, new { paymentType = request.PaymentType, year = request.Year,type = request.Type }).ToList();
                //    response = new Response<List<ClinicalstatisticsListResponseDto>>
                //    {
                //        Data = _data,
                //        IsSuccessful = true,
                //    };
                //}
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
