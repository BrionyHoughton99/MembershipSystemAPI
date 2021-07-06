using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipSystem.Api.Filters
{
    //needs to inherit exception filter attribute to use the resources
    public class MyExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<MyExceptionFilter> logger;
        //add custom filter name to ILogger parameter 
        //custom filter name is configured in startup class for global use
        public MyExceptionFilter(ILogger<MyExceptionFilter> logger)
        {
            this.logger = logger;
        }

        //adding logger error message for when an exception is added 
        public override void OnException(ExceptionContext context)
        {
            //logs the message
            logger.LogError(context.Exception, context.Exception.Message);

            base.OnException(context);
        }
    }
}