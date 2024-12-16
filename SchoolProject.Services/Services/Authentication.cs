using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SchoolProject.Data.Entities;
using SchoolProject.Data.Helper;
using SchoolProject.Infrastructure.Interfaces;
using SchoolProject.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolProject.Services.Services
{
    public class Authentication : IAuthenticationServices
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<User> _userManager;

        public Authentication(JwtSettings jwtSettings, IRefreshTokenRepository refreshTokenRepository, UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
        }
        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var (jwtToken, accessToken) = GenerateJWTToken(user);



            //refresh token
            var refreshToken = new RefreshToken
            {
                ExpireAt = DateTime.Now.AddDays(20),
                UserName = user.UserName,
                TokenString = GenerateRefreshToken()
            };

            //To Save refresh token in Database
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(20),
                IsUsed = true,
                IsRevoked = false,
                JwtId = jwtToken.Id,
                RefreshToken = refreshToken.TokenString,
                Token = accessToken,
                UserId = user.Id,
            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);

            return new JwtAuthResult
            {
                AccessToken = accessToken,
                refreshToken = refreshToken
            };

        }

        private (JwtSecurityToken, string) GenerateJWTToken(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email) , user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),
            };

            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(2),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return (jwtToken, accessToken);
        }

        public async Task<JwtAuthResult> GetRefreshToken(string accessToken, string RefreshToken)
        {
            //Read Token to get Claims
            var token = ReadJWTToken(accessToken);
            if (token == null || !token.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new SecurityTokenException("Algorithim is wrong");
            }
            if (token.ValidTo > DateTime.UtcNow)
            {
                throw new SecurityTokenException("Token is not Expired");
            }

            //get  UserRefreshToken from Table
            var userId = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;

            var userr = _refreshTokenRepository.GetTableNoTracking().FirstOrDefault(x => x.Token == accessToken && x.RefreshToken == RefreshToken && x.UserId == userId);

            if (userr == null)
            {
                throw new SecurityTokenException("Token is null");
            }
            // hena ana bashof lw expire date beta3 el refresh Token 5ls wla l2a
            if (userr.ExpiryDate < DateTime.UtcNow)
            {
                userr.IsRevoked = true;
                userr.IsUsed = false;
                await _refreshTokenRepository.UpdateAsync(userr);
                throw new SecurityTokenException("Refresh Token is Expired");
            }

            //find user by id from UserTable
            var User = _userManager.FindByIdAsync(userId).Result;
            if (User == null)
            {
                throw new SecurityTokenException("User not found");
            }

            var (jwtSecurityToken, newToken) = GenerateJWTToken(User);

            //generate another refreshtoken
            var RefreshTokenResult = new RefreshToken
            {
                ExpireAt = userr.ExpiryDate,
                UserName = token.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value,
                TokenString = RefreshToken
            };

            return new JwtAuthResult
            {
                AccessToken = newToken,
                refreshToken = RefreshTokenResult
            };
        }

        private JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();

            var validateParams = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
            };

            var validator = handler.ValidateToken(accessToken, validateParams, out SecurityToken validatedToken);
            try
            {
                if (validator == null)
                {
                    throw new SecurityTokenException("Invalid Token");
                }
            }
            catch (Exception ex)
            {
                throw new SecurityTokenException("Error");
            }

            var response = handler.ReadJwtToken(accessToken);
            return response;
        }

        private string GenerateRefreshToken()
        {
            var randomeNumber = new byte[32];
            var generateNumberNumber = RandomNumberGenerator.Create();
            generateNumberNumber.GetBytes(randomeNumber);
            return Convert.ToBase64String(randomeNumber);
        }
    }
}
