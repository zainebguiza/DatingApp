using API.DTOs;
using API.Entities;
using API.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController:BaseApiController
    {   
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper ;
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager,ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _mapper= mapper;
            _tokenService = tokenService;
        }
        
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register (RegisterDto registerDto){
            if(await UserExists(registerDto.UserName)){
                return BadRequest("UserName is taken");
            }
            
            var user =_mapper.Map<AppUser>(registerDto);        
            user.UserName=registerDto.UserName.ToLower();
            var result= await _userManager.CreateAsync(user,registerDto.Password);
            if(!result.Succeeded)return BadRequest(result.Errors);
            
            var roleResult= await _userManager.AddToRoleAsync(user,"Member");
            if(!roleResult.Succeeded) return BadRequest(result.Errors);
;            return new UserDto{
                username=user.UserName,
                Token=await  _tokenService.CreateToken(user),
                KnownsAs=user.KnownAs,
                Gender=user.Gender
            };
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user=await _userManager.Users.Include(p=>p.Photos)
            .SingleOrDefaultAsync(x=>
            x.UserName==loginDto.Username);
            if(user==null) return Unauthorized("invalid username");
            var result=await _userManager.CheckPasswordAsync(user,loginDto.Password);
            if(!result)return Unauthorized("Invalid password");
            
             return new UserDto{
                username=user.UserName,
                Token=await _tokenService.CreateToken(user),
                PhotoUrl=user.Photos.FirstOrDefault(x=>x.IsMain)?.Url,
                KnownsAs=user.KnownAs,
                Gender=user.Gender
            };
        }

        private async Task<bool> UserExists(string username){
            return await _userManager.Users.AnyAsync(x=>x.UserName==username.ToLower());
        }
    }
}