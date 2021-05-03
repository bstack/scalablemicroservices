using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binlookup.Filters
{
    public class ModelStateValidationFilter : ActionFilterAttribute
	{
		public override void OnActionExecuting(
			ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				var _errorsAsPipeSeperatedString = new StringBuilder();
				context.ModelState
					.Where(entry => entry.Value.Errors.Count > 0)
					.SelectMany(entry => entry.Value.Errors)
					.Select(error => error.ErrorMessage)
					.ToList()
					.ForEach(errorMessage => _errorsAsPipeSeperatedString.Append($"{errorMessage}|"));

				// TODO: SEND these errors to a reporting service

				context.Result = new StatusCodeResult(StatusCodes.Status400BadRequest);
			}
		}
	}
}

