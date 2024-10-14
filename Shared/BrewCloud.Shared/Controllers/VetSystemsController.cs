using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrewCloud.Shared.Controllers
{
    public class BrewCloudController : ControllerBase
    {
        public IActionResult ReturnResult<T>(Shared.Dtos.Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
