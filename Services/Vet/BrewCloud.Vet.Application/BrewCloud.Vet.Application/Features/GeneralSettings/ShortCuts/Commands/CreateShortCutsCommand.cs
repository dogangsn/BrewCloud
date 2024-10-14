using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Enums;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.Features.VaccineCalendar.Commands;
using BrewCloud.Vet.Application.Models.Appointments;
using BrewCloud.Vet.Application.Models.Customers;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Appointment.Commands
{
    public class CreateShortCutsCommand : IRequest<Response<bool>>
    {
        public VetShortcut shortcut { get; set; } 
    }

    public class CreateShortCutsHandler : IRequestHandler<CreateShortCutsCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateShortCutsHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetShortcut> _shortCutRepository;

        public CreateShortCutsHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateShortCutsHandler> logger,IRepository<Vet.Domain.Entities.VetShortcut> shortCutRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _shortCutRepository = shortCutRepository ?? throw new ArgumentNullException(nameof(shortCutRepository));
        }

        public async Task<Response<bool>> Handle(CreateShortCutsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };

            try
            {

                var shortCut = _shortCutRepository.Get(p => p.link == request.shortcut.link).FirstOrDefault();

                if (shortCut != null) {
                    response.IsSuccessful = false;
                    response.ResponseType = ResponseType.Error;
                    return response;
                }
                await _shortCutRepository.AddAsync(request.shortcut);
                
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
                response.ResponseType = ResponseType.Error;
                _logger.LogError($"Exception: {ex.Message}");
            }
            return response;

        }
    }
}
