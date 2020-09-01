using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstTimeWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SearchNest;
using SearchNest.Model;

namespace FirstTimeWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly QueryHandler _queryHandler;

        public SearchController(ILogger<SearchController> logger, QueryHandler queryHandler)
        {
            _logger = logger;
            _queryHandler = queryHandler;
        }

        [HttpGet]
        [Route("raw")]
        public ActionResult<string> SearchQuery([FromBody] string rawQuery)
        {
            var searchResponse = _queryHandler.ProcessQuery(rawQuery);

            return ResponseValidator.IsSearchSuccessful(searchResponse)
                ? BuildResult(searchResponse.Documents)
                : BadRequest(searchResponse.ApiCall.OriginalException.Message);
        }

        private ActionResult<string> BuildResult(IEnumerable<Document> responseDocuments)
        {
            return responseDocuments.Any()
                ? (ActionResult<string>) Ok(
                    $"Query was found in {responseDocuments.Select(doc => doc.FileName).Aggregate((x, y) => $"{x}, {y}")}")
                : NotFound("Query wasn't found");
        }
    }
}