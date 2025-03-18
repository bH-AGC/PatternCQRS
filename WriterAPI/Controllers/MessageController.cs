using Microsoft.AspNetCore.Mvc;
using WriterAPI.Models;
using WriterAPI.Services;

namespace WriterAPI.Controllers
{
    [ApiController]
    [Route("api/messages")]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost]
        public async Task<IActionResult> AddMessage([FromBody] MessageRequest request)
        {
            int success = await _messageService.AddMessage(request.UserName, request.Content);

            if (success == 0)
            {
                return BadRequest("Erreur lors de l'ajout du message.");
            }

            return Ok("Message ajouté avec succès.");
        }
    }
}