using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Web.Middlewares
{
    public class BasketTransfer
    {
        private readonly RequestDelegate _next;

        public BasketTransfer(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext, IBasketService basketService)
        {
            string anonymousId = httpContext.Request.Cookies[Constants.BASKET_COOKIENAME];
            string userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != null && anonymousId != null)
            {
                await basketService.TransferBasketAsync(anonymousId, userId);
                httpContext.Response.Cookies.Delete(Constants.BASKET_COOKIENAME);
            }
            await _next(httpContext);
        }
    }

    public static class BasketTransferExtensions
    {
        public static void UseBasketTransfer(this IApplicationBuilder app)
        {
            app.UseMiddleware<BasketTransfer>();
        }
    }
}
