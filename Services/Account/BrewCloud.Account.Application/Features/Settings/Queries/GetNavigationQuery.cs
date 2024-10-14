using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BrewCloud.Account.Application.Features.Settings.Commands;
using BrewCloud.Account.Application.Models.Settings;
using BrewCloud.Account.Domain.Contracts;
using BrewCloud.Shared.Dtos;
using BrewCloud.Shared.Dtos.Settings;
using BrewCloud.Shared.Service;
using BrewCloud.Shared.Service.Nav;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BrewCloud.Account.Application.Features.Settings.Queries
{
    public class GetNavigationQuery : IRequest<Shared.Dtos.Response<Navigation>>
    {
        public bool All { get; set; }
        public Guid? RoleId { get; set; }
    }

    public class GetNavigationQueryHandler : IRequestHandler<GetNavigationQuery, Shared.Dtos.Response<Navigation>>
    {
        private readonly IIdentityRepository _identity;
        private readonly IUnitOfWork _uow;
        private readonly ModuleService _moduleService;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly ILogger<CreateRoleSettingCommandHandler> _logger;

        public GetNavigationQueryHandler(IIdentityRepository identity, IUnitOfWork uow, ModuleService moduleService, ISendEndpointProvider sendEndpointProvider, ILogger<CreateRoleSettingCommandHandler> logger)
        {
            _identity = identity;
            _uow = uow;
            _moduleService = moduleService;
            _sendEndpointProvider = sendEndpointProvider;
            _logger = logger;
        }

        public async Task<Shared.Dtos.Response<Navigation>> Handle(GetNavigationQuery request, CancellationToken cancellationToken)
        {

            var response = new Shared.Dtos.Response<Navigation>
            {
                Data = new Navigation(),
                ResponseType = ResponseType.Ok
            };
            var account = _identity.Account;

            try
            {

                //var endpointHub = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:start"));
                //await endpointHub.Send("Başladı");

                _logger.LogInformation(account.ConnectionDb);

                var modules = _moduleService.GetModule();
                _logger.LogInformation("Module Sayısı" + modules.Count.ToString());


                _logger.LogInformation("ConnecDb" + _uow);
                _logger.LogInformation(request.All.ToString());
                _logger.LogInformation(request.RoleId.ToString());
                _logger.LogInformation(account.RoleId.ToString());


                string query = @"select id,action,target,roleSettingid  from rolesettingdetail r 
                                        where r.rolesettingid =@rolesettingid ";
                var roleDetails = _uow.Query<RoleSettingDetailDto>(query, new { rolesettingid = request.All ? request.RoleId : account.RoleId }).ToList();

                if (!request.All)
                {
                    _logger.LogInformation("İf Girdi");
                    foreach (var item in modules)
                    {
                        var navigations = GetData(item);
                        if (navigations != null)
                        {
                            if (account.AccountType != Shared.Accounts.AccountType.CompanyAdmin
                                    && account.AccountType != BrewCloud.Shared.Accounts.AccountType.Admin)
                            {

                                if (navigations.Children.Any())
                                {
                                    foreach (var it in navigations.Children)
                                    {
                                        RemoveChild(it, roleDetails);
                                    }
                                }
                                navigations.Children.RemoveAll(r => (r.Children == null || !r.Children.Any()) && !roleDetails.Any(x => x.Target == r.Id));
                            }
                            if (navigations.Children.Any())
                            {
                                response.Data.Default.Add(navigations);
                            }
                        }
                        else
                        {
                            response.Errors.Add("Dosya Yolu Bulunamadı.");
                        }
                    }
                }
                else
                {
                }




            }
            catch (Exception ex)
            {
                response.Errors.Add(ex.Message.ToString());
                response.Errors.Add(ex.InnerException.ToString());
                response.ResponseType = ResponseType.Error;
            }
            return response;
        }

        private RootNavigationItem GetData(string name)
        {
            _logger.LogInformation(AppDomain.CurrentDomain.RelativeSearchPath == null ? "RelativeSearchPath NULL" : AppDomain.CurrentDomain.RelativeSearchPath);
            _logger.LogInformation(AppDomain.CurrentDomain.BaseDirectory == null ? "BaseDirectory NULL" : AppDomain.CurrentDomain.BaseDirectory);
            var path = Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory, $"Settings/NavBar/{name}.json");
            if (!File.Exists(path))
            {
                return null;
            }
            var file = File.ReadAllText(path);
            var items = JsonConvert.DeserializeObject<RootNavigationItem>(file);

            return items;
        }

        private void RemoveChild(NavigationItem item, List<RoleSettingDetailDto> roleDetails)
        {
            if (item.Children == null)
                return;
            if (item.Children.Any())
            {
                foreach (var it in item.Children)
                {
                    if (it.Children == null)
                        continue;

                    if (it.Children.Any())
                        it.Children.RemoveAll(r => !roleDetails.Any(x => x.Target == r.Id));

                    RemoveChild(it, roleDetails);
                }
                item.Children.RemoveAll(r => (r.Children == null || !r.Children.Any()) && !roleDetails.Any(x => x.Target == r.Id));

            }

        }
    }
}
