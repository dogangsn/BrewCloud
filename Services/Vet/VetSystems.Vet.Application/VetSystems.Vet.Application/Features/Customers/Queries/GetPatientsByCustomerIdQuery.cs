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
using VetSystems.Vet.Application.Models.Patients;
using VetSystems.Vet.Application.Models.SaleBuy;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Customers.Queries
{
    public class GetPatientsByCustomerIdQuery : IRequest<Response<List<PatientDetailsDto>>>
    {
        public Guid Id { get; set; }
    }
    public class GetPatientsByCustomerIdQueryHandler : IRequestHandler<GetPatientsByCustomerIdQuery, Response<List<PatientDetailsDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<Vet.Domain.Entities.VetSaleBuyOwner> _saleBuyOwnerRepository;
        private readonly IRepository<Vet.Domain.Entities.VetAppointments> _AppointmentRepository;

        public GetPatientsByCustomerIdQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<Domain.Entities.VetSaleBuyOwner> salebuyownerRepository, IRepository<Domain.Entities.VetAppointments> AppointmentRepository)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _saleBuyOwnerRepository = salebuyownerRepository ?? throw new ArgumentNullException(nameof(salebuyownerRepository));
            _AppointmentRepository = AppointmentRepository ?? throw new ArgumentNullException(nameof(AppointmentRepository));
        }


        public async Task<Response<List<PatientDetailsDto>>> Handle(GetPatientsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<PatientDetailsDto>>();
            try
            {

                    var patientQuery = @"select
                                            vp.id,
                                            vp.customerid as CustomerId,
                                            vp.name as Name,
                                            FORMAT(vp.birthdate, 'yyyy-MM-dd') AS BirthDate,
                                            vp.chipnumber as ChipNumber,
                                            vp.sex as Sex,
                                            vp.reportnumber as ReportNumber,
                                            vp.specialnote as SpecialNote,
                                            vp.sterilization as Sterilization,
                                            vp.active as Active,
                                            vp.images as Images,
                                            vat.name as AnimalTypeName,
                                            vat.type as AnimalType,
                                            vabd.breedname as BreedType,
                                            vacd.name as AnimalColor from vetpatients as vp
                                                left outer join vetanimalstype as vat on vp.animaltype = vat.type
                                                left outer join vetanimalbreedsdef as vabd on vp.animalbreed = vabd.RecId
                                                left outer join vetanimalcolorsdef as vacd on vp.animalcolor = vacd.RecId
                                                                          where vp.customerid = @customerId
                                                                            and vp.deleted = 0";
                    List<PatientDetailsDto> patientList = _uow.Query<PatientDetailsDto>(patientQuery, new { customerId = request.Id }).ToList();
                    response.Data = patientList;
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
