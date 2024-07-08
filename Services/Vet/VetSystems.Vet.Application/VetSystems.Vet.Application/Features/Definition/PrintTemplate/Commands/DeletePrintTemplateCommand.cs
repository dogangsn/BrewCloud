
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Vet.Domain.Entities;
using VetSystems.Shared.Service;
using VetSystems.Vet.Domain.Contracts;
using AutoMapper;
using Microsoft.Extensions.Logging;
using VetSystems.Shared.Dtos;

namespace VetSystems.Vet.Application.Features.Definition.PrintTemplate.Commands
{
    public class DeletePrintTemplateCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeletePrintTemplateCommandHandler : IRequestHandler<DeletePrintTemplateCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeletePrintTemplateCommandHandler> _logger;
        private readonly IRepository<VetPrintTemplate> _vetPrintTemplateRepository;

        public DeletePrintTemplateCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeletePrintTemplateCommandHandler> logger, IRepository<VetPrintTemplate> vetPrintTemplateRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetPrintTemplateRepository = vetPrintTemplateRepository;
        }

        public async Task<Response<bool>> Handle(DeletePrintTemplateCommand request, CancellationToken cancellationToken)
        {
            var response = Response<bool>.Success(200);
            try
            {
                var printTemplate = await _vetPrintTemplateRepository.GetByIdAsync(request.Id);
                if (printTemplate == null)
                {
                    _logger.LogWarning($"printTemplate deleted failed. Id number: {request.Id}");
                    return Response<bool>.Fail("Çıktı Şablonu Bulunamadı", 404);
                }
                printTemplate.Deleted = true;
                printTemplate.DeletedDate = DateTime.Now;
                printTemplate.DeletedUsers = _identity.Account.UserName;

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
