using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Application.Models.Definition.Product;
using AutoMapper;

namespace VetSystems.Vet.Application.Features.Definition.ProductDescription.Queries
{
    public class ProductMovementListQuery : IRequest<Response<List<ProductMovementListDto>>>
    {
        public Guid ProductId { get; set; }
    }

    public class ProductMovementListQueryHandler : IRequestHandler<ProductMovementListQuery, Response<List<ProductMovementListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductMovementListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<ProductMovementListDto>>> Handle(ProductMovementListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<ProductMovementListDto>>();
            try
            {
                string query = "select trans.id,trans.createdate,trans.invoiceno,trans.netprice "
                                + " from "
                                + " vetsalebuytrans as trans "
                                + " Inner join vetsalebuyowner on vetsalebuyowner.id = trans.vetsalebuyownerid "
                                + " where trans.productid = @productid  and trans.deleted = 0 ";

                var _data = _uow.Query<ProductMovementListDto>(query, new { productid = request.ProductId }).ToList();
                response = new Response<List<ProductMovementListDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;

            }
            return response;
        }
    }
}
