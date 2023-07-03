using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace VetSystems.Shared.Controllers
{
    public class VetSystemsController : ControllerBase
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
