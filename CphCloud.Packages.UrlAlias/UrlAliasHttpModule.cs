using System;
using System.Text;
using System.Web;
using Composite.Core.Logging;
using Composite.Data;
using System.Linq;
using Composite.Data.Types;
using CphCloud.Packages.UrlAlias.Data;

namespace CphCloud.Packages
{
    public class UrlAliasHttpModule : IHttpModule
    {
        public void Init(HttpApplication httpApplication)
        {
            httpApplication.BeginRequest += (httpApplication_BeginRequest);
        }

        static void httpApplication_BeginRequest(object sender, EventArgs e)
        {
            var httpApplication = (HttpApplication)sender;
            var incomingUrlPath = HttpUtility.UrlDecode(httpApplication.Context.Request.Url.AbsolutePath.TrimEnd(new[] { '/' }));

            try
            {
                if (httpApplication.Request.Url.AbsolutePath.Equals("/UrlAlias/Preview"))
                {
                    var location = HttpUtility.UrlDecode(httpApplication.Request["p"]);
                    httpApplication.Response.Redirect(location);
                }
                else
                {
                    using (var conn = new DataConnection())
                    {
                        var matchingUrlAlias =
                            conn.Get<IUrlAlias>()
                                .SingleOrDefault(x => x.UrlAlias.Equals(incomingUrlPath, StringComparison.OrdinalIgnoreCase));

                        if (matchingUrlAlias != null 
                            && matchingUrlAlias.Enabled 
                            && (matchingUrlAlias.Hostname == Guid.Empty || conn.Get<IHostnameBinding>().Single(x => x.Id == matchingUrlAlias.Hostname).Hostname == httpApplication.Request.Url.Host))
                        {
                            httpApplication.Response.Clear();
                            httpApplication.Response.StatusCode = matchingUrlAlias.HttpStatusCode;
                            httpApplication.Response.RedirectLocation = matchingUrlAlias.RedirectLocation;
                            httpApplication.Response.End();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggingService.LogError("UrlAlias", ex);
                throw;
            }
        }

        public void Dispose() { }
    }
}
