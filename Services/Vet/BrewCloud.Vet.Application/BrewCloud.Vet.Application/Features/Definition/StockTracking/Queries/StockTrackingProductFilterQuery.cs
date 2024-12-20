﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Definition.StockTracking;
using BrewCloud.Vet.Application.Models.Definition.UnitDefinitions;
using BrewCloud.Vet.Domain.Contracts;


namespace BrewCloud.Vet.Application.Features.Definition.StockTracking.Queries
{
    public class StockTrackingProductFilterQuery : IRequest<Response<List<StockTrackingDto>>>
    {
        public Guid ProductId { get; set; }
    }

    public class StockTrackingProductFilterQueryHandler : IRequestHandler<StockTrackingProductFilterQuery, Response<List<StockTrackingDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public StockTrackingProductFilterQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<StockTrackingDto>>> Handle(StockTrackingProductFilterQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<StockTrackingDto>>();

            try
            {
                #region old
                //string query = "select    CASE vetstocktracking.processtype "
                //         + "         WHEN 1 THEN 'Yeni Stok' "
                //         + "         WHEN 2 THEN 'Transfer' "
                //         + "         WHEN 3 THEN 'Ayarlama' "
                //         + "         WHEN 4 THEN 'Diğer' "
                //         + "         ELSE '-'   "
                //         + "     END AS ProcessTypeName,  CASE  "
                //         + "         WHEN vetstocktracking.supplierid IS NULL OR vetstocktracking.supplierid = '00000000-0000-0000-0000-000000000000' THEN '-' "
                //         + "         ELSE vetsuppliers.SupplierName "
                //         + "     END AS SupplierName, "
                //         + " 	  CASE  "
                //         + "         WHEN vetstocktracking.ExpirationDate IS NULL THEN 'SKT bilgisi yok' "
                //         + "         ELSE CONVERT(varchar, vetstocktracking.ExpirationDate, 103)  "
                //         + "     END AS ExpirationDateString, "
                //         + " vetstocktracking.* from vetstocktracking "
                //         + " Left Join vetsuppliers ON vetstocktracking.supplierid = vetsuppliers.id  "
                //         + " where  productid = @xProductId and vetstocktracking.deleted = 0"
                //         + " order by vetstocktracking.createdate desc "; 
                #endregion

                string query = "select    CASE vetstocktracking.processtype "
                        + " WHEN 1 THEN 'Yeni Stok'           "
                        + " WHEN 2 THEN 'Transfer'           "
                        + " WHEN 3 THEN 'Ayarlama'           "
                        + " WHEN 4 THEN 'Diğer'           "
                        + " ELSE '-'         "
                        + " END AS ProcessTypeName,   "
                        + " CASE            "
                        + " WHEN vetstocktracking.supplierid IS NULL OR vetstocktracking.supplierid = '00000000-0000-0000-0000-000000000000' THEN '-'          ELSE vetsuppliers.SupplierName      END AS SupplierName,  	   "
                        + " CASE            "
                        + " WHEN vetstocktracking.ExpirationDate IS NULL THEN 'SKT bilgisi yok'           "
                        + " ELSE CONVERT(varchar, vetstocktracking.ExpirationDate, 103)        "
                        + " END AS ExpirationDateString,   "
                        + "  CASE "
                        + "         WHEN vetstocktracking.type = 2 THEN -vetstocktracking.piece "
                        + "         ELSE vetstocktracking.piece "
                        + "     END AS Piece, "
                        + " 	vetstocktracking.id, "
                        + " 	vetstocktracking.PurchasePrice, "
                        + " 	vetstocktracking.RemainingPiece, "
                        + " 	vetstocktracking.ProcessType, "
                        + " 	vetstocktracking.SupplierId, "
                        + " 	vetstocktracking.ExpirationDate "
                        + " from vetstocktracking   "
                        + " Left Join vetsuppliers ON vetstocktracking.supplierid = vetsuppliers.id    "
                        + " where  productid = @xProductId and vetstocktracking.deleted = 0  "
                        + " order by vetstocktracking.createdate desc ";

                var _data = _uow.Query<StockTrackingDto>(query, new { xProductId = request.ProductId }).ToList();
                response = new Response<List<StockTrackingDto>>
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
