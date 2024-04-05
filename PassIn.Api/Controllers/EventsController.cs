using Microsoft.AspNetCore.Mvc;
using PassIn.Application;
using PassIn.Communication;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisterEventsJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestEventJson request)
        {
            try
            {
                var useCase = new RegisterEventUseCase();

                var response = useCase.Execute(request);

                return Created(string.Empty, response);
            }
            catch (PassInException ex)
            {
                return BadRequest(new ResponseErrorJson(ex.Message));
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseErrorJson("Unknow error.")
                );
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            try
            {
                var useCase = new GetByIdEventUseCase();

                var response = useCase.Execute(id);

                return Ok(response);
            }
            catch (PassInException ex)
            {
                return BadRequest(new ResponseErrorJson(ex.Message));
            }
            catch
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new ResponseErrorJson("Unknow error")
                );
            }
        }
    }
}
