using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Application.Models.Customers;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;
using Microsoft.Extensions.Logging;
using AutoMapper;

namespace VetSystems.Vet.Application.Features.Customers.Commands
{
    public class UpdatePatientCommand : IRequest<Response<bool>>
    {
        public Guid CustomerId { get; set; }
        public PatientsDetailsDto PatientDetails { get; set; }
    }

    public class UpdatePatientCommandHandler : IRequestHandler<UpdatePatientCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdatePatientCommandHandler> _logger;
        private readonly IRepository<VetPatients> _vetPatientsRepository;
        private readonly IRepository<Vet.Domain.Entities.VetCustomers> _customerRepository;

        public UpdatePatientCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdatePatientCommandHandler> logger, IRepository<VetPatients> vetPatientsRepository, IRepository<VetCustomers> customerRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetPatientsRepository = vetPatientsRepository;
            _customerRepository = customerRepository;
        }

        public async Task<Response<bool>> Handle(UpdatePatientCommand request, CancellationToken cancellationToken)
        {

            var response = Response<bool>.Success(200);
            try
            {

                VetPatients patients = await _vetPatientsRepository.GetByIdAsync(request.PatientDetails.Id);
                if (patients == null)
                {
                    return Response<bool>.Fail("Not Found Patients", 404);
                }

                patients.AnimalBreed = request.PatientDetails.AnimalBreed.GetValueOrDefault();
                patients.AnimalColor = request.PatientDetails.AnimalColor.GetValueOrDefault();
                patients.AnimalType = Convert.ToInt32(request.PatientDetails.AnimalType);
                patients.BirthDate = string.IsNullOrEmpty(request.PatientDetails.BirthDate) ? DateTime.Now : Convert.ToDateTime(request.PatientDetails.BirthDate);
                patients.ChipNumber = request.PatientDetails.ChipNumber;
                patients.Name = request.PatientDetails.Name;
                patients.ReportNumber = request.PatientDetails.ReportNumber;
                patients.Sex = request.PatientDetails.Sex;
                patients.SpecialNote = request.PatientDetails.SpecialNote;
                patients.Sterilization = request.PatientDetails.Sterilization;
                patients.UpdateDate = DateTime.Now;
                patients.UpdateUsers = _identity.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 400);
            }
            return response;


        }
    }
}
