using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VetSystems.Account.Application.Models.Settings;
using VetSystems.Account.Domain.Contracts;
using VetSystems.Shared.Dtos;
using VetSystems.Shared.Dtos.Settings;
using VetSystems.Shared.Service;
using VetSystems.Shared.Service.Nav;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace VetSystems.Account.Application.Features.Settings.Queries
{
    public class GetNavigationQuery : IRequest<Response<Navigation>>
    {
        public bool All { get; set; }
        public Guid? RoleId { get; set; }
    }

    public class GetNavigationQueryHandler : IRequestHandler<GetNavigationQuery, Response<Navigation>>
    {
        private readonly IIdentityRepository _identity;
        private readonly IUnitOfWork _uow;
        private readonly ModuleService _moduleService;

        public GetNavigationQueryHandler(IIdentityRepository identity, IUnitOfWork uow, ModuleService moduleService)
        {
            _identity = identity;
            _uow = uow;
            _moduleService = moduleService;
        }

        public async Task<Response<Navigation>> Handle(GetNavigationQuery request, CancellationToken cancellationToken)
        {

            var response = new Response<Navigation>
            {
                Data = new Navigation(),
                ResponseType = ResponseType.Ok
            };
            var account = _identity.Account;

            try
            {

                var modules = _moduleService.GetModule();

                string query = @"select id,action,target,roleSettingid  from rolesettingdetail r 
                                        where r.rolesettingid =@rolesettingid ";
                var roleDetails = _uow.Query<RoleSettingDetailDto>(query, new { rolesettingid = request.All ? request.RoleId : account.RoleId }).ToList();

                if (!request.All)
                {
                    foreach (var item in modules)
                    {
                        var navigations = GetData(item);
                        if (navigations != null)
                        {
                            if (account.AccountType != Shared.Accounts.AccountType.CompanyAdmin
                                    && account.AccountType != VetSystems.Shared.Accounts.AccountType.Admin)
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
                    }
                }
                else
                {


                }
            }
            catch (Exception ex)
            {
            }
            return response;
        }

        private RootNavigationItem GetData(string name)
        {
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
