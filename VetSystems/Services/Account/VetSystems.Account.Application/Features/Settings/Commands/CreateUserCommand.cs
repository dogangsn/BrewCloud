using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetSystems.Shared.Dtos;

namespace VetSystems.Account.Application.Features.Settings.Commands
{
    public class CreateUserCommand : IRequest<Response<bool>>
    {
        public bool Active { get; set; }
        public string FirstLastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleId { get; set; }
    }



}
