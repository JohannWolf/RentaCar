using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RentaCar.Data;
using RentaCar.DTOs;
using RentaCar.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace RentaCar.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        public static Users user = new Users();
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        public AuthController(ILogger<AuthController> logger, IConfiguration configuration , DataContext context)
        {
            _logger = logger;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register"), Authorize(Roles = "Admin")]
        public async Task<ActionResult<Users>> Register(UserDTO request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Role = request.Role;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDTO request)
        {
            Users userfound = await _context.Users.Where(r => r.Email == request.Email).FirstOrDefaultAsync();
            if (userfound == null)
            {
                return BadRequest("Email not found.");
            }
            else
            {

                if (!VerifyPasswordHash(request.Password, userfound.PasswordHash, userfound.PasswordSalt))
                {
                    return BadRequest("Wrong password.");
                }
                else
                {
                    string token = CreateToken(userfound);

                    var refreshToken = GenerateRefreshToken();
                    SetRefreshToken(refreshToken);

                    return Ok(token);
                }
            }
        }


        [HttpPut("update")]
        public async Task<ActionResult<List<Users>>> UpdateUser(Users request)
        {
            var editUser = await _context.Users.FindAsync(request.UserId);
            if (editUser == null)
                return BadRequest("User not found.");

            editUser.LastName = request.FirstName;
            editUser.LastName = request.LastName;

            await _context.SaveChangesAsync();

            return Ok(editUser);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            } 
        }

        private string CreateToken(Users user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private DTOs.RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new DTOs.RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(DTOs.RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            user.RefreshToken = newRefreshToken.Token;
            user.TokenCreated = newRefreshToken.Created;
            user.TokenExpires = newRefreshToken.Expires;
        }
    }
}
