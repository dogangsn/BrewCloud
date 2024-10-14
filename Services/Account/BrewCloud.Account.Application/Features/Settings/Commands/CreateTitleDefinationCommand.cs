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
using BrewCloud.Account.Domain.Contracts;
using BrewCloud.Account.Domain.Entities;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Service;

namespace BrewCloud.Account.Application.Features.Settings.Commands
{
    public class CreateTitleDefinationCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; } = string.Empty;
        public string Remark { get; set; } = string.Empty;
        public bool? IsAppointmentShow { get; set; } = false;
    }
    public class CreateTitleDefinationCommandHandler : IRequestHandler<CreateTitleDefinationCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateTitleDefinationCommandHandler> _logger;
        private readonly IRepository<TitleDefinitions> _titleRepository;

        public CreateTitleDefinationCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateTitleDefinationCommandHandler> logger, IRepository<TitleDefinitions> titleRepository)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _identity = identity ?? throw new ArgumentNullException(nameof(identity));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _titleRepository = titleRepository ?? throw new ArgumentNullException(nameof(titleRepository));
        }

        public async Task<Response<bool>> Handle(CreateTitleDefinationCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                TitleDefinitions title = new()
                {
                    Id = Guid.NewGuid(),
                    Name = request.Name,
                    Remark = request.Remark,
                    IsAppointmentShow = request.IsAppointmentShow.GetValueOrDefault(),
                    CreateDate = DateTime.Now,
                    CreateUser = _identity.Account.UserName
                };
                await _titleRepository.AddAsync(title);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }

            return response;

            throw new NotImplementedException();
        }
    }
}
