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

namespace BrewCloud.Vet.Application.Features.Customers.Queries
{
    public class CustomersListQuery : IRequest<Response<List<CustomersDto>>>
    {
        public bool IsArchive { get; set; }
    }

    public class CustomersListQueryHandler : IRequestHandler<CustomersListQuery, Response<List<CustomersDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CustomersListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<CustomersDto>>> Handle(CustomersListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<CustomersDto>>();
            try
            {
                #region Old
                //string query = "SELECT Vc.id, Vc.firstname, Vc.lastname, Vc.phonenumber, Vc.phonenumber2, \r\n " +
                //    "      Vc.email, Vc.taxoffice, Vc.vkntcno, Vc.customergroup, Vc.note, \r\n" +
                //    "       Vc.discountrate, Vc.isemail, Vc.isphone, Vc.adressid, Vc.createdate, \r\n " +
                //    "      Vc.updatedate, Vc.deleteddate, Vc.deleted, Vc.deletedusers, \r\n " +
                //    "      Vc.updateusers, Vc.createusers, COUNT(Vp.id) AS PetCount\r\n" +
                //    "      FROM VetCustomers Vc \r\n" +
                //    "      LEFT OUTER JOIN VetPatients Vp ON Vc.id = Vp.customerid and Vp.deleted = 0 \r\n" +
                //    "      WHERE Vc.Deleted = 0 \r\n" +
                //    "      GROUP BY Vc.id, Vc.firstname, Vc.lastname, Vc.phonenumber, Vc.phonenumber2, \r\n" +
                //    "      Vc.email, Vc.taxoffice, Vc.vkntcno, Vc.customergroup, Vc.note, \r\n" +
                //    "      Vc.discountrate, Vc.isemail, Vc.isphone, Vc.adressid, Vc.createdate, \r\n" +
                //    "      Vc.updatedate, Vc.deleteddate, Vc.deleted, Vc.deletedusers, \r\n" +
                //    "      Vc.updateusers, Vc.createusers\r\nORDER BY Vc.CreateDate; "; 
                #endregion

                string query = "SELECT "
                    + "     '#' +  CAST(Vc.recid AS VARCHAR(MAX)) as recid, "
                    + "     Vc.id, "
                    + "     Vc.firstname, "
                    + "     Vc.lastname, "
                    + "     Vc.phonenumber, "
                    + "     Vc.phonenumber2, "
                    + "     Vc.email, "
                    + "     Vc.taxoffice, "
                    + "     Vc.vkntcno, "
                    + "     Vc.customergroup, "
                    + "     Vc.note, "
                    + "     Vc.discountrate, "
                    + "     Vc.isemail, "
                    + "     Vc.isphone, "
                    + "     Vc.adressid, "
                    + "     Vc.createdate, "
                    + "     Vc.updatedate, "
                    + "     Vc.deleteddate, "
                    + "     Vc.deleted, "
                    + "     Vc.deletedusers, "
                    + "     Vc.updateusers, "
                    + "     Vc.createusers,"
                    + "     Vc.IsArchive, "
                    + "     (SELECT COUNT(*) FROM VetPatients Vp WHERE Vp.customerid = Vc.id AND Vp.deleted = 0) AS PetCount,"
                    + "     ISNULL((SELECT SUM(vsb.total) FROM VetSaleBuyOwner vsb WHERE vsb.customerid = Vc.id AND vsb.deleted = 0), 0) "
                    + "     - ISNULL((SELECT SUM(vpc.total) FROM VetPaymentCollection vpc WHERE vpc.customerid = Vc.id AND vpc.deleted = 0), 0) "
                    + "     AS Balance"
                    + " FROM "
                    + "     VetCustomers Vc "
                    + " WHERE "
                    + "     Vc.Deleted = 0  and ISNULL(Vc.IsArchive, 0) = @xArchive "
                    + " ORDER BY "
                    + "     Vc.CreateDate;";

                var _data = _uow.Query<CustomersDto>(query, new { xArchive = request.IsArchive }).ToList();
                response = new Response<List<CustomersDto>>
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
