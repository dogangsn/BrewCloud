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

namespace BrewCloud.Vet.Application.Features.Definition.AnimalColorsDef.Commands
{
    public class CreateAnimalColorsDefCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class CreateAnimalColorsDefCommandHandler : IRequestHandler<CreateAnimalColorsDefCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateAnimalColorsDefCommandHandler> _logger;
        private readonly IRepository<VetAnimalColorsDef> _animalColorDefRepository;

        public CreateAnimalColorsDefCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateAnimalColorsDefCommandHandler> logger, IRepository<VetAnimalColorsDef> animalColorDefRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _animalColorDefRepository = animalColorDefRepository;
        }

        public async Task<Response<bool>> Handle(CreateAnimalColorsDefCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>
            {
                ResponseType = ResponseType.Ok,
                Data = true,
                IsSuccessful = true
            };
            try
            {
                Vet.Domain.Entities.VetAnimalColorsDef animalColorsDef = new()
                {
                    Name = request.Name,
                    CreateDate = DateTime.Now,
                };
                await _animalColorDefRepository.AddAsync(animalColorsDef);
                await _uow.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                response.IsSuccessful = false;
            }
            return response;
        }
    }
}
