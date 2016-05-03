using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IdentityModel.Tokens;
using Thinktecture.IdentityServer.v3.AccessTokenValidation;
using System.Web.Http;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Thinktecture.IdentityModel;
using System.Reflection;
using arkitektum.kommit.noark5.api.App_Start;

[assembly: OwinStartup(typeof(arkitektum.kommit.noark5.api.Startup))]

namespace arkitektum.kommit.noark5.api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            DependencyConfig.Configure(app);

            //JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();


            //app.UseIdentitiyServerSelfContainedToken(new SelfContainedTokenValidationOptions
            //{
            //    IssuerName = "https://identity.arkitektum.no",
            //    //SigningCertificate = X509.LocalMachine.TrustedPeople.SubjectDistinguishedName.Find("CN=identity.arkitektum.no", false).First()
            //    SigningCertificate = Certificate.Get()
            //});

            //app.UseIdentitiyServerReferenceToken(new ReferenceTokenValidationOptions
            //{
            //    TokenValidationEndpoint = "https://identity.arkitektum.no/core/connect/accessTokenValidation"
            //});

       
        }
    }


    static class Certificate
    {
        public static X509Certificate2 Get()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream("arkitektum.kommit.noark5.api.Config.identity_arkitektum_no.pfx"))
            {
                return new X509Certificate2(ReadStream(stream));
            }
        }

        private static byte[] ReadStream(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}