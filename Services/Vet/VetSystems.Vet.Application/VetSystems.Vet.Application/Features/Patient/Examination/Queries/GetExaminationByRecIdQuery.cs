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
using VetSystems.Vet.Domain.Entities;
using VetSystems.Vet.Application.Models.Patients;

namespace VetSystems.Vet.Application.Features.Patient.Examination.Queries
{
    public class GetExaminationByRecIdQuery : IRequest<Response<VetExamination>>
    {
        public Guid Id { get; set; }
    }

    public class GetExaminationByRecIdQueryHandler : IRequestHandler<GetExaminationByRecIdQuery, Response<VetExamination>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetExaminationByRecIdQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<VetExamination>> Handle(GetExaminationByRecIdQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<VetExamination>();
            try
            {
                string query = "Select * from VetExamination ve \n " +
                             "where ve.Deleted = 0 " +
                             "and ve.id = @Id " +
                             "order by ve.CreateDate ";

                var _data = _uow.Query<VetExamination>(query, new { Id = request.Id }).FirstOrDefault();
                response = new Response<VetExamination>
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
