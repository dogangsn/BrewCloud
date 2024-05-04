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
    public class GetPatientListQuery : IRequest<Response<List<PatientOwnerListDto>>>
    {
    }

    public class GetPatientListQueryHandler : IRequestHandler<GetPatientListQuery, Response<List<PatientOwnerListDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetPatientListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<PatientOwnerListDto>>> Handle(GetPatientListQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<PatientOwnerListDto>>();
            try
            {
                string query = "SELECT        vetpatients.id, vetpatients.customerid, vetpatients.name, vetpatients.birthdate, vetpatients.chipnumber, vetpatients.sex, vetpatients.animaltype, vetpatients.animalbreed, vetpatients.animalcolor, vetpatients.reportnumber,  "
                    + "                          vetpatients.specialnote, vetpatients.sterilization, vetpatients.images, vetpatients.active, vetpatients.customersid, vetpatients.createdate, vetpatients.updatedate, vetpatients.deleteddate, vetpatients.deleted,  "
                    + "                          vetpatients.deletedusers, vetpatients.updateusers, vetpatients.createusers, (vetcustomers.firstname + ' ' + vetcustomers.lastname) as CustomerFirsLastName, vetcustomers.phonenumber, vetanimalstype.name as AnimalTypeName "
                    + " FROM            vetpatients INNER JOIN "
                    + "                          vetcustomers ON vetpatients.customerid = vetcustomers.id and vetcustomers.deleted = 0 "
                    + " INNER JOIN vetanimalstype ON vetpatients.animaltype = vetanimalstype.type "
                    + " where vetpatients.deleted = 0";

                var _data = _uow.Query<PatientOwnerListDto>(query).ToList();
                response = new Response<List<PatientOwnerListDto>>
                {
                    Data = _data,
                    IsSuccessful = true,
                };

            }
            catch (Exception)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
