using GIHUN_MVC_Project.Core.Couchbase;
using GIHUN_MVC_Project.Core.Interfaces;
using GIHUN_MVC_Project.Filter;
using GIHUN_MVC_Project.Models.Hotels;
using GIHUN_MVC_Project.ViewModels.Hotels;
using GIHUN_MVC_Project.ViewModels.Search;
using Microsoft.AspNetCore.Mvc;
using Shared.Pagination;

namespace GIHUN_MVC_Project.Controllers
{
    public class HotelController : Controller
    {
        private readonly IReservationHotelRepository _hotelRepository;
        private readonly IUserRepository _userRepository;
        public ICouchbaseRepository _couchbaseRepository { get; set; }

        public HotelsInfoViewModel? HotelsInfo { get; set; } = new HotelsInfoViewModel();

        public HotelController(IReservationHotelRepository hotelRepository, IUserRepository userRepository, ICouchbaseRepository couchbaseRepository)
        {
            _hotelRepository = hotelRepository;
            _userRepository = userRepository;
            SearchViewModel hotelView = new SearchViewModel();
            hotelView.SearchString = string.Empty;
            HotelsInfo = new HotelsInfoViewModel();
            _couchbaseRepository = couchbaseRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateReservationHotel(int hotelId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (hotelId < 0)
            {
                return BadRequest("0일수는 없습니다.");
            }

            HotelsInfo = _hotelRepository?.GetByHotelId(hotelId);

            return View(HotelsInfo);
        }

        [ActionName("CreateReservationHotel")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [TypeFilter(typeof(UserActionFilter))]
        public IActionResult CreateReservationHotelPost(HotelsInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.ReservationHotel?.ReservationHotel == null || model.ReservationHotel?.UserInfo?.Email == string.Empty ||
                 model.ReservationHotel?.Paid == null)
            {
                return BadRequest();
            }

            string userEmail = string.Empty;

            if (Request.Cookies["Email"] != null)
            {
                userEmail = Request.Cookies["Email"].ToString();
            }
            else
            {
                return BadRequest("Plase Login");
            }

            // Email값을 이용하여 DB에서 유저의 GuId값을 가져온다.
            var userGuid = _userRepository.GetUserGuId(userEmail);

            model.ReservationHotel.UserInfo.Guid = userGuid;

            var tempGuid = Guid.NewGuid().ToString();
            model.ReservationHotel.Paid.Paid_Date = DateTime.Now;
            model.ReservationHotel.ReservationHotel.Booking_Id = tempGuid;
            model.ReservationHotel.Paid.Paid_Guid = tempGuid;

            var result = _hotelRepository.Create(model).Result.Value;

            if (result == null || Convert.ToInt32(result) != 0)
            {
                return BadRequest();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        [HttpGet]
        public IActionResult UpdateReservationHotel(string bookingId)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            string userEmail = string.Empty;

            if (Request.Cookies["Email"] != null)
            {
                userEmail = Request.Cookies["Email"].ToString();
            }
            else
            {
                return BadRequest("Plase Login");
            }

            var userGuid = _userRepository.GetUserGuId(userEmail);

            UpdateReservationHotelViewModel model = _hotelRepository.Details(userGuid, bookingId);

            var hotelInfoViewModel = _hotelRepository.GetByHotelName(model.Hotel_Name);

            // 오른쪽 사이드바 메뉴
            ViewData["hotelInfoViewModel"] = hotelInfoViewModel;

            return View(model);
        }

        [ActionName("UpdateReservationHotel")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReservationHotelPost(UpdateReservationHotelViewModel model)
        {
            string userEmail = string.Empty;

            if (Request.Cookies["Email"] != null)
            {
                userEmail = Request.Cookies["Email"].ToString();
            }
            else
            {
                return BadRequest("Plase Login");
            }

            var userGuid = _userRepository.GetUserGuId(userEmail);
            model.Paid_Date = DateTime.Now;

            var updateResult = await _hotelRepository.Update(model, userGuid);

            return RedirectToAction("GetUserReservation", "Hotel");
        }

        [HttpGet]
        public IActionResult SearchHotelByCountry()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return View();
        }

        [ActionName("SearchHotelByCountry")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchHotelByCountryPost(string country)
        {
            if (country == null || country.Length == 0)
            {
                return BadRequest("가고 싶은 나라를 선택해주세요.");
            }
            return RedirectToAction("SearchResult", "Hotel", new { country });
        }

        public IActionResult SearchResult(string country, int pg = 1)
        {
            var hotelInfo = _hotelRepository.SearchHotelByCountry(country);

            if (hotelInfo == null)
            {
                return BadRequest("데이터가 없습니다.");
            }

            return View(hotelInfo);
        }

        public async Task<ActionResult<List<HotelsInfoViewModel>>> GetAllHotels(int pg = 1)
        {
            var result = _hotelRepository.GetAll().DistinctBy(x => x.addressline1).ToList();

            const int pageSize = 4;
            if (pg < 1) pg = 1;

            int count = result.Count();
            var pager = new Pager(count, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = result.Skip(recSkip).Take(pager.PageSize).ToList();

            ViewBag.Pager = pager;

            var cacheData = await _couchbaseRepository.GetAllCacheData();

            TempData["cacheData"] = cacheData;

            return View(data);
        }

        [HttpPost]
        public IActionResult AddLikeHotels(int hotelId)
        {
            if (hotelId <= 0)
            {
                return BadRequest("다시 로그인 후 추가바랍니다.");
            }

            string userEmail = string.Empty;

            if (Request.Cookies["Email"] != null)
            {
                userEmail = Request.Cookies["Email"].ToString();
            }
            else
            {
                return BadRequest("Plase Login");
            }

            var userGuid = _userRepository.GetUserGuId(userEmail);
            var result = _hotelRepository.AddLikeHotels(hotelId, userGuid)?.Value;

            if (Convert.ToInt32(result) != 0 || result == null)
            {
                return BadRequest("추가하기가 실패했습니다. 운영자에게 문의바랍니다.");
            }

            return RedirectToAction("GetUserLikes", "Hotel");
        }

        [HttpGet]
        public IActionResult GetUserLikes(string userEmail, int pg = 1)
        {
            string email = userEmail;

            if (Request.Cookies["Email"] != null)
            {
                email = Request.Cookies["Email"].ToString();
            }
            else
            {
                return BadRequest("로그인해주세요!");
            }

            var userGuid = _userRepository.GetUserGuId(email);

            var result = _hotelRepository.GetUserLikeHotelList(userGuid);

            ViewData["email"] = email;

            const int pageSize = 2;
            if (pg < 1) pg = 1;

            int count = result.Count();
            var pager = new Pager(count, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = result.Skip(recSkip).Take(pager.PageSize).ToList();

            ViewBag.Pager = pager;

            return View(data);
        }


        [HttpGet]
        public IActionResult GetUserReservation(string userEmail, int pg = 1)
        {
            string email = userEmail;

            if (Request.Cookies["Email"] != null)
            {
                email = Request.Cookies["Email"].ToString();
            }
            else
            {
                return BadRequest("로그인해주세요!");
            }
            var userGuid = _userRepository.GetUserGuId(email);

            var result = _hotelRepository.GetUserReservationHotels(userGuid);

            const int pageSize = 2;
            if (pg < 1) pg = 1;

            int count = result.Count();
            var pager = new Pager(count, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = result.Skip(recSkip).Take(pager.PageSize).ToList();

            ViewBag.Pager = pager;

            return View(data);
        }


        [HttpGet]
        public async Task<IActionResult> HotelDetails(int hotelId)
        {
            var hotelInfo = _hotelRepository.GetByHotelId(hotelId);
            hotelInfo.CacheAddedDate = DateTime.UtcNow.ToString();

            if (hotelInfo == null)
            {
                return BadRequest("데이터가 없습니다.");
            }

            return View(hotelInfo);
        }

        [HttpGet]
        public async Task SearchInHotelsAPI()
        {
            await _couchbaseRepository.SaveUserSearchedFromAPI();
        }

        [HttpGet]
        public async Task<IActionResult> ShowHotelAPI()
        {
            var result = await _couchbaseRepository.GetAllBucketData();

            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Upsert(string id)
        {
            if (id == null || string.IsNullOrEmpty(id))
            {
                return View();
            }

            var data = await _couchbaseRepository.GetById(id);

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(SaveHotelSummary model)
        {

            if (model.Id == null || string.IsNullOrEmpty(model.Id))
            {
                return NotFound();
            }

            await _couchbaseRepository.Upsert(model);

            return RedirectToAction(nameof(ShowHotelAPI));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var result = await _couchbaseRepository.GetById(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            await _couchbaseRepository.Delete(id);

            return RedirectToAction(nameof(ShowHotelAPI));
        }

    }
}
