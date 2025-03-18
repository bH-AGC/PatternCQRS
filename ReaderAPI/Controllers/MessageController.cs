using Microsoft.AspNetCore.Mvc;
using ReaderAPI.Models;
using ReaderAPI.Services;

namespace ReaderAPI.Controllers
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
        
        // GET: api/messages
        [HttpGet]
        public IActionResult GetAllMessages()
        {
            List<Message> result = _messageService.GetAllMessages();

            if (!result.Any())
            {
                return BadRequest("Aucun message trouvé.");
            }

            return Ok(result);
        }

    }
}