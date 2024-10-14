using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Definition.Product;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Definition.ProductDescription.Queries
{
    public class ProductDescriptionFiltersQuery : IRequest<Response<List<ProductDescriptionsDto>>>
    {
        public int ProductType { get; set; }
    }

    public class ProductDescriptionFiltersQueryHandler : IRequestHandler<ProductDescriptionFiltersQuery, Response<List<ProductDescriptionsDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductDescriptionFiltersQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<ProductDescriptionsDto>>> Handle(ProductDescriptionFiltersQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<ProductDescriptionsDto>>();
            try
            {
                //string query = "Select * from VetProducts where Deleted = 0 and producttypeid = @ProductTypeId order by CreateDate ";
                string query =
                         " SELECT        ISNULL(SUM( CASE WHEN vetstocktracking.type = 2 THEN -vetstocktracking.piece ELSE vetstocktracking.piece END), 0)  AS stockcount, vetproducts.id, vetproducts.name, vetproducts.producttypeid, vetproducts.unitid, vetproducts.categoryid, vetproducts.supplierid, vetproducts.productbarcode, vetproducts.productcode, "
                        + "                          vetproducts.ratio, vetproducts.buyingprice, vetproducts.sellingprice, vetproducts.criticalamount, vetproducts.active, vetproducts.sellingincludekdv, vetproducts.buyingincludekdv, vetproducts.fixprice, "
                        + "                          vetproducts.isexpirationdate, vetproducts.animaltype, vetproducts.numberrepetitions, vetproducts.createdate, vetproducts.updatedate, vetproducts.deleteddate, vetproducts.deleted, vetproducts.deletedusers, "
                        + "                          vetproducts.updateusers, vetproducts.createusers, vetproducts.storeid, vetproducts.taxisid, vetunits.unitname "
                        + " FROM            vetproducts left JOIN"
                        + "                          vetstocktracking ON vetstocktracking.productid = vetproducts.id and vetstocktracking.deleted = 0"
                        + "             Left Join vetunits ON vetunits.id = vetproducts.unitid and vetunits.deleted = 0 "
                        + " WHERE        (vetproducts.deleted = 0) and vetproducts.producttypeid = @ProductTypeId"
                        + " GROUP BY vetproducts.id,vetproducts.name, vetproducts.producttypeid, vetproducts.unitid, vetproducts.categoryid, vetproducts.supplierid, vetproducts.productbarcode, vetproducts.productcode, "
                        + "                          vetproducts.ratio, vetproducts.buyingprice, vetproducts.sellingprice, vetproducts.criticalamount, vetproducts.active, vetproducts.sellingincludekdv, vetproducts.buyingincludekdv, vetproducts.fixprice, "
                        + "                          vetproducts.isexpirationdate, vetproducts.animaltype, vetproducts.numberrepetitions, vetproducts.createdate, vetproducts.updatedate, vetproducts.deleteddate, vetproducts.deleted, vetproducts.deletedusers, "
                        + "                          vetproducts.updateusers, vetproducts.createusers, vetproducts.storeid, vetproducts.taxisid,vetunits.unitname order by CreateDate";


                var _data =  _uow.Query<ProductDescriptionsDto>(query, new { ProductTypeId= request.ProductType }).ToList();
                response = new Response<List<ProductDescriptionsDto>>
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
