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
using BrewCloud.Vet.Application.Models.Patients;
using BrewCloud.Vet.Domain.Contracts;

namespace BrewCloud.Vet.Application.Features.Customers.Queries
{
    public class GetTransactionMovementListQuery : IRequest<Response<List<TransactionMovementListDto>>>
    {
        public Guid CustomerId { get; set; }
    }

    public class GetTransactionMovementListQueryHandler : IRequestHandler<GetTransactionMovementListQuery, Response<List<TransactionMovementListDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetTransactionMovementListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<TransactionMovementListDto>>> Handle(GetTransactionMovementListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<TransactionMovementListDto>>();
            try
            {
                string _query = "SELECT        vetsalebuyowner.id, vetsalebuyowner.recordid as operationNumber, vetsalebuyowner.date, vetsalebuyowner.remark, vetsalebuyowner.total as amount, vetpaymentmethods.name AS paymenttype\r\n " +
                    "FROM            vetsalebuyowner INNER JOIN\r\n                         vetpaymentmethods ON vetsalebuyowner.paymenttype = vetpaymentmethods.RecId\r\nWHERE        " +
                    "(vetsalebuyowner.deleted = 0) and customerid = @CustomerId and ISNULL(isappointment, 0) = 0 ";
                 var result = _uow.Query<TransactionMovementListDto>(_query, new { CustomerId = request.CustomerId }).ToList();

                response.Data = result;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
