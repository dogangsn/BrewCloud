using AutoMapper;
using MassTransit.Futures.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VetSystems.Account.Domain.Contracts;
using VetSystems.Account.Domain.Entities;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Service;

namespace VetSystems.Account.Application.Features.Settings.Commands
{
    public class UpdateTitleDefinationCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
    }

    public class UpdateTitleDefinationCommandHandler : IRequestHandler<UpdateTitleDefinationCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateTitleDefinationCommandHandler> _logger;
        private readonly IRepository<TitleDefinitions> _titleRepository;

        public UpdateTitleDefinationCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<UpdateTitleDefinationCommandHandler> logger, IRepository<TitleDefinitions> titleRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _titleRepository = titleRepository ?? throw new ArgumentNullException(nameof(titleRepository));
        }

        public async Task<Response<bool>> Handle(UpdateTitleDefinationCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                var title = await _titleRepository.GetByIdAsync(request.Id);
                if (title == null)
                {
                    _logger.LogWarning($"Store update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Store update failed", 404);
                }

                title.Name = request.Name;
                title.Remark = request.Remark;
                title.UpdateDate = DateTime.Now;
                title.UpdateUser = _identity.Account.UserName;
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            return response;

        }
    }


}
