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
using BrewCloud.Vet.Application.Features.Definition.StockTracking.Commands;
using BrewCloud.Vet.Domain.Contracts;
using BrewCloud.Vet.Domain.Entities;

namespace BrewCloud.Vet.Application.Features.Definition.SmsTemplate.Commands
{
    public class DeleteSmsTemplateCommand : IRequest<Response<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteSmsTemplateCommandHandler : IRequestHandler<DeleteSmsTemplateCommand, Response<bool>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityRepository _identity;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteSmsTemplateCommandHandler> _logger;
        private readonly IRepository<VetSmsTemplate> _vetSmsTemplateRepository;

        public DeleteSmsTemplateCommandHandler(IUnitOfWork uow, IIdentityRepository identity, IMapper mapper, ILogger<DeleteSmsTemplateCommandHandler> logger, IRepository<VetSmsTemplate> vetSmsTemplateRepository)
        {
            _uow = uow;
            _identity = identity;
            _mapper = mapper;
            _logger = logger;
            _vetSmsTemplateRepository = vetSmsTemplateRepository;
        }

        public async Task<Response<bool>> Handle(DeleteSmsTemplateCommand request, CancellationToken cancellationToken)
        {
            var response = Response<bool>.Success(200);
            try
            {
                var smstemplate = await _vetSmsTemplateRepository.GetByIdAsync(request.Id);
                if (smstemplate == null)
                {
                    _logger.LogWarning($"stocktracking update failed. Id number: {request.Id}");
                    return Response<bool>.Fail("stocktracking update failed", 404);
                }
                smstemplate.Deleted = true;
                smstemplate.DeletedDate = DateTime.Now;
                smstemplate.DeletedUsers = _identity.Account.UserName;

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
