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
    public class GetWeightControlsQuery : IRequest<Response<List<VetWeightControl>>>
    {
        public Guid PatientId { get; set; }
    }

    public class GetWeightControlsQueryHandler : IRequestHandler<GetWeightControlsQuery, Response<List<VetWeightControl>>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetWeightControlsQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Response<List<VetWeightControl>>> Handle(GetWeightControlsQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<VetWeightControl>>();
            try
            {

                
                var weightQuery = @"select * from VetWeightControl VW where VW.patientid = @patientId and VW.deleted = 0 order by VW.controldate desc";
                List<VetWeightControl> weightList = _uow.Query<VetWeightControl>(weightQuery, new { patientId = request.PatientId }).ToList();
                response.Data = weightList;
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
