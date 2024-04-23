using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Customers.Queries
{
    public class GetPaymentTransactionListQuery : IRequest<Response<List<PaymentTransactionListDto>>>
    {
        public Guid CustomerId { get; set; }
    }

    public class GetPaymentTransactionListQueryHandler : IRequestHandler<GetPaymentTransactionListQuery, Response<List<PaymentTransactionListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetPaymentTransactionListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<PaymentTransactionListDto>>> Handle(GetPaymentTransactionListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<PaymentTransactionListDto>>();
            try
            {
                string _query = "SELECT "
                                + "     vetappointments.id, "
                                + "     vetappointments.appointmenttype, "
                                + " 	vetproducts.sellingprice,  "
                                + "     vetappointments.vaccineid,"
                                + "     CASE "
                                + "         WHEN vetappointments.vaccineid = '00000000-0000-0000-0000-000000000000' THEN  "
                                + "             CASE vetappointments.appointmenttype "
                                + "                 WHEN 0 THEN 'İlk Muayene' "
                                + "                 WHEN 1 THEN 'Aşı Randevusu' "
                                + "                 WHEN 2 THEN 'Genel Muayene' "
                                + "                 WHEN 3 THEN 'Kontrol Muayene' "
                                + "                 WHEN 4 THEN 'Operasyon' "
                                + "                 WHEN 5 THEN 'Tıraş' "
                                + "                 WHEN 6 THEN 'Tedavi' "
                                + "                 ELSE 'Diğer' "
                                + "             END "
                                + "         ELSE "
                                + "             CONCAT(  CASE vetappointments.appointmenttype "
                                + "                 WHEN 0 THEN 'İlk Muayene' "
                                + "                 WHEN 1 THEN 'Aşı Randevusu' "
                                + "                 WHEN 2 THEN 'Genel Muayene' "
                                + "                 WHEN 3 THEN 'Kontrol Muayene' "
                                + "                 WHEN 4 THEN 'Operasyon' "
                                + "                 WHEN 5 THEN 'Tıraş' "
                                + "                 WHEN 6 THEN 'Tedavi' "
                                + "                 ELSE 'Diğer' "
                                + "             END , ' - ' , vetproducts.name "
                                + "            ) "
                                + "     END AS textvalue , vetappointmenttypes.isdefaultprice, vetappointmenttypes.price,CASE WHEN vetappointments.vaccineid = '00000000-0000-0000-0000-000000000000' THEN  vetappointmenttypes.taxisid ELSE vetproducts.taxisid END as taxisid "
                                + " FROM "
                                + "     vetappointments "
                                + " INNER JOIN "
                                + "     vetcustomers ON vetappointments.customerid = vetcustomers.id "
                                + " LEFT JOIN "
                                + "     vetproducts ON vetappointments.vaccineid = vetproducts.id "
                                + " LEFT JOIN vetappointmenttypes ON vetappointments.appointmenttype = vetappointmenttypes.type and vetappointmenttypes.deleted = 0 "
                                + " WHERE "
                                + "     vetappointments.deleted = 0 "
                                + "     AND COALESCE(vetappointments.iscompleted, 0) = 1 and ISNULL(vetappointments.ispaymentreceived, 0) = 0 and  vetappointments.customerid = @CustomerId";

                var result = _uow.Query<PaymentTransactionListDto>(_query, new { CustomerId = request.CustomerId }).ToList();
                response.Data = result;
                response.IsSuccessful = true;

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Errors.Add(ex.Message);
            }
            return response;
        }
    }
}
