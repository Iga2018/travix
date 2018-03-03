using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace Travix.API.WebAPI.Filters
{
	public class GlobalErrorFilter : IExceptionFilter
	{
		private static Logger logger = LogManager.GetCurrentClassLogger("File");

		public void OnException(ExceptionContext context)
		{
			if (context.ExceptionHandled)
			{
				return;
			}
			logger.Error(context.Exception);
			context.Result = new StatusCodeResult(500);
			context.ExceptionHandled = true;
		}
	}
}
