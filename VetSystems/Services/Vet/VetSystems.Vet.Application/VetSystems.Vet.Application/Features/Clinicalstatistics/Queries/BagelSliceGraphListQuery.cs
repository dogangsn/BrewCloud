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

    public class BagelSliceGraphListQuery : IRequest<Response<List<BagelSliceGraphListDto>>>
    {
    }

    public class BagelSliceGraphListQueryHandler : IRequestHandler<BagelSliceGraphListQuery, Response<List<BagelSliceGraphListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public BagelSliceGraphListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<BagelSliceGraphListDto>>> Handle(BagelSliceGraphListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<BagelSliceGraphListDto>>();
            response.Data = new List<BagelSliceGraphListDto>();
            try
            {
                string query = @"select top 2 animaltype as id,Count(*) as Counts, 1 as types 
                                from vetpatients where deleted = 0 and CreateDate > DATEADD(MONTH,-1,GETDATE())
                                Group by animaltype ORDER BY Counts DESC";
                var _data = _uow.Query<BagelSliceGraphListDto>(query).ToList();
                response.Data.AddRange(_data);


                string query2 = @"select top 2 suppliers as GuidId,Count(*) as Counts, 2 as types from vetdemands where deleted = 0 
                                 and date > DATEADD(MONTH,-1,GETDATE())
                                 Group by suppliers order by Counts DESC ";
                var _data2 = _uow.Query<BagelSliceGraphListDto>(query2).ToList();
                response.Data.AddRange(_data2);
                string query3 = @"select top 2 productid as GuidId,Count(*) as Counts, 3 as types from vetdemandproducts where deleted = 0 
                                  and createdate > DATEADD(MONTH,-1,GETDATE())
                                  Group by productid order by Counts DESC ";
                var _data3 = _uow.Query<BagelSliceGraphListDto>(query3).ToList();

                response.Data.AddRange(_data3);
                response.IsSuccessful = true;
                
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
