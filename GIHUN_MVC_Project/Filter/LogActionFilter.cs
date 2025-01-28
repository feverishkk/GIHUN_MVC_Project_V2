using GIHUN_MVC_Project.Core.Couchbase;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GIHUN_MVC_Project.Filter
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        private readonly ICouchbaseRepository _couchbaseRepo;

        public LogActionFilterAttribute(ICouchbaseRepository couchbaseRepository)
        {
            _couchbaseRepo = couchbaseRepository;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userEmail = context.HttpContext.Request.Cookies["Email"].ToString();
            var controllerName = context.ActionDescriptor.RouteValues.Values.ToArray();
            var position = context.RouteData.Values.Values.ToArray();
            var log = position[0].ToString() + "/" + position[1].ToString();

            await _couchbaseRepo.InsertLog("Gihun-MVC-Project-Start-Log", userEmail, log, "", "Start");

            await next();

            await _couchbaseRepo.InsertLog("Gihun-MVC-Project-Start-Log", userEmail, log, "", "END");
        }


    }
}
