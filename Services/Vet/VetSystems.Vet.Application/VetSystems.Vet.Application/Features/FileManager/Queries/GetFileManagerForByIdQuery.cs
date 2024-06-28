using AutoMapper;
using MediatR;
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

namespace VetSystems.Vet.Application.Features.FileManager.Queries
{
    public class GetFileManagerForByIdQuery : IRequest<Response<FileItemsDto>>
    {
        public Guid Id { get; set; }
    }

    public class GetFileManagerForByIdHandler : IRequestHandler<GetFileManagerForByIdQuery, Response<FileItemsDto>>
    {

        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<VetDocuments> _vetDocumentsRepository;
        public GetFileManagerForByIdHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<VetDocuments> vetDocumentsRepository)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _vetDocumentsRepository = vetDocumentsRepository;
        }

        public async Task<Response<FileItemsDto>> Handle(GetFileManagerForByIdQuery request, CancellationToken cancellationToken)
        {

            var response = Response<FileItemsDto>.Success(200);
            response.Data = new FileItemsDto();
            try
            {
                var _documents = await _vetDocumentsRepository.GetByIdAsync(request.Id);
                if (_documents != null)
                {
                    Item _file = new Item();
                    _file.Id = _documents.Id;
                    _file.Name = _documents.FileName;
                    _file.CreatedAt = _documents.CreateUsers;
                    _file.Type = ".txt";
                    _file.Size = "0Mb";
                    _file.Contents = "";
                    _file.CreatedBy = "";
                    _file.Description = "";
                    _file.FolderId = Guid.Empty;
                    response.Data.Files.Add(_file);
                }

            }
            catch (Exception ex)
            {
                return Response<FileItemsDto>.Fail(ex.Message, 400);
            }
            return response;


        }
    }
}
