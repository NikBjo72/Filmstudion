using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SFF.API.Persistence.Interfaces;
using SFF.API.Services.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SFF.API.Domain.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            if (userId != null)
            {
                // lägger till användare i context vid lyckad validering
                context.Items["User"] = userService.GetById(userId);
            }

            await _next(context);
        }
    }
}