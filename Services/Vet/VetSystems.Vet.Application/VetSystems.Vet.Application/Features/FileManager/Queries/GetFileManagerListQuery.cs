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
    public class GetFileManagerListQuery : IRequest<Response<FileItemsDto>>
    {

    }

    public class GetFileManagerListQueryHandler : IRequestHandler<GetFileManagerListQuery, Response<FileItemsDto>>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IRepository<VetDocuments> _vetDocumentsRepository;

        public GetFileManagerListQueryHandler(IIdentityRepository identityRepository, IUnitOfWork uow, IMapper mapper, IRepository<VetDocuments> vetDocumentsRepository)
        {
            _identityRepository = identityRepository;
            _uow = uow;
            _mapper = mapper;
            _vetDocumentsRepository = vetDocumentsRepository;
        }

        public async Task<Response<FileItemsDto>> Handle(GetFileManagerListQuery request, CancellationToken cancellationToken)
        {
            var response = Response<FileItemsDto>.Success(200);
            response.Data = new FileItemsDto();
            try
            {
                List<VetDocuments> _documents = (await _vetDocumentsRepository.GetAsync(x=>x.Deleted == false)).ToList();
                foreach (var item in _documents)
                {
                    Item _file = new Item();
                    _file.Id = item.Id;
                    _file.Name = item.FileName;
                    _file.CreatedBy = item.CreateUsers;
                    _file.CreatedAt = item.CreateDate;
                    _file.Type = ".txt";
                    _file.Size = "0Mb";
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
