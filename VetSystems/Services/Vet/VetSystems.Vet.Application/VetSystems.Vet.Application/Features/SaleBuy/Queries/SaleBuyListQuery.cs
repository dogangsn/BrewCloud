using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Features.Customers.Queries;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Application.Models.SaleBuy;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.SaleBuy.Queries
{
    public class SaleBuyListQuery : IRequest<Response<List<SaleBuyListDto>>>
    {
        public int Type { get; set; }
    }

    public class SaleBuyListQueryHandler : IRequestHandler<SaleBuyListQuery, Response<List<SaleBuyListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public SaleBuyListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<SaleBuyListDto>>> Handle(SaleBuyListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<SaleBuyListDto>>();
            try
            {
                string query = "SELECT        vetsalebuyowner.id, vetsalebuyowner.type, vetsalebuyowner.date, vetsalebuyowner.invoiceno, " +
                    "vetsalebuyowner.paymenttype as PaymentName, \r\nvetsalebuyowner.total as Total, \r\nvetsalebuyowner.discount, " +
                    "\r\nvetsalebuyowner.kdv, vetsalebuyowner.remark, \r\n                         " +
                    "vetsalebuyowner.supplierid, vetsalebuyowner.netprice, vetsalebuyowner.customerid, " +
                    "\r\n\t\t\t\t\t\t CASE WHEN vetsalebuyowner.customerid IS NULL OR\r\n                        " +
                    " vetsalebuyowner.customerid = '00000000-0000-0000-0000-000000000000' THEN 'PARAKENDE SATIŞ' ELSE vetcustomers.firstname END AS customerName,\r\n\t\t\t\t\t\t " +
                    " CASE WHEN vetsalebuyowner.supplierid IS NULL OR\r\n                         " +
                    "vetsalebuyowner.supplierid = '00000000-0000-0000-0000-000000000000' THEN '-' ELSE vetsuppliers.suppliername END AS supplierName\r\n\r\nFROM            " +
                    "vetsalebuyowner INNER JOIN\r\n                         " +
                    "vetsalebuytrans ON vetsalebuyowner.id = vetsalebuytrans.ownerid LEFT JOIN\r\n                         " +
                    "vetcustomers ON vetsalebuyowner.customerid = vetcustomers.id LEFT JOIN\r\n                         " +
                    "vetsuppliers ON vetsalebuyowner.supplierid = vetsuppliers.id\r\nWHERE        (vetsalebuyowner.deleted = 0) and (vetsalebuyowner.type = @type)\r\n";
                var _data = _uow.Query<SaleBuyListDto>(query, new { type = request.Type }).ToList();
                response = new Response<List<SaleBuyListDto>>
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
