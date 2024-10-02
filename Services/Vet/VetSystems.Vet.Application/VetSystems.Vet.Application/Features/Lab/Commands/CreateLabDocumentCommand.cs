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
using VetSystems.Vet.Application.Features.FileManager.Commands;
using VetSystems.Vet.Domain.Entities;
using VetSystems.Vet.Domain.Contracts;

namespace VetSystems.Vet.Application.Features.Lab.Commands
{
    public class CreateLabDocumentCommand : IRequest<Response<bool>>
    {

    }

    public class CreateLabDocumentCommandHandler : IRequestHandler<CreateLabDocumentCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateFileManagerCommandHandler> _logger;
        private readonly IRepository<VetLabDocument> _vetDocumnetRepository;

        public CreateLabDocumentCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateFileManagerCommandHandler> logger, IRepository<VetLabDocument> vetDocumnetRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetDocumnetRepository = vetDocumnetRepository;
        }

        public async Task<Response<bool>> Handle(CreateLabDocumentCommand request, CancellationToken cancellationToken)
        {
            var response = Response<bool>.Success(200);
            try
            {



            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
