using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyApi.Presentation.Controllers
{
    [ApiController, Authorize(Roles = "admin")]
    public class BaseApiController : ControllerBase
    {

    }
}
