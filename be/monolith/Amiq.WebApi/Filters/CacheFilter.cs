using Amiq.WebApi.Core.Cache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Amiq.WebApi.Filters
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheFilter : Attribute, IResourceFilter
    {
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            string key = context.HttpContext.Request.Path;
            SynchronizedCache cache = CacheFacade.GetCacheByKey(key);
            cache.Add(key, context.Result);
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string key = context.HttpContext.Request.Path;
            SynchronizedCache cache = CacheFacade.GetCacheByKey(key);
            IActionResult cachedValue = cache.Read(key);
            if (cachedValue != null)
            {
                context.Result = cachedValue;
            }
        }
    }
}
