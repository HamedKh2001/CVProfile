using CoreLayer.IServices;
using DataLayer.Entities;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace CVProfile.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Owner")]
    public class UserController : Controller
    {
        private readonly IGenericRepository<User> _userGenericRepository;
        public UserController(IGenericRepository<User> userGenericRepository)
        {
            _userGenericRepository = userGenericRepository;
        }
        public IActionResult Index()
        {
            return View(_userGenericRepository.GetAll());
        }
    }
}
