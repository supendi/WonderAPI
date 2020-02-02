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
    /// Wires business services instance. Manages DI for controllers.
    /// </summary>
    public class WireDI
    {
        IServiceCollection services;

        /// <summary>
        /// Creates new instance of WireDI
        /// </summary>
        /// <param name="services"></param>
        public WireDI(IServiceCollection services)
        {
            this.services = services;
        }

        /// <summary>
        /// Injects db context
        /// </summary>
        private void InjectDBContext()
        {
            services.AddScoped<WonderDBContext, WonderDBContext>();
        }

        /// <summary>
        /// Member controller dependencies injection
        /// </summary>
        /// <param name="services"></param>
        private void WireMemberService()
        {
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IPasswordHasher, BCryptHasher>();
            services.AddScoped<ITokenGenerator, JWTGenerator>();
            services.AddScoped<MemberService, MemberService>();
        }

        /// <summary>
        /// Inject all dependencies to all business services
        /// </summary>
        public void DoInjection()
        {
            InjectDBContext();
            WireMemberService();
        }
    }
}
