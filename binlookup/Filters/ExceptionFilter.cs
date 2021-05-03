using binlookup.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binlookup.Filters
{
	public class ExceptionFilter : IExceptionFilter
	{
		public void OnException(
			ExceptionContext context)
		{
			Error _errorInformation;

			if (context.Exception is AggregateException _aggregateException)
			{
				var _exceptionDetails = new StringBuilder("InnerExceptions: ");
				_aggregateException.InnerExceptions.ToList().ForEach(
					innerException => _exceptionDetails.Append($"{innerException.ToString().Replace(Environment.NewLine, " ")} ||"));

				_errorInformation = new Error($"Unhandled exception processing {context.HttpContext.Request.Method} for {context.HttpContext.Request.Path}: " +
					$"AggregateException: {_aggregateException.ToString() } {_exceptionDetails.ToString()}");
				// TODO: SEND _reportingEntry to a reporting service
			}
			else
			{
				_errorInformation = new Error($"Unhandled exception processing {context.HttpContext.Request.Method} for {context.HttpContext.Request.Path}: " +
					$"{context.Exception.ToString()}");
			}

			context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
		}
	}
}
