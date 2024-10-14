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
    public class GetPayChartListQuery  : IRequest<Response<List<PayChartListDto>>>
    {
        public Guid CustomerId { get; set; }
    }

    public class GetPayChartListQueryHandler : IRequestHandler<GetPayChartListQuery, Response<List<PayChartListDto>>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetPayChartListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<PayChartListDto>>> Handle(GetPayChartListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<PayChartListDto>>();
            try
            {
                //string query = "SELECT        vetpaymentcollection.id, vetpaymentcollection.date, vetpaymentcollection.debit,  "
                //        + "  CASE WHEN vetappointments.vaccineid = '00000000-0000-0000-0000-000000000000' THEN CASE vetappointments.appointmenttype WHEN 0 THEN 'İlk Muayene' WHEN 1 THEN 'Aşı Randevusu' WHEN 2 THEN 'Genel Muayene' "
                //        + "   WHEN 3 THEN 'Kontrol Muayene' WHEN 4 THEN 'Operasyon' WHEN 5 THEN 'Tıraş' WHEN 6 THEN 'Tedavi' ELSE 'Diğer' END ELSE CONCAT(CASE vetappointments.appointmenttype WHEN 0 THEN 'İlk Muayene' WHEN 1 THEN " 
                //        + "   'Aşı Randevusu' WHEN 2 THEN 'Genel Muayene' WHEN 3 THEN 'Kontrol Muayene' WHEN 4 THEN 'Operasyon' WHEN 5 THEN 'Tıraş' WHEN 6 THEN 'Tedavi' ELSE 'Diğer' END, ' - ', vetproducts.name) END AS operation,  "
                //        + "  vetpaymentcollection.paid, vetpaymentcollection.totalpaid, vetpaymentcollection.total, vetappointments.id as appointmentId "
                //        + "  FROM            vetpaymentcollection INNER JOIN "
                //        + "  vetcustomers ON vetpaymentcollection.customerid = vetcustomers.id INNER JOIN "
                //        + "  vetappointments ON vetpaymentcollection.collectionid = vetappointments.id LEFT JOIN "
                //        + "  vetproducts ON vetappointments.vaccineid = vetproducts.id "
                //        + "  WHERE(vetpaymentcollection.customerid = @CustomerId) AND(vetpaymentcollection.deleted = 0)";

                string query = "SELECT        "
                    + " vetpaymentcollection.id, "
                    + " vetpaymentcollection.date,  "
                    + " vetpaymentcollection.debit, "      
                    + " vetpaymentcollection.credit, "
                    + " vetpaymentcollection.paid,  "
                    + " vetpaymentcollection.totalpaid,  "
                    + " vetpaymentcollection.total, "
                    + " vetpaymentmethods.name as PaymentName, "
                    + " vetpaymentcollection.salebuyid, "
                    + " vetpaymentcollection.remark, "
                    + " ISNULL(vetpaymentcollection.paymetntId, 0) as paymetntId "
                    + " FROM "
                    + " vetpaymentcollection "
                    + " INNER JOIN   vetcustomers ON vetpaymentcollection.customerid = vetcustomers.id "
                    + " INNER JOIN vetpaymentmethods ON vetpaymentcollection.paymetntId = vetpaymentmethods.RecId "
                        + " WHERE (vetpaymentcollection.deleted = 0) and (vetpaymentcollection.customerid = @CustomerId) ";

                var result = _uow.Query<PayChartListDto>(query, new { CustomerId = request.CustomerId }).ToList();
                response.Data = result;
                response.IsSuccessful = true;

            }
            catch (Exception)
            {
                response.IsSuccessful = false;
                
            }
            return response;

        }
    }
}
