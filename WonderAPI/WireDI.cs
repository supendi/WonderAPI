using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WonderAPI.Controllers.Account;
using WonderAPI.Controllers.Account.Auth;
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
        /// Injects jwt handler
        /// </summary>
        private void InjectTokenHandler()
        {
            services.AddScoped<ISecurityTokenHandler, JWTHandler>();
        }

        /// <summary>
        /// Injects db context
        /// </summary>
        private void InjectDBContext()
        {
            services.AddScoped<WonderDBContext, WonderDBContext>();
        }

        /// <summary>
        /// Inject member repo
        /// </summary>
        private void InjectMemberRepo()
        {
            services.AddScoped<IMemberRepository, MemberRepository>();
        }

        /// <summary>
        /// Inject token handler
        /// </summary>
        private void InjectJWTHandler()
        {
            services.AddScoped<ISecurityTokenHandler, JWTHandler>();
        }

        /// <summary>
        /// Inject password hasher
        /// </summary>
        private void InjectPasswordHasher()
        {
            services.AddScoped<IPasswordHasher, BCryptHasher>();
        }

        /// <summary>
        /// Inject token repo
        /// </summary>
        private void InjectTokenRepo()
        {
            services.AddScoped<ITokenRepository, TokenRepository>();
        }

        /// <summary>
        /// Member controller dependencies injection
        /// </summary>
        /// <param name="services"></param>
        private void WireMemberService()
        {
            services.AddScoped<MemberService, MemberService>();
        }

        /// <summary>
        /// Auth controller dependencies injection
        /// </summary>
        /// <param name="services"></param>
        private void WireAuthService()
        {
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<AuthService, AuthService>();
        }

        /// <summary>
        /// Inject all dependencies to all controller
        /// </summary>
        public void DoInjection()
        {
            InjectTokenHandler();
            InjectDBContext();
            InjectMemberRepo();
            InjectJWTHandler();
            InjectPasswordHasher();
            WireMemberService();
            WireAuthService();
        }
    }
}
