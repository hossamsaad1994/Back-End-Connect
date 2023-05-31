using connect_.Models;
using connect_.Secure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace connect_.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<User> _userManager;
        private readonly JWT _JWt;

        public AuthServices(UserManager<User> userManager, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _JWt = jwt.Value;
        }

        public async Task<AuthModel> GetTokenAsync(TokenRequestModel model)
        {
            var authmodel = new AuthModel();
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authmodel.Message = "Email or Password is incorrect";
                return authmodel;
            }

            var JwtSecurityToken = await CreatJwtToken(user);
            var roleslist = await _userManager.GetRolesAsync(user);

            authmodel.IsAuthenticated = true;
            authmodel.Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken);
            authmodel.Email = user.Email;
            authmodel.Username = user.UserName;
            authmodel.ExpiresOn = JwtSecurityToken.ValidTo;
            authmodel.Roles = roleslist.ToList();

            return authmodel;
        }

        public async Task<AuthModel> RegisterationAsync(RegisterationModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is Already used" };

            if (await _userManager.FindByNameAsync(model.Username) is not null)
                return new AuthModel { Message = "Username is Already used" };

            var user = new User
            {
                UserName = model.Username,
                Email = model.Email,
                FirstName = model.Firstname,
                LastName = model.Lastname,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthModel { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, "user");
            var JwtSecurityToken = await CreatJwtToken(user);

            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = JwtSecurityToken.ValidTo,
                IsAuthenticated = true,
                Roles = new List<string> { "user" },
                Token = new JwtSecurityTokenHandler().WriteToken(JwtSecurityToken),
                Username = user.UserName
            };

        }

        private async Task<JwtSecurityToken> CreatJwtToken(User user)
        {
            var userclaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleclaims = new List<Claim>();

            foreach (var role in roles)
                roleclaims.Add(new Claim("roles", role));
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userclaims)
            .Union(roleclaims);

            var SymmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWt.Key));
            var SigningCredentials = new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var JwtSecurityToken = new JwtSecurityToken(
                issuer: _JWt.Issuer,
                audience: _JWt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_JWt.DurationInDays),
                signingCredentials: SigningCredentials);

            return JwtSecurityToken;
        }
    }
}
