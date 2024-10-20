﻿using AutoMapper;
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
using BrewCloud.Vet.Application.Models.Patients.Examinations;

namespace BrewCloud.Vet.Application.Features.Patient.Examination.Queries
{
    public class GetExaminationsQuery : IRequest<Response<List<ExaminationDto>>>
    {
        public Guid? CustomerId { get; set; }
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
                string query = "Select ve.id as Id,vt.firstname as CustomerName,vp.name as PatientName,ve.date,ve.weight,ve.complaintstory,ve.treatmentdescription,ve.symptoms,ve.status, vp.id as PatientId from VetExamination ve \n "
                             + "LEFT OUTER JOIN vetcustomers vt WITH(NOLOCK) ON vt.id=ve.customerid \n"
                             + "LEFT OUTER JOIN vetpatients vp WITH(NOLOCK) ON vp.id=ve.patientid \n"
                             + "where ve.Deleted = 0 ";

                if (request.CustomerId != Guid.Empty)
                {
                    query += " and ve.customerid = @xCustomerId";
                }
                query += " order by ve.Date desc ";

                var _data = _uow.Query<ExaminationDto>(query, new { xCustomerId  = request.CustomerId }).ToList();
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
