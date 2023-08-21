using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Customers.Commands;
using VetSystems.Vet.Application.Features.Customers.Queries;
using VetSystems.Vet.Application.Features.Definition.CasingDefinition.Commands;
using VetSystems.Vet.Application.Features.Definition.CasingDefinition.Queries;
<<<<<<< Updated upstream
using VetSystems.Vet.Application.Features.Definition.CustomerGroup.Commands;
using VetSystems.Vet.Application.Features.Definition.CustomerGroup.Queries;
=======
>>>>>>> Stashed changes
using VetSystems.Vet.Application.Features.Definition.ProductCategory.Commands;
using VetSystems.Vet.Application.Features.Definition.ProductCategory.Queries;
using VetSystems.Vet.Application.Features.Definition.ProductDescription.Commands;
using VetSystems.Vet.Application.Features.Definition.ProductDescription.Queries;
using VetSystems.Vet.Application.Features.Definition.UnitDefinitions.Commands;
using VetSystems.Vet.Application.Features.Definition.UnitDefinitions.Queries;

namespace VetSystems.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DefinitionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DefinitionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region ProductDescription

        [HttpGet(Name = "ProductDescriptionList")]
        public async Task<IActionResult> ProductDescriptionList()
        {
            var command = new ProductDescriptionListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateProductDescription")]
        public async Task<IActionResult> CreateProductDescription([FromBody] CreateProductDescriptionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateProductDescription")]
        public async Task<IActionResult> UpdateProductDescription([FromBody] UpdateProductDescriptionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteProductDescription")]
        public async Task<IActionResult> DeleteProductDescription([FromBody] DeleteProductDescriptionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion




        #region ProductCategory

        [HttpGet(Name = "ProductCategoryList")]
        public async Task<IActionResult> ProductCategoryList()
        {
            var command = new ProductCategoryListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateProductCategories")]
        public async Task<IActionResult> CreateProductCategories([FromBody] CreateProductCategoriesCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateProductCategories")]
        public async Task<IActionResult> UpdateProductCategories([FromBody] UpdateProductCategoriesCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteProductCategories")]
        public async Task<IActionResult> DeleteProductCategories([FromBody] DeleteProductCategoriesCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }



        #endregion

        #region UnitDefinitions

        [HttpGet(Name = "UnitsList")]
        public async Task<IActionResult> UnitsList()
        {
            var command = new UnitsListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost(Name = "CreateUnits")]
        public async Task<IActionResult> CreateUnits([FromBody] CreateUnitsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion

<<<<<<< Updated upstream
        #region CustomerGroupDef

        [HttpGet(Name = "CustomerGroupList")]
        public async Task<IActionResult> CustomerGroupList()
        {
            var command = new CustomerGroupListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateCustomerGroupDef")]
        public async Task<IActionResult> CreateCustomerGroupDef([FromBody] CreateCustomerGroupDefCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }



        #endregion
        #region CasingDefinitions
=======

        #region CasingDefinition
>>>>>>> Stashed changes

        [HttpGet(Name = "CasingDefinitionList")]
        public async Task<IActionResult> CasingDefinitionList()
        {
            var command = new CasingDefinitionListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateCasingDefinition")]
        public async Task<IActionResult> CreateCasingDefinition([FromBody] CreateCasingDefinitionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateCasingDefinition")]
        public async Task<IActionResult> UpdateCasingDefinition([FromBody] UpdateCasingDefinitionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteCasingDefinition")]
        public async Task<IActionResult> DeleteCasingDefinition([FromBody] DeleteCasingDefinitionCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }



        #endregion
    }
}
