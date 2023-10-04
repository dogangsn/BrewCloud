using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;

namespace VetSystems.Account.Application.Features.Settings.Commands
{
    public class CreateRoleSettingCommand : IRequest<Response<bool>>
    {
    }


}
