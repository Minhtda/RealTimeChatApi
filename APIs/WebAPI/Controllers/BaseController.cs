using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
    }
}
