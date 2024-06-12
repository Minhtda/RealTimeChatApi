using Application.InterfaceService;
using Application.ViewModel.MessageModel;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using MobileAPI.Hubs;

namespace MobileAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageController(IMessageService messageService, IHubContext<ChatHub> hubContext)
        {
            _messageService = messageService;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMessages()
        {
            var messages = await _messageService.GetAllMessages();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMessageById(Guid id)
        {
            var message = await _messageService.GetMessageById(id);
            if (message == null)
            {
                return NotFound();
            }
            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage([FromBody] CreateMessageModel message)
        {
            var isCreated = await _messageService.CreateMessage(message);
            if (isCreated)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage([FromBody] UpdateMessageModel message)
        {
            var isUpdated = await _messageService.UpdateMessage(message);
            if (isUpdated)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(Guid id)
        {
            var isDeleted = await _messageService.DeleteMessage(id);
            if (isDeleted)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(Guid messageId, string user, string message)
        {
            // Get sender information from the message ID
            var messageEntity = await _messageService.GetMessageById(messageId);
            if (messageEntity == null)
            {
                return NotFound("Message not found");
            }

            // Pass sender information along with the message
            await _hubContext.Clients.Group(messageId.ToString()).SendAsync("ReceiveMessage", messageEntity.Sender.UserName, message);
            return Ok();
        }

        [HttpPost("JoinRoom")]
        public async Task<IActionResult> JoinRoom(Guid messageId)
        {
            //await _hubContext.Groups.AddToGroupAsync(Context.ConnectionId, messageId.ToString());
            return Ok();
        }

        [HttpPost("LeaveRoom")]
        public async Task<IActionResult> LeaveRoom(Guid messageId)
        {
           //await _hubContext.Groups.RemoveFromGroupAsync(Context.ConnectionId, messageId.ToString());
            return Ok();
        }
    }
}