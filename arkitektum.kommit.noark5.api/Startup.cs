﻿using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens;

[assembly: OwinStartup(typeof(arkitektum.kommit.noark5.api.Startup))]

namespace arkitektum.kommit.noark5.api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "implicitclient",
                Authority = "http://localhost:3333/core",
                RedirectUri = "http://localhost:49708/",
                ResponseType = "id_token token",
                Scope = "openid email",

                SignInAsAuthenticationType = "Cookies",

                // sample how to access token on form (for token response type)
                //Notifications = new OpenIdConnectAuthenticationNotifications
                //{
                //    MessageReceived = async n =>
                //        {
                //            var token = n.ProtocolMessage.Token;

                //            if (!string.IsNullOrEmpty(token))
                //            {
                //                n.OwinContext.Set<string>("idsrv:token", token);
                //            }
                //        },
                //    SecurityTokenValidated = async n =>
                //        {
                //            var token = n.OwinContext.Get<string>("idsrv:token");

                //            if (!string.IsNullOrEmpty(token))
                //            {
                //                n.AuthenticationTicket.Identity.AddClaim(
                //                    new Claim("access_token", token));
                //            }
                //        }
                //}
            });
        }
    }
}