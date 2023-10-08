using Microsoft.AspNetCore.Mvc;
using Messaging.Application.Services;
using Messaging.Domain;
using System;
using System.Threading.Tasks;

namespace Messaging.Intraestructure
{
    public class CreateMessageRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class UpdateMessageRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }




    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessagesController(MessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var message = await _messageService.GetMessageById(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _messageService.GetAllMessages();
            return Ok(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMessageRequest request)
        {
            var message = await _messageService.CreateMessage(new Email(request.From), new Email(request.To), request.Subject, request.Body);
            return CreatedAtAction(nameof(GetById), new { id = message.Id }, message);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateMessageRequest request)
        {
            try
            {
                var message = await _messageService.UpdateMessage(id, new Email(request.From), new Email(request.To), request.Subject, request.Body);
                return Ok(message);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _messageService.DeleteMessage(id);
            return NoContent();
        }
    }
}
