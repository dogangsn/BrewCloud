using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Patients;
using VetSystems.Vet.Application.Models.Patients.PatientList;
using VetSystems.Vet.Application.Models.SaleBuy;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.Patient.PatientList.Queries
{
    public class GetPatientByIdQuery : IRequest<Response<PatientDetailsDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetPatientByIdQueryHandler : IRequestHandler<GetPatientByIdQuery, Response<PatientDetailsDto>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetPatientByIdQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<PatientDetailsDto>> Handle(GetPatientByIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<PatientDetailsDto>();
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
                                                                          where vp.id = @id
                                                                            and vp.deleted = 0";
                PatientDetailsDto patientList = _uow.Query<PatientDetailsDto>(patientQuery, new { id = request.Id }).FirstOrDefault();
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
