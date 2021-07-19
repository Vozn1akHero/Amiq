using AmicaPlus.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmicaPlus.Filters
{

    public class CacheFilter : TypeFilterAttribute
    {
        /// <summary>
        /// params: storedDto
        /// </summary>
        /// <param name="params"></param>
        public CacheFilter(params object[] @params) : base(typeof(CacheFilterImp))
        {
            Arguments = @params;
        }

        public class CacheFilterImp : IResourceFilter
        {
            private Type _objectType;

            public CacheFilterImp(params object[] @params)
            {
                if(!(@params[0] is Type))
                {
                    throw new Exception("Typ != Type");
                }
                _objectType = (Type)@params[0];
            }

            public void OnResourceExecuted(ResourceExecutedContext context)
            {

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
}
