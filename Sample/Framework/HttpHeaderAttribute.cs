using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sample.Framework
{ 
    public class HttpHeaderAttribute : ActionFilterAttribute 
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public HttpHeaderAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Headers.Add(Name, Value);
            base.OnActionExecuted(filterContext);
        }
    }
}