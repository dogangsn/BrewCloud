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

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VetSystems.Vet.Application.Features.Patient.Examination.Queries
{
    public class GetSymptomsQuery : IRequest<Response<List<string>>>
    {
    }

    public class GetSymptomsQueryHandler : IRequestHandler<GetSymptomsQuery, Response<List<string>>>
    {
        private readonly IUnitOfWork _uow;

        public GetSymptomsQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Response<List<string>>> Handle(GetSymptomsQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<List<string>>();
            try
            {
                string query = "SELECT symptom FROM vetsymptoms ORDER BY symptom ASC";

                var symptoms =  _uow.Query<string>(query).ToList();

                response.Data = symptoms;
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.Errors = new List<string> { ex.Message };
            }

            return response;
        }
    }
}

