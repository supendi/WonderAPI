using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WonderAPI.Controllers.Account;
using WonderAPI.Entities;

namespace WonderAPI
{
    /// <summary>
    /// Wires business services instance. Managing DI for controllers.
    /// </summary>
    public class WireDI
    {
        /// <summary>
        /// Member controller dependencies injection
        /// </summary>
        /// <param name="services"></param>
        public static void WireMemberService(IServiceCollection services)
        {
            services.AddScoped<WonderDBContext, WonderDBContext>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IPasswordHasher, BCryptHasher>();
            services.AddScoped<ITokenGenerator, JWTGenerator>();
            services.AddScoped<MemberService, MemberService>();
        }
    }
}
