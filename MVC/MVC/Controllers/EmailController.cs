using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class EmailController : Controller
    {
        private MailBox _mailBox;

        public EmailController(MailBox mailBox)
        {
            _mailBox = mailBox;
        }

        // GET: EmailController
        public ActionResult Index()
        {
            var emails = _mailBox.FindEmails();

            return View(new EmailViewModel { Emails = emails });
        }

        // GET: EmailController/Details/5
        public ActionResult Details(int id)
        {
            var email = _mailBox.FindEmail(id);

            return View(new EmailDetailsViewModel { Email = email });
        }

        // GET: EmailController/Create
        public ActionResult Create()
        {
            return View(new EmailActionViewModel());
        }

        // POST: EmailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmailDTO formData)
        {
            var valid = EmailDTO.Validate(formData);
            var email = formData.Email;

            if (valid)
            {
                _mailBox.AddEmail(email);
            }

            return View(new EmailActionViewModel { ActionCompleted = valid, HasError = !valid, Email = email });
        }

        // GET: EmailController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(new EmailActionViewModel { Email = _mailBox.FindEmail(id), ActionCompleted = false });
        }

        // POST: EmailController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            var email = _mailBox.FindEmail(id);
            _mailBox.DeleteEmail(email);

            return View(new EmailActionViewModel { ActionCompleted = true, Email = email });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Clear(IFormCollection collection)
        {
            _mailBox.Clear();

            return View(new EmailActionViewModel{ ActionCompleted = true });
        }

        public ActionResult Clear()
        {
            return View(new EmailActionViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(SearchDTO formData)
        {
            var emails = _mailBox.FindEmails(formData.Search);

            return View(new EmailViewModel { Emails = emails, Search = formData.Search });
        }

        public ActionResult Search()
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
