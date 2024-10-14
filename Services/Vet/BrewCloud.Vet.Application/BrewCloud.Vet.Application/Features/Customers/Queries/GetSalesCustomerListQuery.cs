using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Customers;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Customers.Queries
{
    public class GetSalesCustomerListQuery : IRequest<Response<List<SalesCustomerListDto>>>
    {
        public Guid CustomerId { get; set; }
    }

    public class GetSalesCustomerListQueryHandler : IRequestHandler<GetSalesCustomerListQuery, Response<List<SalesCustomerListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetSalesCustomerListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<SalesCustomerListDto>>> Handle(GetSalesCustomerListQuery request, CancellationToken cancellationToken)
        {
            var response = Response<List<SalesCustomerListDto>>.Success(200);
            try
            {
                #region old
                //string _query = "SELECT   "
                // + " vetsalebuyowner.id as SaleOwnerId,  "
                // + " ISNULL(vetpaymentcollection.collectionid, '00000000-0000-0000-0000-000000000000') as CollectionId,"
                // + " vetproducts.name as SalesContent,"
                // + " vetsalebuyowner.date,  "
                // + " vetsalebuyowner.total as Amount,  "
                // + " ISNULL(vetpaymentcollection.credit, 0) as Collection, "
                // + " vetsalebuyowner.total - ISNULL(vetpaymentcollection.credit, 0) as RameiningBalance,"
                // + " vetsalebuyowner.createusers as Kayitlikullanici,"
                // + " vetsalebuyowner.createdate as KayitTarihi"
                // + " "
                // + " FROM            vetsalebuyowner "
                // + " INNER JOIN vetsalebuytrans ON vetsalebuyowner.id = vetsalebuytrans.ownerid "
                // + " LEFT JOIN vetproducts ON vetsalebuytrans.productid = vetproducts.id "
                // + " LEFT JOIN vetcustomers ON vetsalebuyowner.customerid = vetcustomers.id"
                // + " LEFT JOIN vetpaymentcollection ON vetsalebuyowner.id = vetpaymentcollection.salebuyid and vetpaymentcollection.deleted = 0 "
                // + " where "
                // + " vetsalebuyowner.deleted = 0 and vetsalebuyowner.customerid = @xCustomerId "
                // + " ORDER BY vetsalebuyowner.CreateDate"; 
                #endregion
                string _query =  " SELECT  "
                    + "     vetsalebuyowner.id as SaleOwnerId, "
                    + "     ISNULL(vetpaymentcollection.collectionid, '00000000-0000-0000-0000-000000000000') as CollectionId, "
                    + "                  CASE"
                    + "     WHEN vetsalebuyowner.examinationsid != '00000000-0000-0000-0000-000000000000' THEN 'MUAYENE SATIŞ'"
                    + "     WHEN vetsalebuyowner.AccomodationId != '00000000-0000-0000-0000-000000000000' THEN 'KONAKLAMA BEDELİ'"
                    + "     ELSE STUFF(("
                    + "         SELECT ', ' + vetproducts.name"
                    + "         FROM vetsalebuytrans"
                    + "         INNER JOIN vetproducts ON vetsalebuytrans.productid = vetproducts.id"
                    + "         WHERE vetsalebuytrans.ownerid = vetsalebuyowner.id"
                    + "         FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 2, '')"
                    + " END as SalesContent, "
                    + "     vetsalebuyowner.date, "
                    + "     vetsalebuyowner.total as Amount, "
                    + "     ISNULL(vetpaymentcollection.credit, 0) as Collection, "
                    + "     vetsalebuyowner.total - ISNULL(vetpaymentcollection.credit, 0) as RameiningBalance, "
                    + "     vetsalebuyowner.createusers as Kayitlikullanici, "
                    + "     vetsalebuyowner.createdate as KayitTarihi, "
                    + "  vetsalebuyowner.examinationsid as examinationsid, "
                    + " vetsalebuyowner.isexaminations as isexaminations "
                    + " FROM  "
                    + "     vetsalebuyowner "
                    + " LEFT JOIN  "
                    + "     vetcustomers ON vetsalebuyowner.customerid = vetcustomers.id "
                    + " LEFT JOIN  "
                    + "     vetpaymentcollection ON vetsalebuyowner.id = vetpaymentcollection.salebuyid AND vetpaymentcollection.deleted = 0 "
                    + " WHERE  "
                    + "     vetsalebuyowner.deleted = 0 AND  "
                    + "     vetsalebuyowner.customerid =  @xCustomerId "
                    + " ORDER BY  "
                    + "     vetsalebuyowner.createdate"; 


                var result = _uow.Query<SalesCustomerListDto>(_query, new { xCustomerId = request.CustomerId }).ToList();
                response.Data = result;
            }
            catch (Exception ex)
            {
                return Response<List<SalesCustomerListDto>>.Fail(ex.Message, 400);
            }
            return response;
        }
    }
}
