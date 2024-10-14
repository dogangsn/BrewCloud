using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Features.Store.Commands;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteCustomerCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetCustomers> _customersRepository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IRepository<VetPatients> _patientsRepository;
        private readonly IRepository<VetAppointments> _appointmentsRepository;

        public DeleteCustomerCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteCustomerCommandHandler> logger, IRepository<Domain.Entities.VetCustomers> customerRepository, IIdentityRepository identityRepository, IRepository<VetPatients> patientsRepository, IRepository<VetAppointments> appointmentsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _customersRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _identityRepository = identityRepository ?? throw new ArgumentNullException(nameof(identityRepository));
            _patientsRepository = patientsRepository ?? throw new ArgumentNullException(nameof(patientsRepository));
            _appointmentsRepository = appointmentsRepository ?? throw new ArgumentNullException(nameof(appointmentsRepository));
        }

        public async Task<Response<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var customers = await _customersRepository.GetByIdAsync(request.Id);
                if (customers == null)
                {
                    _logger.LogWarning($"Customer update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Customer update failed", 404);
                }

                customers.Deleted = true;
                customers.DeletedDate = DateTime.Now;
                customers.DeletedUsers = _identityRepository.Account.UserName;

                List<VetPatients> patients = (await _patientsRepository.GetAsync(x => x.CustomerId == request.Id)).ToList();
                if (patients != null)
                {
                    foreach (var item in patients)
                    {
                        item.Deleted = true;
                        item.DeletedDate = customers.DeletedDate;
                        item.DeletedUsers = _identityRepository.Account.UserName;
                    }
                }

                List<VetAppointments> appointments = (await _appointmentsRepository.GetAsync(x => x.CustomerId == request.Id)).ToList();
                if (appointments != null)
                {
                    foreach (var item in appointments)
                    {
                        item.Deleted = true;
                        item.DeletedDate = customers.DeletedDate;
                        item.DeletedUsers = _identityRepository.Account.UserName;
                    }
                }


                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;

        }
    }


}
