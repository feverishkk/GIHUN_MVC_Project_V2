using GIHUN_MVC_Project.Core.AmazonS3;
using GIHUN_MVC_Project.Core.Couchbase;
using GIHUN_MVC_Project.Core.Interfaces;
using GIHUN_MVC_Project.Models.AmazonS3;
using GIHUN_MVC_Project.ViewModels.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shared.Users;
using System.Security.Claims;

namespace GIHUN_MVC_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IReservationHotelRepository _reservationHotelRepository;
        private readonly IAmazonS3Repository _s3Repository;
        private readonly ICouchbaseRepository _couchbaseRepository;

        public UserController(IUserRepository userRepository, IReservationHotelRepository reservationHotelRepository,
                              IAmazonS3Repository s3Repository, ICouchbaseRepository couchbaseRepository)
        {
            _userRepository = userRepository;
            _reservationHotelRepository = reservationHotelRepository;
            _s3Repository = s3Repository;
            _couchbaseRepository = couchbaseRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userEmail = HttpContext.Request.Cookies["Email"];

            if (string.IsNullOrEmpty(userEmail) == true)
            {
                return BadRequest();
            }

            var userProfileImageUrl = await _couchbaseRepository.GetProfileImageCache(userEmail);

            var result = new UserProfileImage()
            {
                UserName = userEmail,
                UserProfileImageUrl = userProfileImageUrl,
            };

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> IndexAsync(IFormFile file)
        {
            if (file == null)
                return BadRequest();
            else
            {
                var userEmail = HttpContext.Request.Cookies["Email"];

                var uploadResult = await _s3Repository.UploadFile(file);

                if (uploadResult == false || string.IsNullOrEmpty(userEmail))
                    return BadRequest();

                var getUserProfileByKey = await _s3Repository.GetFileByKey(userEmail + ".jpg");


                var replacedEmail = userEmail.Replace("@", "%40");

                var result = new UserProfileImage()
                {
                    UserName = userEmail,
                    UserProfileImageUrl = $"https://gihun-mvc-project-profile-image.s3.ap-northeast-2.amazonaws.com/{replacedEmail + ".jpg"}"
                };

                await _couchbaseRepository.SetProfileImageCache(userEmail, result.UserProfileImageUrl);

                return View(result);
            }


        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [ActionName("Register")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                return View(model);
            }

            if ((model.Password != model.ConfirmPassword) || model.Password == null || model.ConfirmPassword == null ||
                 string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.ConfirmPassword))
            {
                return View();
            }
            else
            {
                model.MemberSince = DateTime.Now;
                model.Guid = Guid.NewGuid().ToString();
                model.Password = HashAndVerify.Encrypt(model.Password);

                var result = await _userRepository.Register(model);

                if (Convert.ToInt32(result.SqlValue) != 0)
                {
                    return View("Register");
                }

                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [ActionName("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoginPost(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var userEmail = _userRepository.Login(model.Email);
                var userGuId = _userRepository.GetUserGuId(userEmail);

                var userPW = _userRepository.GetUserPassword(userEmail, userGuId);

                var verifyPW = HashAndVerify.Encrypt(model.Password);

                if (userPW != verifyPW)
                {
                    return BadRequest("비밀번호가 다릅니다.");
                }

                var claims = new List<Claim>
                {
                    new Claim (ClaimTypes.Email, model.Email)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddDays(99)
                };

                CookieOptions cookie = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(99),
                    Secure = true,
                    HttpOnly = true,
                    IsEssential = true
                };

                Response.Cookies.Append("Email", model.Email, cookie);

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                    );

                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync();

            Response.Cookies.Delete("Email");

            return RedirectToAction("Index", "Home");
        }





    }
}
