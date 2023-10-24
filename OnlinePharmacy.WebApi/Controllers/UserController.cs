using Microsoft.AspNetCore.Mvc;
using ScriptEase.OnlinePharmacy.Data.DataModels;
using ScriptEase.OnlinePharmacy.Data.Repositories;

namespace ScriptEase.OnlinePharmacy.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    public class UsersControllerV1 : BaseController<User, UserRepository>
    {
        public UsersControllerV1(UserRepository userRepository) : base(userRepository)
        {
        }

        // Implement specific features for V1 if necessary
    }

    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    public class UsersControllerV2 : BaseController<User, UserRepository>
    {
        public UsersControllerV2(UserRepository userRepository) : base(userRepository)
        {
        }

        // Implement specific features for V2 if necessary
    }
}
