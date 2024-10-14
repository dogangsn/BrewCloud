using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Vet.Domain.Entities;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Shared.Service;
using Microsoft.Extensions.Logging;
using AutoMapper;
using BrewCloud.Vet.Application.Features.Accounting.Commands;

namespace BrewCloud.Vet.Application.Features.PetHotels.Accomodation.Commands
{
    public class UpdateCheckOutCommand : IRequest<Response<bool>>
    {
        public Guid AccomodationId { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? PatientsId { get; set; }
        public Guid RoomId { get; set; }
        public DateTime CheckinDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal AccomodationAmount { get; set; }
        public decimal CollectionAmount { get; set; }
        public int PaymentId { get; set; }
    }

    public class UpdateCheckOutCommandHandler : IRequestHandler<UpdateCheckOutCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCheckOutCommandHandler> _logger;
        private readonly IRepository<VetAccomodation> _vetAccomodationRepository;
        private readonly IMediator _mediator;
        private readonly IRepository<VetAccomodationCheckOuts> _vetAccomodationCheckOutsRepository;


        public UpdateCheckOutCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateCheckOutCommandHandler> logger, IRepository<VetAccomodation> vetAccomodationRepository, IMediator mediator, IRepository<VetAccomodationCheckOuts> vetAccomodationCheckOutsRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetAccomodationRepository = vetAccomodationRepository;
            _mediator = mediator;
            _vetAccomodationCheckOutsRepository = vetAccomodationCheckOutsRepository;
        }

        public async Task<Response<bool>> Handle(UpdateCheckOutCommand request, CancellationToken cancellationToken)
        {

            var response = Response<bool>.Success(200);
            try
            {
                var _accomodation = await _vetAccomodationRepository.GetByIdAsync(request.AccomodationId);
                if (_accomodation != null)
                {

                    VetAccomodationCheckOuts vetAccomodationCheck = new()
                    {
                        Id = Guid.NewGuid(),
                        AccomodationAmount = request.AccomodationAmount,
                        AccomodationId = request.AccomodationId,
                        CheckinDate = request.CheckinDate,
                        CheckOutDate = request.CheckOutDate,
                        CollectionAmount = request.CollectionAmount,
                        CreateDate = DateTime.Now,
                        CreateUsers = _identity.Account.UserName,
                        PaymentId = request.PaymentId,
                    };
                    await _vetAccomodationCheckOutsRepository.AddAsync(vetAccomodationCheck);

                    _accomodation.IsLogOut = true;
                    _accomodation.AccomodationcheckOutId = vetAccomodationCheck.Id;
                    await _uow.SaveChangesAsync(cancellationToken);

                    var reqsale = new CreateSaleCommand()
                    {
                        CustomerId = _accomodation.CustomerId.GetValueOrDefault(),
                        Date = vetAccomodationCheck.CreateDate,
                        Remark = "",
                        Trans = new List<Models.Accounting.SaleTransRequestDto>(),
                        IsPrice = true,
                        Price = request.CollectionAmount,
                        IsExaminations = false,
                        IsAccomodation = true,
                        AccomodationId = _accomodation.Id
                    };
                    var resp = await _mediator.Send(reqsale);
                    if (resp.IsSuccessful)
                    {
                        var req = new CreateSaleCollectionCommand
                        {
                            CustomerId = _accomodation.CustomerId.GetValueOrDefault(),
                            Date = vetAccomodationCheck.CreateDate,
                            PaymentId = request.PaymentId,
                            Remark = "Konaklama Satışı",
                            Amount = vetAccomodationCheck.CollectionAmount,
                            SaleOwnerId = resp.Data.Id,
                        };
                        await _mediator.Send(req);
                    }

                }
                else
                {
                    return Response<bool>.Fail("Not Accomodation", 404);
                }
            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 400);
            }
            return response;

        }


    }
}
