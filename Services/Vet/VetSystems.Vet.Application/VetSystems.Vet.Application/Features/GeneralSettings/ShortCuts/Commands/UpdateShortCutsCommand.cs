using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Application.Models.Appointments;
using VetSystems.Shared.Enums;
using VetSystems.Vet.Domain.Entities;
using Grpc.Core;

namespace VetSystems.Vet.Application.Features.Appointment.Commands
{
    public class UpdateShortCutsCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
        public VetShortcut shortcut { get; set; }

    }

    public class UpdateShortCutsCommandHandler : IRequestHandler<UpdateShortCutsCommand, Response<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateShortCutsCommandHandler> _logger;
        private readonly IRepository<Vet.Domain.Entities.VetShortcut> _shortCutRepository;

        public UpdateShortCutsCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateShortCutsCommandHandler> logger, IRepository<Vet.Domain.Entities.VetShortcut> shortCutRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _shortCutRepository = shortCutRepository;
        }

        public async Task<Response<string>> Handle(UpdateShortCutsCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<string>
            {
                ResponseType = ResponseType.Ok,
                IsSuccessful = true,
            };
            try
            {
                TimeZoneInfo localTimeZone = TimeZoneInfo.Local;

                var shortCut = await _shortCutRepository.GetByIdAsync(request.Id);
                if (shortCut == null || shortCut.Deleted)
                {
                    response.IsSuccessful = false;
                    response.ResponseType = ResponseType.Error;
                    response.Data = "Kayıt Bulunamadı.";
                    return response;
                }

                shortCut.icon = request.shortcut.icon;
                shortCut.description = request.shortcut.description;
                shortCut.useRouter = request.shortcut.useRouter;
                shortCut.link = request.shortcut.link;
                shortCut.Label=request.shortcut.Label;
                shortCut.UpdateDate = DateTime.Now;
                shortCut.UpdateUsers = _identity.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);
             
            }
            catch (Exception ex)
            {
                response = Response<string>.Fail("ShortCut kayıt edilemedi: " + ex.Message, 501);
                _logger.LogError("ShortCut not created: " + ex.Message);
            }
            return response;
        }
    }
}
