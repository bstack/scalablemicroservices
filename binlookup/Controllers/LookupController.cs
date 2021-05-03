using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace binlookup.Controllers
{
    [Route("api/[controller]")]
    public class LookupController : ControllerBase
    {
        private readonly Data.IBINLookupResultRepository c_BINLookupResultRepository;
        private readonly Data.IBINRepository c_BINRepository;
        private readonly Data.ITestHelperRepository c_testHelperRepository;


        public LookupController(
            [FromServices] Data.IBINLookupResultRepository BINLookupResultRepository,
            [FromServices] Data.IBINRepository BINRepository,
            [FromServices] Data.ITestHelperRepository testHelperRepository)
        {
            this.c_BINLookupResultRepository = BINLookupResultRepository;
            this.c_BINRepository = BINRepository;
            this.c_testHelperRepository = testHelperRepository;
        }


        [HttpPost]
        public IActionResult Post(
            [FromBody] Models.LookupRequest lookupRequest)
        {
            // For testing purposes only
            this.c_testHelperRepository.SeedDatabase();

            var _requestId = Request.Headers["X-Request-Id"].ToString();
            var _correlationId = Request.Headers["X-Correlation-Id"].ToString();
            var _reportingEntry = lookupRequest.ToString();


            // Idempotency check, if already there, return result
            var _previouslyCreatedBINLookupResult = this.c_BINLookupResultRepository.Get(_requestId);
            if(_previouslyCreatedBINLookupResult != null)
            {
                var _lookupResponse = new binlookup.Models.LookupResponse(
                    _previouslyCreatedBINLookupResult.CardScheme,
                    _previouslyCreatedBINLookupResult.Country,
                    _previouslyCreatedBINLookupResult.Currency);

                // TODO: SEND response to a reporting service
                return this.StatusCode(
                    StatusCodes.Status200OK,
                    _lookupResponse);
            }

            var _bins = this.c_BINRepository.GetAllBins();
            if (_bins.Count() == 0)
            {
                // TODO: SEND response to a reporting service

                return this.StatusCode(
                    StatusCodes.Status500InternalServerError);
            }

            var _matchedBin = _bins.SingleOrDefault(
                bin => lookupRequest.CardNumberBin >= bin.Low && lookupRequest.CardNumberBin <= bin.High);

            if (_matchedBin != null)
            {
                var _lookupResponse = new binlookup.Models.LookupResponse(
                    _matchedBin.CardScheme,
                    _matchedBin.Country,
                    _matchedBin.Currency);

                // TODO: SEND response to a reporting service
                // var _reportingResponseEntry = _lookupResponse.ToString();

                var _BINLookupResult = new Entity.BINLookupResult(
                    _requestId,
                    _correlationId,
                    _matchedBin.CardScheme,
                    _matchedBin.Country,
                    _matchedBin.Currency);
                this.c_BINLookupResultRepository.Save(_BINLookupResult);

                return this.StatusCode(
                    StatusCodes.Status201Created,
                    _lookupResponse);
            }
            else
            {
                // TODO: SEND response to a reporting service
                return this.StatusCode(
                    StatusCodes.Status404NotFound);
            }
        }
    }
}
