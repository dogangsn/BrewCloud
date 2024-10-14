using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BrewCloud.Shared.Service;
using BrewCloud.Vet.Application.GrpServices;

namespace BrewCloud.Vet.Application.Attributes
{
    public class ClientIpCheckActionFilter : ActionFilterAttribute
    {
        private readonly string _safelist;
        private readonly ILogger _logger;
        private readonly IIdentityRepository _identityRepository;
        private readonly IdentityGrpService _identityGrpService;

        public ClientIpCheckActionFilter(string safelist, ILogger logger, IIdentityRepository identityRepository, IdentityGrpService identityGrpService)
        {
            _safelist = safelist;
            _logger = logger;
            _identityRepository = identityRepository;
            _identityGrpService = identityGrpService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
            _logger.LogDebug("Remote IpAddress: {RemoteIp}", remoteIp);
            var ip = _safelist.Split(';');
            var badIp = true;

            if (remoteIp.IsIPv4MappedToIPv6)
            {
                remoteIp = remoteIp.MapToIPv4();
            }

            //Grpc üzerinden erişim kontrolü yapılacak

            foreach (var address in ip)
            {
                var testIp = IPAddress.Parse(address);

                if (testIp.Equals(remoteIp))
                {
                    badIp = false;
                    break;
                }
            }

            if (badIp)
            {
                _logger.LogWarning("Forbidden Request from IP: {RemoteIp}", remoteIp);
                context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
