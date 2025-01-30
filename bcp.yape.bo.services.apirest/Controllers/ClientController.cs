using bcp.yape.bo.core.Ports.Driving;
using bcp.yape.bo.services.apirest.DTO;
using Microsoft.AspNetCore.Mvc;

namespace bcp.yape.bo.services.apirest.Controllers
{
    /// <summary>
    /// Controlador para la gestión de clientes.
    /// </summary>
    [ApiController]
    [Route("api/clients")]
    public class ClientController : Controller
    {
        private readonly IClientService _clientService;

        /// <summary>
        /// Constructor del controlador de clientes.
        /// Inyecta el servicio de cliente.
        /// </summary>
        /// <param name="clientService">El servicio que maneja la lógica de negocio relacionada con los clientes.</param>
        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        /// <summary>
        /// Registra un nuevo cliente en el sistema.
        /// </summary>
        /// <param name="request">El objeto de solicitud que contiene los datos del cliente a registrar.</param>
        /// <returns>Una respuesta HTTP con el estado de la operación. Si es exitoso, devuelve un código 201 con la URL del recurso creado.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientRegistrationRequest request)
        {
            try
            {
                // CHECK IF THE MODEL IS VALID ACCORDING TO THE VALIDATION RULES OF THE DTO ATTRIBUTES.
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // GET THE CLIENT'S IP ADDRESS.
                // TRY TO GET THE IP FROM THE X-FORWARDED-FOR HEADER, USEFUL IF BEHIND A PROXY, LOAD BALANCER, OR SIMILAR (CLOUDFLARE, ETC.).
                var forwardedFor = HttpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                var clientIpAddress = forwardedFor ?? HttpContext?.Connection.RemoteIpAddress?.ToString();

                // CONVERT THE RECEIVED DTO INTO A BUSINESS ENTITY OBJECT.
                var clientRegistrationData = request.ToClientRegistrationData(clientIpAddress);

                // CALL THE BUSINESS SERVICE TO ADD THE CLIENT TO THE SYSTEM.
                var validationResult = await _clientService.AddClientAsync(clientRegistrationData);

                // IF THE CLIENT WAS SUCCESSFULLY ADDED.
                if (validationResult.Success)
                {
                    // RETURN AN HTTP 201 RESPONSE WITH THE URL OF THE CREATED RESOURCE.
                    return CreatedAtAction(nameof(CreateClient), new { Guid = validationResult.ReturnObject });
                }
                else
                {
                    // IF VALIDATION FAILS, RETURN A BAD REQUEST WITH THE FAILURE MESSAGE.
                    return BadRequest(validationResult.Message);
                }
            }
            catch (Exception ex)
            {
                // GENERIC ERROR HANDLING FOR UNEXPECTED ISSUES.
                // LOGGING THE EXCEPTION CAN BE DONE HERE IF NECESSARY (e.g., using a logger).
                return StatusCode(500, new { message = "Ocurrio un error.", details = ex.Message });
            }
        }
    }
}
