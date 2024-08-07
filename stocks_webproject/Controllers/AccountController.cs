using Domain_or_abstractionLayer.Interface;
using Domain_or_abstractionLayer.modelsDTO.Account;
using Microsoft.AspNetCore.Mvc;

namespace stocks_webproject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(IAuthRepository authRepo):ControllerBase
    {
        [HttpPost]
        [Route("registered")]
        public async Task<IActionResult> RegisterAsync(RegistrationModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var result =  await authRepo.RegistrationAsync(model);
            if(!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }


            return Ok(result);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LoginAsync(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await authRepo.LoginAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

    }
}
