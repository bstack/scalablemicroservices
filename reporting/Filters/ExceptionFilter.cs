using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reporting.Filters
{
	public class ExceptionFilter : IExceptionFilter
	{
		public void OnException(
			ExceptionContext context)
		{
			if (context.Exception is AggregateException _aggregateException)
			{
				var _exceptionDetails = new StringBuilder("InnerExceptions: ");
				_aggregateException.InnerExceptions.ToList().ForEach(
					innerException => _exceptionDetails.Append($"{innerException.ToString().Replace(Environment.NewLine, " ")} ||"));

				Console.WriteLine($"Unhandled exception processing {context.HttpContext.Request.Method} for {context.HttpContext.Request.Path}: " +
					$"AggregateException: {_aggregateException.ToString() } {_exceptionDetails.ToString()}");
			}
			else
			{
				Console.WriteLine($"Unhandled exception processing {context.HttpContext.Request.Method} for {context.HttpContext.Request.Path}: " +
					$"{context.Exception.ToString()}");
			}

			context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
		}
	}
}
