using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Queue_WebApi.Auth;

namespace Queue_WebApi.Controllers
{
    [Authorize]
    [ApiController]
    public class GetUserController : ControllerBase
    {
        private readonly IUserProvider _userProvider;

        public GetUserController(IUserProvider userProvider)
        {
            _userProvider = userProvider ?? throw new ArgumentNullException(nameof(userProvider));
        }
        [HttpGet("GetUserIdInJwt")]
        public ActionResult<Guid> GetUserIdByJwt()
        {
            
            var userId = _userProvider.GetUserId();
            if (userId == null)
            {
                return Unauthorized("User ID not found.");
            }
            return Ok(userId);
        }
    }

}
