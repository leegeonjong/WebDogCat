using dogcat.Models.Domain;
using dogcat.Models.ViewModels;
using dogcat.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace dogcat.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepositories _messageRepositories;

        public MessageController(IMessageRepositories messageRepositories)
        {
            _messageRepositories = messageRepositories;
        }
        [HttpGet]
        public async Task<IActionResult> home(long id)
        {
            var message = await _messageRepositories.MessageAsync(id);
            return View(message);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(long id)
        {
            var message = await _messageRepositories.McontextAsync(id);
            return View(message);
        }
        [HttpGet]
        public async Task<IActionResult> Add(long id)
        {
            var user = await _messageRepositories.UserAsync(id);
            AddMessageRequest m = new AddMessageRequest()
            {
                Title = (string)TempData["Title"],
                Mail = (string)TempData["Mail"],
                Context = (string)TempData["Context"],
                From_id = user.Id,
                Time = DateTime.Now,
            };
            return View(m);
        }
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add (AddMessageRequest addMessageRequest)
        {
            addMessageRequest.Validate();
            if (addMessageRequest.HasError)
            {
                TempData["TitleError"] = addMessageRequest.ErrorTitle;
                TempData["MailError"] = addMessageRequest.ErrorMail;
                TempData["ContextError"] = addMessageRequest.ErrorContext;

                TempData["Title"] = addMessageRequest.Title;
                TempData["Mail"] = addMessageRequest.Mail;
                TempData["Context"] = addMessageRequest.Context;

                return RedirectToAction("Add", new {id = addMessageRequest.From_id});
            }
            var to_id = await _messageRepositories.MailAsync(addMessageRequest.Mail);
            if (to_id == null) return View("Nomail", 1);
            var Message = new Message
            {
                Title = addMessageRequest.Title,
                Time = addMessageRequest.Time,
                Context = addMessageRequest.Context,
                Status = true,
                From_id = addMessageRequest.From_id,
                To_id = to_id.Id,
            };
            await _messageRepositories.AddAsync(Message);
            return RedirectToAction("home", new {id = Message.From_id});
        }
    }
}
