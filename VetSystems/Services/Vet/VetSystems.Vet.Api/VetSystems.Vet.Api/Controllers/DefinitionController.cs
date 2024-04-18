using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetSystems.Vet.Application.Features.Customers.Commands;
using VetSystems.Vet.Application.Features.Customers.Queries;
using VetSystems.Vet.Application.Features.Definition.AnimalColorsDef.Commands;
using VetSystems.Vet.Application.Features.Definition.AnimalColorsDef.Queries;
using VetSystems.Vet.Application.Features.Definition.AppointmentTypes.Commands;
using VetSystems.Vet.Application.Features.Definition.AppointmentTypes.Queries;
using VetSystems.Vet.Application.Features.Definition.CasingDefinition.Commands;
using VetSystems.Vet.Application.Features.Definition.CasingDefinition.Queries;
using VetSystems.Vet.Application.Features.Definition.CustomerGroup.Commands;
using VetSystems.Vet.Application.Features.Definition.CustomerGroup.Queries;
using VetSystems.Vet.Application.Features.Definition.PaymentMethods.Commands;
using VetSystems.Vet.Application.Features.Definition.PaymentMethods.Queries;
using VetSystems.Vet.Application.Features.Definition.ProductCategory.Commands;
using VetSystems.Vet.Application.Features.Definition.ProductCategory.Queries;
using VetSystems.Vet.Application.Features.Definition.ProductDescription.Commands;
using VetSystems.Vet.Application.Features.Definition.ProductDescription.Queries;
using VetSystems.Vet.Application.Features.Definition.Taxis.Commands;
using VetSystems.Vet.Application.Features.Definition.Taxis.Queries;
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

        [HttpPost(Name = "ProductDescriptionFilters")]
        public async Task<IActionResult> ProductDescriptionFilters([FromBody] ProductDescriptionFiltersQuery command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpPost(Name = "ProductMovementList")]
        public async Task<IActionResult> ProductMovementList([FromBody] ProductMovementListQuery command)
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

        [HttpPost(Name = "UpdateUnits")]
        public async Task<IActionResult> UpdateUnits([FromBody] UpdateUnitsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteUnits")]
        public async Task<IActionResult> DeleteUnits([FromBody] DeleteUnitsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion

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

        [HttpPost(Name = "UpdateCustomerGroupDef")]
        public async Task<IActionResult> UpdateCustomerGroupDef([FromBody] UpdateCustomerGroupDefCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteCustomerGroupDef")]
        public async Task<IActionResult> DeleteCustomerGroupDef([FromBody] DeleteCustomerGroupDefCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        #endregion

        #region CasingDefinition

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

        #region AnimalColorsDef

        [HttpGet(Name = "AnimalColorsDefList")]
        public async Task<IActionResult> AnimalColorsDefList()
        {
            var command = new AnimalColorsDefListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateAnimalColorsDef")]
        public async Task<IActionResult> CreateAnimalColorsDef([FromBody] CreateAnimalColorsDefCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion


        #region PaymentMethods


        [HttpGet(Name = "PaymentMethodList")]
        public async Task<IActionResult> PaymentMethodList()
        {
            var command = new PaymentMethodListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreatePaymentMethods")]
        public async Task<IActionResult> CreatePaymentMethods([FromBody] CreatePaymentMethodsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdatePaymentMethods")]
        public async Task<IActionResult> UpdatePaymentMethods([FromBody] UpdatePaymentMethodsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeletePaymentMethods")]
        public async Task<IActionResult> DeletePaymentMethods([FromBody] DeletePaymentMethodsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion

        #region AppointmentTypes

        [HttpGet(Name = "GetAppointmentTypesList")]
        public async Task<IActionResult> GetAppointmentTypesList()
        {
            var command = new GetAppointmentTypesListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateAppointmentTypes")]
        public async Task<IActionResult> CreateAppointmentTypes([FromBody] CreateAppointmentTypesCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteAppointmentTypes")]
        public async Task<IActionResult> DeleteAppointmentTypes([FromBody] DeleteAppointmentTypesCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateAppointmentTypes")]
        public async Task<IActionResult> UpdateAppointmentTypes([FromBody] UpdateAppointmentTypesCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        #endregion

        #region Taxis

        [HttpGet(Name = "GetTaxisList")]
        public async Task<IActionResult> GetTaxisList()
        {
            var command = new GetTaxisListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "CreateTaxis")]
        public async Task<IActionResult> CreateTaxis([FromBody] CreateTaxisCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteTaxis")]
        public async Task<IActionResult> DeleteTaxis([FromBody] DeleteTaxisCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateTaxis")]
        public async Task<IActionResult> UpdateTaxis([FromBody] UpdateTaxisCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        #endregion
    }
}
