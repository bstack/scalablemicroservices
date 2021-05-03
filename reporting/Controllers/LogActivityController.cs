using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reporting.Models;
using System;
using System.Linq;

namespace reporting.Controllers
{
    [Route("api/[controller]")]
    public class LogActivityController : ControllerBase
    {
        private readonly Data.ILogActivityRepository c_logActivityRepository;



        public LogActivityController(
            [FromServices] Data.ILogActivityRepository logActivityRepository)
        {
            this.c_logActivityRepository = logActivityRepository;
        }


        [HttpPost]
        public IActionResult Post(
            [FromBody] Models.LogActivityRequest logActivityRequest)
        {
            var _requestId = Request.Headers["X-Request-Id"].ToString();
            var _correlationId = Request.Headers["X-Correlation-Id"].ToString();

            var _logActivity = new Entity.LogActivity(
                Guid.Parse(_correlationId),
                Guid.Parse(_requestId),
                logActivityRequest.Service,
                logActivityRequest.Activity,
                logActivityRequest.ActivityDetail);

            this.c_logActivityRepository.Log(_logActivity);

            return this.StatusCode(StatusCodes.Status201Created);
        }


        [HttpGet]
        public IActionResult Get()
        {
            var _logActivityData = this.c_logActivityRepository.GetRecentLogs();
            var _logActivityModel = _logActivityData.Select(
                item => new LogActivity()
                {
                    Id = item.Id,
                    CorrelationId = item.CorrelationId,
                    RequestId = item.RequestId,
                    Service = item.Service,
                    Activity = item.Activity,
                    ActivityDetail = item.ActivityDetail,
                    Timestamp = item.Timestamp
                });
    
            return this.StatusCode(StatusCodes.Status200OK, _logActivityModel);
        }
    }
}
