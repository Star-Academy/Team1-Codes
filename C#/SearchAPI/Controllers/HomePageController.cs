using Microsoft.AspNetCore.Mvc;

namespace FirstTimeWeb.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class HomePageController
    {
        public string Welcome()
        {
            return "Yo wassup?\n" +
                   "You could search using /Search/raw and fill the Body with the rawQuery\n" +
                   "Or using /Search and fill the Body with your query object (like Models.SearchPattern)\n" +
                   "But it's not implemented yet ._.";
        }
    }
}