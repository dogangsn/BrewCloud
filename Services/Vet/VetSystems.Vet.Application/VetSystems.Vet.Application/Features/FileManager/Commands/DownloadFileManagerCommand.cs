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
using VetSystems.Vet.Application.Models.FileManager;
using VetSystems.Vet.Domain.Contracts;
using VetSystems.Vet.Domain.Entities;

namespace VetSystems.Vet.Application.Features.FileManager.Commands
{
    public class DownloadFileManagerCommand : IRequest<Response<FileManagerResponse>>
    {
        public Guid Id { get; set; }
    }

    public class DownloadFileManagerCommandHandler : IRequestHandler<DownloadFileManagerCommand, Response<FileManagerResponse>>
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

        public async Task<Response<FileManagerResponse>> Handle(DownloadFileManagerCommand request, CancellationToken cancellationToken)
        {
            var response = Response<FileManagerResponse>.Success(200);
            try
            {
                var document = await _vetDocumnetRepository.GetAsync(d => d.Id == request.Id);
                if (document == null)
                {
                    return Response<FileManagerResponse>.Fail("Document Not Found", 400);
                }
                var _fileData = document.FirstOrDefault().FileData;

                response.Data = new FileManagerResponse();
                response.Data.FileData = _fileData;
                response.Data.FileName = document.FirstOrDefault().FileName;

            }
            catch (Exception ex)
            {
                return Response<FileManagerResponse>.Fail(ex.Message, 400);
            }

            return response;

             
             
        }
    }
}
