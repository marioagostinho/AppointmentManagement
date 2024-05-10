using Microsoft.AspNetCore.Mvc;

namespace Team.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BaseAPIController : Controller
    {
    }
}
