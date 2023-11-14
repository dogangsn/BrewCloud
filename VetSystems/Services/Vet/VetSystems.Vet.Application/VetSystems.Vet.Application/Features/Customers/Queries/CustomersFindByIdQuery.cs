using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Application.Models.Patients;
using VetSystems.Vet.Application.Models.SaleBuy;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Customers.Queries
{
    public class CustomersFindByIdQuery : IRequest<Response<CustomerDetailsDto>>
    {
        public Guid Id { get; set; }
    }

    public class CustomersFindByIdQueryHandler : IRequestHandler<CustomersFindByIdQuery, Response<CustomerDetailsDto>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public CustomersFindByIdQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<Response<CustomerDetailsDto>> Handle(CustomersFindByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<CustomerDetailsDto>();
            try
            {
                string query = @"select 
                                    vc.id, 
                                    vc.firstname, 
                                    vc.lastname, 
                                    vc.phonenumber,
                                    vc.phonenumber2, 
                                    vc.email, 
                                    vc.taxoffice, 
                                    vc.vkntcno, 
                                    vc.customergroup,
                                    vc.note,
                                    vc.discountrate,
                                    vc.isemail, 
                                    vc.isphone,
                                    vc.adressid,
                                    FORMAT(vc.createdate, 'yyyy-MM-dd') AS createdate
                                    from vetcustomers as vc where vc.deleted = 0 and id = @id";

                CustomerDetailsDto? customerDetail = _uow.Query<CustomerDetailsDto>(query, new { id = request.Id }).FirstOrDefault();

                

                if (customerDetail != null)
                {

                    if (customerDetail.adressid != null)
                    {
                        string addressQuery = @"select province, district, longadress from vetadress where id = @id";

                        var address = _uow.Query<VetAdress>(addressQuery, new { id = customerDetail.adressid }).FirstOrDefault();

                        customerDetail.city = address.Province;
                        customerDetail.district = address.District;
                        customerDetail.longadress = address.LongAdress;
                    }

                    var patientQuery = @"select
                                            vp.id as RecId,
                                            vp.customerid as CustomerId,
                                            vp.name as Name,
                                            vp.birthdate as Birthdate,
                                            vp.chipnumber as ChipNumber,
                                            vp.sex as Sex,
                                            vat.name as AnimalType,
                                            vabd.breedname as BreedType,
                                            vacd.name as AnimalColor from vetpatients as vp
                                                left outer join vetanimalstype as vat on vp.animaltype = vat.type
                                                left outer join vetanimalbreedsdef as vabd on vp.animalbreed = vabd.RecId
                                                left outer join vetanimalcolorsdef as vacd on vp.animalcolor = vacd.RecId
                                                                          where vp.customerid = @customerId
                                                                            and vp.deleted = 0";


                    List<PatientDetailsDto> patientList = _uow.Query<PatientDetailsDto>(patientQuery, new { customerId = customerDetail.id }).ToList();

                    customerDetail.PatientDetails = patientList;

                    response = new Response<CustomerDetailsDto>
                    {
                        IsSuccessful = true,
                        ResponseType = ResponseType.Ok,
                        Data = customerDetail
                    };
                }
                else
                {
                    response = new Response<CustomerDetailsDto>
                    {
                        IsSuccessful = false,
                        ResponseType = ResponseType.Error,
                        Data = null
                    };
                }

            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                response.Data = null;
            }

             return response;
        }
    }
}
