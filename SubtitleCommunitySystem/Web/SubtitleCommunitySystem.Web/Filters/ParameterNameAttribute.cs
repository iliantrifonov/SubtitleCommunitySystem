namespace SubtitleCommunitySystem.Web.Filters
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ParameterNameAttribute : ActionFilterAttribute
    {
        public string ViewParameterName { get; set; }
        public string ActionParameterName { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Request.QueryString[ViewParameterName] != null)
            {
                var parameterValue = filterContext.HttpContext.Request.QueryString[ViewParameterName];
                filterContext.ActionParameters.Remove(ActionParameterName);
                filterContext.ActionParameters.Add(ActionParameterName, parameterValue);
            }
        }
    }
}