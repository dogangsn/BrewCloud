using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Clinicalstatistics;
using BrewCloud.Vet.Domain.Contracts;
namespace BrewCloud.Vet.Application.Features.Clinicalstatistics.Queries
{

    public class GraphicListQuery : IRequest<Response<List<GraphicListDto>>>
    {
        public int? year { get; set; }
        
    }

    public class GraphicListQueryHandler : IRequestHandler<GraphicListQuery, Response<List<GraphicListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GraphicListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<GraphicListDto>>> Handle(GraphicListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<GraphicListDto>>();
            try
            {


                
                try
                {
                    string query = @"SELECT 
                                	sum(kdv) AS KdvSum,sum(netprice) AS NetPriceSum,
                                	Sum(Case WHEN Type = 2 then total else 0 end ) as SumAlis,
                                	Sum(Case WHEN Type = 1 then total else 0 end ) as SumSatis,
                                	Case when type = 2 then 'Alış Tutarı' else 'Satış Tutarı' end as name,
                                	Case when type = 1 then 'column' else 'line' end as types,
                                	type as realType,
                                	@Year as realDateYear
                                	FROM vetsalebuyowner
                                WHERE  deleted = 0 and MONTH(date) IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12) and Year(date) = @Year
                                Group By Type";
                    var _data = _uow.Query<GraphicListDto>(query, new { Year = request.year}).ToList();

                    foreach ( var item in _data ) 
                    {
                        string querys = @"SELECT 
                                    SUM(CASE WHEN MONTH(date) = 1 THEN total ELSE 0 END) AS Ocak,
                                    SUM(CASE WHEN MONTH(date) = 2 THEN total ELSE 0 END) AS Subat,
                                    SUM(CASE WHEN MONTH(date) = 3 THEN total ELSE 0 END) AS Mart,
                                    SUM(CASE WHEN MONTH(date) = 4 THEN total ELSE 0 END) AS Nisan,
                                    SUM(CASE WHEN MONTH(date) = 5 THEN total ELSE 0 END) AS Mayis,
                                    SUM(CASE WHEN MONTH(date) = 6 THEN total ELSE 0 END) AS Haziran,
                                    SUM(CASE WHEN MONTH(date) = 7 THEN total ELSE 0 END) AS Temmuz,
                                    SUM(CASE WHEN MONTH(date) = 8 THEN total ELSE 0 END) AS Agustos,
                                    SUM(CASE WHEN MONTH(date) = 9 THEN total ELSE 0 END) AS Eylul,
                                    SUM(CASE WHEN MONTH(date) = 10 THEN total ELSE 0 END) AS Ekim,
                                    SUM(CASE WHEN MONTH(date) = 11 THEN total ELSE 0 END) AS Kasim,
                                    SUM(CASE WHEN MONTH(date) = 12 THEN total ELSE 0 END) AS Aralik,
                                	type as realType
                                	FROM vetsalebuyowner
                                WHERE type=@Type and deleted = 0 and MONTH(date) IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12) and Year(date) = @Years
                                Group By Type";
                        var _dataMonth = _uow.Query<MonthList>(querys, new { Type = item.RealType ,Years = request.year }).ToList();
                        item.Months = _dataMonth;
                    }




                    response = new Response<List<GraphicListDto>>
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
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                //response.Errors = ex.ToString();
            }

            return response;
        }
    }
}
