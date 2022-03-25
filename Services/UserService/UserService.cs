using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Bilog.Utility;

using bilog.Data;
using bilog.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;

namespace bilog.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IRegexUtilities _regexUtilities;
        private readonly IConfiguration _configuration;
        public UserService(DataContext context, IRegexUtilities regexUtilities, IConfiguration configuration)
        {
            _configuration = configuration;
            _regexUtilities = regexUtilities;
            _context = context;
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (email == null || password == null)
            {
                response.Success = false;
                response.Message = "Email ve şifre boş olamaz.";
                return response;
            }

            if (email.Equals("") || password.Equals(""))
            {
                response.Success = false;
                response.Message = "Email ve şifre boş olamaz.";
                return response;
            }

            try
            {
                User user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()) || x.Username.ToLower().Equals(email.ToLower()));
                if (user == null)
                {
                    response.Success = false;
                    response.Message = "Kullanıcı adı veya şifre yanlış.";
                    return response;
                }
                else if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                {
                    response.Success = false;
                    response.Message = "Kullanıcı adı veya şifre yanlış.";
                    return response;
                }
                else
                {
                    response.Data = CreateToken(user);
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<string>> Register(User user, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            if (!(await _regexUtilities.IsValidEmail(user.Email)))
            {
                response.Success = false;
                response.Message = "Lütfen geçerli bir email adresi girin.";
                return response;
            }

            if (user.Email == null || user.Username == null || password == null)
            {
                response.Success = false;
                response.Message = "Lütfen tüm alanları doldurun.";
                return response;
            }

            if (user.Email.Equals("") || user.Username.Equals("") || password.Equals(""))
            {
                response.Success = false;
                response.Message = "Lütfen tüm alanları doldurun.";
                return response;
            }

            if (await UserExist(user.Username, user.Email))
            {
                response.Success = false;
                response.Message = "Kullanıcı adı veya email zaten kayıtlı.";
                return response;
            }

            try
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.TimeCreated = DateTime.UtcNow;
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                response = await Login(user.Email, password);
                return response;
            }
            catch (System.Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                throw;
            }
        }

        public async Task<bool> UserExist(string username, string email)
        {
            if (await _context.Users.AnyAsync(x => (x.Username.ToLower() == username.ToLower() || x.Email.ToLower() == email)))
            {
                return true;
            }

            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)); //UTF8 donusumu turkce karakter iceren sifreler icin sikinti cikarabilir 
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey
            (
                Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value)
            );

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddYears(2), //5 gunde bir yeniden giris
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}