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
using VetSystems.Vet.Application.Models.Patients.Examinations;

namespace VetSystems.Vet.Application.Features.Patient.Examination.Queries
{
    public class GetExaminationsQuery : IRequest<Response<List<ExaminationDto>>>
    {
    }

    public class GetExaminationsQueryHandler : IRequestHandler<GetExaminationsQuery, Response<List<ExaminationDto>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetExaminationsQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<ExaminationDto>>> Handle(GetExaminationsQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<ExaminationDto>>();
            try
            {
                string query = "Select ve.id as Id,vt.firstname as CustomerName,vp.name as PatientName,ve.date,ve.weight,ve.complaintstory,ve.treatmentdescription,ve.symptoms from VetExamination ve \n "
                             + "LEFT OUTER JOIN vetcustomers vt WITH(NOLOCK) ON vt.id=ve.customerid \n"
                             + "LEFT OUTER JOIN vetpatients vp WITH(NOLOCK) ON vp.id=ve.patientid \n"
                             + "where ve.Deleted = 0 order by ve.Date desc ";

                var _data = _uow.Query<ExaminationDto>(query).ToList();
                response = new Response<List<ExaminationDto>>
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
