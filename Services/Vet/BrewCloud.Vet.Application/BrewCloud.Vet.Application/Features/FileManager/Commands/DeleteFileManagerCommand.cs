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

namespace BrewCloud.Vet.Application.Features.FileManager.Commands
{
    public class DeleteFileManagerCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteFileManagerCommandHandler : IRequestHandler<DeleteFileManagerCommand, Response<bool>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteFileManagerCommandHandler> _logger;
        private readonly IRepository<VetDocuments> _vetDocumentsRepository;

        public DeleteFileManagerCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteFileManagerCommandHandler> logger, IRepository<VetDocuments> vetDocumnetRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetDocumentsRepository = vetDocumnetRepository;
        }

        public async Task<Response<bool>> Handle(DeleteFileManagerCommand request, CancellationToken cancellationToken)
        {
            var response = Response<bool>.Success(200);
            try
            {
                var _documents = await _vetDocumentsRepository.GetByIdAsync(request.Id);
                if (_documents == null)
                {
                    _logger.LogWarning($"File Manager Not deleted: Id number: {request.Id}");
                    return Response<bool>.Fail("File Manager update failed", 404);
                }
                _documents.Deleted = true;
                _documents.DeletedDate = DateTime.Now;
                _documents.DeletedUsers = _identity.Account.UserName;

                await _uow.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                return Response<bool>.Fail(ex.Message, 400);
            }
            return response;

        }
    }
}
