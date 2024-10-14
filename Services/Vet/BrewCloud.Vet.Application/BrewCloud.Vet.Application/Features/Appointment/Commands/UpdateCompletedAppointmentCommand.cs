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

namespace BrewCloud.Vet.Application.Features.Appointment.Commands
{
    public class UpdateCompletedAppointmentCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class UpdateCompletedAppointmentCommandHandler : IRequestHandler<UpdateCompletedAppointmentCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCompletedAppointmentCommandHandler> _logger;
        private readonly IRepository<VetAppointments> _appointmentRepository;
        private readonly IRepository<VetPaymentCollection> _paymentCollectionRepository;

        public UpdateCompletedAppointmentCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateCompletedAppointmentCommandHandler> logger, IRepository<VetAppointments> appointmentRepository, IRepository<VetPaymentCollection> paymentCollectionRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _appointmentRepository = appointmentRepository;
            _paymentCollectionRepository = paymentCollectionRepository;
        }

        public async Task<Response<string>> Handle(UpdateCompletedAppointmentCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>
            {
                ResponseType = ResponseType.Ok,
                Data = string.Empty,
                IsSuccessful = true
            };
            try
            {
                VetAppointments appointment = await _appointmentRepository.GetByIdAsync(request.Id);
                if (appointment == null)
                {
                    _logger.LogWarning($"Not Foun number: {request.Id}");
                    return Response<string>.Fail("Appointments update failed", 404);
                }
                if (appointment.IsPaymentReceived.GetValueOrDefault())
                {
                    return Response<string>.Fail("Tahsilatı Yapılmış İşlemlerde Değişiklik Yapılamaz.", 404);
                }
                appointment.IsCompleted = request.IsCompleted;

                if (appointment.IsCompleted.GetValueOrDefault())
                {
                    VetPaymentCollection paymentCollection = new()
                    {
                        Id = Guid.NewGuid(),
                        CollectionId = appointment.Id,
                        CustomerId = appointment.CustomerId,
                        Date = DateTime.Today,
                        Remark = "",
                        CreateDate = DateTime.Now,
                        CreateUsers = _identity.Account.UserName,
                        Credit = 0,
                        Debit = 0,
                        Paid = 0,
                        Total = 0,
                        TotalPaid = 0,
                        SaleBuyId = Guid.Empty
                    };
                    await _paymentCollectionRepository.AddAsync(paymentCollection);
                }
                else
                {
                    var _collection = (await _paymentCollectionRepository.GetAsync(x => !x.Deleted && x.CollectionId == appointment.Id)).FirstOrDefault();
                    if (_collection != null)
                    {
                        _collection.Deleted = true;
                        _collection.DeletedDate = DateTime.Today;
                        _collection.DeletedUsers = _identity.Account.UserName; 
                    }
                }

                await _uow.SaveChangesAsync(cancellationToken);
                response.IsSuccessful = true;
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
