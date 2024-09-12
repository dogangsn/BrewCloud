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
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.FileManager.Commands
{
    public class DownloadFileManagerCommand : IRequest<Response<byte[]>>
    {
        public Guid Id { get; set; }
    }

    public class DownloadFileManagerCommandHandler : IRequestHandler<DownloadFileManagerCommand, Response<byte[]>>
    {

        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DownloadFileManagerCommandHandler> _logger;
        private readonly IRepository<VetDocuments> _vetDocumnetRepository;

        public DownloadFileManagerCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DownloadFileManagerCommandHandler> logger, IRepository<VetDocuments> vetDocumnetRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetDocumnetRepository = vetDocumnetRepository;
        }

        public async Task<Response<byte[]>> Handle(DownloadFileManagerCommand request, CancellationToken cancellationToken)
        {
            var response = Response<byte[]>.Success(200);
            try
            {
                var document = await _vetDocumnetRepository.GetAsync(d => d.Id == request.Id);
                if (document == null)
                {
                    return Response<byte[]>.Fail("Document Not Found", 400);
                }
                var _fileData = document.FirstOrDefault().FileData;
                response.Data = _fileData;
            }
            catch (Exception ex)
            {
                return Response<byte[]>.Fail(ex.Message, 400);
            }

            return response;

             
             
        }
    }
}
