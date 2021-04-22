using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Parvaryk.Application.Commands.Business;
using Parvaryk.Application.Commands.Entity.User;
using Parvaryk.Contracts;
using Parvaryk.Contracts.Models.Dto;
using Parvaryk.Contracts.Models.Request;
using System.Threading.Tasks;

namespace Parvaryk.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ApiController
    {
        private readonly IUserDetails _userDetails;
        private readonly IMapper _mapper;

        public UserController(IUserDetails userDetails, IMapper mapper)
        {
            _userDetails = userDetails;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<int> SignUp([FromBody] SignUpRequest request)
        {
            return await Mediator.Send(_mapper.Map<SignUpCommand>(request));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<UserDto> SignIn([FromBody] SignInRequest request)
        {
            await HttpContext.SignInAsync(await Mediator.Send(_mapper.Map<SignInCommand>(request)));

            return await Mediator.Send(new GetUserByUsernameCommand { Username = request.Username });
        }

        [HttpGet]
        public async Task<UserDto> GetCurrentUser()
        {
            return await Mediator.Send(new GetUserByUsernameCommand { Username = _userDetails.Username });
        }
    }
}
