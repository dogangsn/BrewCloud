﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Models.Customers;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Customers.Commands
{
    public class CreatePatientCommand : IRequest<Response<string>>
    {
        public Guid CustomerId { get; set; }
        public PatientsDetailsDto PatientDetails { get; set; }

    }

    public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreatePatientCommandHandler> _logger;
        private readonly IRepository<VetPatients> _vetPatientsRepository;
        private readonly IRepository<Vet.Domain.Entities.VetCustomers> _customerRepository;

        public CreatePatientCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreatePatientCommandHandler> logger, IRepository<VetPatients> vetPatientsRepository, IRepository<Domain.Entities.VetCustomers> customerRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _vetPatientsRepository = vetPatientsRepository ?? throw new ArgumentNullException(nameof(vetPatientsRepository));
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }


        public async Task<Response<string>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
                Data = string.Empty
            };
            _uow.CreateTransaction(IsolationLevel.ReadCommitted);

            try
            { 

                VetCustomers customers = await _customerRepository.GetByIdAsync(request.CustomerId);
                if (customers == null)
                {
                    _logger.LogWarning($"Not Foun number: {request.CustomerId}");
                    return Response<string>.Fail("Property update failed", 404);
                }


                VetPatients patients = new()
                {
                    Active = request.PatientDetails.Active,
                    AnimalBreed = request.PatientDetails.AnimalBreed.GetValueOrDefault(),
                    AnimalColor =   request.PatientDetails.AnimalColor.GetValueOrDefault(),
                    AnimalType = Convert.ToInt32(request.PatientDetails.AnimalType),
                    BirthDate = string.IsNullOrEmpty(request.PatientDetails.BirthDate) ? DateTime.Now : Convert.ToDateTime(request.PatientDetails.BirthDate),
                    ChipNumber = request.PatientDetails.ChipNumber,
                    Name = request.PatientDetails.Name,
                    ReportNumber = request.PatientDetails.ReportNumber,
                    Sex = request.PatientDetails.Sex,
                    SpecialNote = request.PatientDetails.SpecialNote,
                    Sterilization = request.PatientDetails.Sterilization,
                    Id = Guid.NewGuid(),
                    CustomerId = request.CustomerId,
                    CreateUsers = _identity.Account.UserName,
                    CreateDate = DateTime.Now,
                    Customers = customers
                    
                };

                //customers.Patients.Add(patients);

                await _vetPatientsRepository.AddAsync(patients);
                
                
                await _uow.SaveChangesAsync(cancellationToken);
                _uow.Commit();

                response.Data = patients.Id.ToString();

            }
            catch (Exception ex)
            {
                _uow.Rollback();
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                _logger.LogError($"Exception: {ex.Message}");
            }
            return response;
        }
    }
}
