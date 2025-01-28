using GIHUN_MVC_Project.Core.Couchbase;
using GIHUN_MVC_Project.Core.Interfaces;
using GIHUN_MVC_Project.Filter;
using Microsoft.AspNetCore.Mvc;

namespace GIHUN_MVC_Project.Controllers
{

    [TypeFilter(typeof(LogActionFilterAttribute))]
    public class HomeController : Controller
    {
        private readonly IReservationHotelRepository _hotelRepository;
        private readonly ICouchbaseRepository _couchbaseRepository;

        public HomeController(IReservationHotelRepository hotelRepository, ICouchbaseRepository couchbaseRepository)
        {
            _hotelRepository = hotelRepository;
            _couchbaseRepository = couchbaseRepository;
        }


        public IActionResult Index()
        {
            var item1 = _hotelRepository.GetByCountryId(3); // 일본
            var item2 = _hotelRepository.GetByCountryId(107); // UK
            var item3 = _hotelRepository.GetByCountryId(205); // 이태리
            var item4 = _hotelRepository.GetByCountryId(181); // 미국

            ViewBag.Item1 = item1;
            ViewBag.Item2 = item2;
            ViewBag.Item3 = item3;
            ViewBag.Item4 = item4;

            return View();
        }

        [HttpGet]
        public async Task Save()
        {
            await _couchbaseRepository.SaveUserSearchedFromAPI();
        }

    }
}
