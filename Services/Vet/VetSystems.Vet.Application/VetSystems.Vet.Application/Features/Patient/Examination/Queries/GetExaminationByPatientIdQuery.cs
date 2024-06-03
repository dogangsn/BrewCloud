﻿using AutoMapper;
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
using VetSystems.Vet.Domain.Entities;
using VetSystems.Vet.Application.Models.Patients;

namespace VetSystems.Vet.Application.Features.Patient.Examination.Queries
{
    public class GetExaminationByPatientIdQuery : IRequest<Response<List<VetExamination>>>
    {
        public Guid Id { get; set; }
    }

    public class GetExaminationByPatientIdQueryHandler : IRequestHandler<GetExaminationByPatientIdQuery, Response<List<VetExamination>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetExaminationByPatientIdQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<VetExamination>>> Handle(GetExaminationByPatientIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<VetExamination>>();
            try
            {
                string query = "Select * from VetExamination ve \n " +
                             "where ve.Deleted = 0 " +
                             "and ve.patientid = @Id " +
                             "order by ve.CreateDate ";

                var _data = _uow.Query<VetExamination>(query, new { Id = request.Id }).ToList();
                response = new Response<List<VetExamination>>
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
