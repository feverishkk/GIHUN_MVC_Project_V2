using GIHUN_MVC_Project.Core.Couchbase;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace GIHUN_MVC_Project.Filter
{
    public class UserActionFilter : ActionFilterAttribute
    {

        private readonly ICouchbaseRepository _couchbaseRepository;
        public string log { get; set; } = "";

        public UserActionFilter(ICouchbaseRepository couchbaseRepository)
        {
            _couchbaseRepository = couchbaseRepository;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string userEmail = context.HttpContext.Request.Cookies["Email"].ToString();
            var actionArgument = JsonConvert.SerializeObject(context.ActionArguments.Values);

            await _couchbaseRepository.InsertLog("Gihun-MVC-Project-User-Log", userEmail, context.ActionDescriptor.DisplayName.ToString(), actionArgument, "Started");

            await next();

            await _couchbaseRepository.InsertLog("Gihun-MVC-Project-User-Log", userEmail, context.ActionDescriptor.DisplayName.ToString(), actionArgument, "Ended");
        }

    }
}
