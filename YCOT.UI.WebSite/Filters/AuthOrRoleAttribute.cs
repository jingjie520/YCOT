using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YCOT.UI.WebSite.Filters
{
    public class AuthOrRoleAttribute : ActionFilterAttribute
    {

        /// <summary>
        /// Action执行前的操作
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.RouteData.Values["action"].ToString();
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            // filterContext.HttpContext.Response.Write("\n<!--OnActionExecuting:" + controllerName + "/" + actionName + " -->");

            var username = filterContext.HttpContext.User.Identity.Name;
            var ResultInfo = "";

            if (!BLL.Power.Verification.ControllerActionByUserName(username, controllerName, actionName, ref ResultInfo))
            {
                filterContext.HttpContext.Response.Write("\n<!--拒绝：" + ResultInfo + "-->");
                filterContext.HttpContext.Response.Redirect("/Account/Login", true);


            }
            else
            {
                filterContext.HttpContext.Response.Write("\n<!--允许:" + ResultInfo + "-->");

            }
        }

        /// <summary>
        /// Action执行后的操作
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // filterContext.HttpContext.Response.Write("<!--OnActionExecuted -->");
        }

        /// <summary>
        /// 解析ActionResult前执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // filterContext.HttpContext.Response.Write("<!-- OnResultExecuting-->");
        }

        /// <summary>
        /// 解析ActionResult后执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            // filterContext.HttpContext.Response.Write("<!--OnResultExecuted -->");
        }

    }
}