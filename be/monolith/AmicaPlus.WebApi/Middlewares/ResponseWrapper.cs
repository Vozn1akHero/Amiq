﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace AmicaPlus.Middlewares
{
    public class ResponseWrapper
    {
        private readonly RequestDelegate _next;

        public ResponseWrapper(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var currentBody = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                //set the current response to the memorystream.
                context.Response.Body = memoryStream;

                await _next(context);

                //reset the body 
                context.Response.Body = currentBody;
                memoryStream.Seek(0, SeekOrigin.Begin);

                var readToEnd = new StreamReader(memoryStream).ReadToEnd();
                //var objResult = JsonConvert.DeserializeObject(readToEnd);
                var objResult = JsonSerializer.Deserialize(readToEnd, typeof(object));
                var result = CommonApiResponse.Create((HttpStatusCode)context.Response.StatusCode, objResult, null);
                await context.Response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }

    }

    public static class ResponseWrapperExtensions
    {
        public static IApplicationBuilder UseResponseWrapper(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseWrapper>();
        }
    }


    public class CommonApiResponse
    {
        public static CommonApiResponse Create(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            return new CommonApiResponse(statusCode, result, errorMessage);
        }

        public string Version => "1.2.3";

        public int StatusCode { get; set; }
        public string RequestId { get; }

        public string ErrorMessage { get; set; }

        public object Result { get; set; }

        protected CommonApiResponse(HttpStatusCode statusCode, object result = null, string errorMessage = null)
        {
            RequestId = Guid.NewGuid().ToString();
            StatusCode = (int)statusCode;
            Result = result;
            ErrorMessage = errorMessage;
        }
    }
}