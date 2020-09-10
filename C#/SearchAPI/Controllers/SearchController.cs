using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
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
        private readonly ILogger<SearchController> logger;
        private readonly QueryHandler queryHandler;

        public SearchController(ILogger<SearchController> logger, QueryHandler queryHandler)
        {
            this.logger = logger;
            this.queryHandler = queryHandler;
        }

        [HttpGet]
        [Route("raw")]
        public ActionResult<string> SearchQuery([FromBody] string rawQuery)
        {
            var searchResponse = queryHandler.ProcessQuery(rawQuery);

            return ResponseValidator.IsSearchSuccessful(searchResponse)
                ? BuildResult(searchResponse.Documents)
                : BadRequest(searchResponse.ApiCall.OriginalException.Message);
        }

        [HttpPost]
        public IEnumerable<Document> GetSearchResult([FromBody] QueryWrapper queryWrapper)
        {
            return queryHandler.ProcessQuery(queryWrapper.RawQuery).Documents.ToArray();
        }
        private ActionResult<string> BuildResult(IEnumerable<Document> responseDocuments)
        {
            return responseDocuments.Any()
                ? (ActionResult<string>)Ok(
                    $"Query was found in {responseDocuments.Select(doc => doc.FileName).Aggregate((x, y) => $"{x}, {y}")}")
                : NotFound("Query wasn't found");
        }
    }

    public class QueryWrapper
    {
        [JsonPropertyName("rawQuery")]
        public string RawQuery { get; set; }
    }
}