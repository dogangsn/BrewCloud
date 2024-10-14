using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
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
    public class CreateFileManagerCommand : IRequest<Response<bool>>
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string Size { get; set; }
    }

    public class CreateFileManagerCommandHandler : IRequestHandler<CreateFileManagerCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateFileManagerCommandHandler> _logger;
        private readonly IRepository<VetDocuments> _vetDocumnetRepository;

        public CreateFileManagerCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<CreateFileManagerCommandHandler> logger, IRepository<VetDocuments> vetDocumnetRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetDocumnetRepository = vetDocumnetRepository;
        }

        public async Task<Response<bool>> Handle(CreateFileManagerCommand request, CancellationToken cancellationToken)
        {

            var response = Response<bool>.Success(200);
            try
            {
                byte[] fileData;
                using (var memoryStream = new MemoryStream())
                {
                    await request.File.CopyToAsync(memoryStream, cancellationToken);
                    fileData = memoryStream.ToArray();
                }
                VetDocuments vetDocument = new()
                {
                    SourceId = Guid.Empty,
                    FileName = request.FileName,
                    FileData = fileData,
                    Size = request.Size,
                    CreateDate = DateTime.Now,
                    CreateUsers = _identity.Account.UserName
                };

                await _vetDocumnetRepository.AddAsync(vetDocument);
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
