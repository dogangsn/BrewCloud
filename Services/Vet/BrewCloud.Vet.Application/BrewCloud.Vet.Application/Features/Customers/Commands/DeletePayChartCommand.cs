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
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Customers.Commands
{
    public class DeletePayChartCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public Guid AppointmentId { get; set; }
    }

    public class DeletePayChartCommandHandler : IRequestHandler<DeletePayChartCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePayChartCommandHandler> _logger;
        private readonly IRepository<VetPaymentCollection> _vetPaymentCollectionRepository;
        private readonly IRepository<VetSaleBuyOwner> _vetSaleBuyOwnerRepository;
        private readonly IRepository<VetAppointments> _vetAppointmentsRepository;

        public DeletePayChartCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeletePayChartCommandHandler> logger, IRepository<VetPaymentCollection> vetPaymentCollectionRepository, IRepository<VetSaleBuyOwner> vetSaleBuyOwnerRepository, IRepository<VetAppointments> vetAppointmentsRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _vetPaymentCollectionRepository = vetPaymentCollectionRepository ?? throw new ArgumentNullException(nameof(vetPaymentCollectionRepository));
            _vetSaleBuyOwnerRepository = vetSaleBuyOwnerRepository ?? throw new ArgumentNullException(nameof(vetSaleBuyOwnerRepository));
            _vetAppointmentsRepository = vetAppointmentsRepository ?? throw new ArgumentNullException(nameof(vetAppointmentsRepository)); ;
        }

        public async Task<Response<bool>> Handle(DeletePayChartCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var _paymentCollection = await _vetPaymentCollectionRepository.GetByIdAsync(request.Id);
                if (_paymentCollection != null)
                {
                    _paymentCollection.Deleted = true;
                    _paymentCollection.DeletedDate = DateTime.Now;
                    _paymentCollection.DeletedUsers = _identity.Account.UserName;
 
                    var _appointment = await _vetAppointmentsRepository.GetByIdAsync(request.AppointmentId);
                    if (_appointment != null)
                        _appointment.IsPaymentReceived = false;


                    await _uow.SaveChangesAsync(cancellationToken);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
