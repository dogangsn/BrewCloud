﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BrewCloud.Vet.Application.Features.Appointment.Commands;
using BrewCloud.Vet.Application.Features.Customers.Queries;
using BrewCloud.Vet.Application.Features.GeneralSettings.Users.Queries;
using BrewCloud.Vet.Application.Features.Patient.Commands;
using BrewCloud.Vet.Application.Features.Patient.Examination.Commands;
using BrewCloud.Vet.Application.Features.Patient.Examination.Queries;
using BrewCloud.Vet.Application.Features.Patient.PatientList.Queries;

namespace BrewCloud.Vet.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PatientController(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region PatientList

        [HttpGet(Name = "GetPatientList")]
        public async Task<IActionResult> GetPatientList()
        {
            var command = new GetPatientListQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "GetPatientById")]
        public async Task<IActionResult> GetPatientById([FromBody] GetPatientByIdQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost(Name = "UpdatePatientsWeight")]
        public async Task<IActionResult> UpdatePatientsWeight([FromBody] UpdatePatientsWeightCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        #endregion

        #region Examination "Muayne"

        [HttpPost(Name = "CreateExamination")]
        public async Task<IActionResult> CreateExamination([FromBody] CreateExaminationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "GetExaminations")]
        public async Task<IActionResult> GetExaminations([FromBody] GetExaminationsQuery command)
        { 
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "GetExaminationByRecId")]
        public async Task<IActionResult> GetExaminationByRecId([FromBody] GetExaminationByRecIdQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }


        [HttpPost(Name = "GetExaminationlistByPatientId")]
        public async Task<IActionResult> GetExaminationlistByPatientId([FromBody] GetExaminationByPatientIdQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpGet(Name = "GetSymptoms")]
        public async Task<IActionResult> GetSymptoms()
        {
            var command = new GetSymptomsQuery();
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "UpdateExamination")]
        public async Task<IActionResult> UpdateExamination([FromBody] UpdateExaminationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpPost(Name = "UpdateExaminationStatus")]
        public async Task<IActionResult> UpdateExaminationStatus([FromBody] UpdateExaminationStatusCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "DeleteExamination")]
        public async Task<IActionResult> DeleteExamination([FromBody] DeleteExaminationCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPost(Name = "GetWeightControls")]
        public async Task<IActionResult> GetWeightControls([FromBody] GetWeightControlsQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        [HttpPost(Name = "GetExaminationsBySaleList")]
        public async Task<IActionResult> GetExaminationsBySaleList([FromBody] GetExaminationsBySaleListQuery model)
        {
            var result = await _mediator.Send(model);
            return Ok(result);
        }

        #endregion

    }
}
