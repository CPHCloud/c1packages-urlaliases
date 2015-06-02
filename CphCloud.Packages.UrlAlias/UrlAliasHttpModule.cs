using Composite.Core.Logging;
using Composite.Data;
using Composite.Data.Types;
using CphCloud.Packages.UrlAlias.Data;
using System;
using System.Linq;
using System.Threading;
using System.Web;

namespace CphCloud.Packages.UrlAlias
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
            var incomingUrlPath = HttpUtility.UrlDecode(httpApplication.Context.Request.Url.PathAndQuery.TrimEnd(new[] { '/' }));

            try
            {
                if (httpApplication.Request.Url.AbsolutePath.StartsWith("/Composite",
                                                                        StringComparison.InvariantCultureIgnoreCase))
                {
                    // Let's leave Composite alone so the user aren't able to lock themselves out of the console.
                    return;
                }
                else if (httpApplication.Request.Url.AbsolutePath == "/UrlAlias/Preview")
                {
                    var location = HttpUtility.UrlDecode(httpApplication.Request["p"]);
                    httpApplication.Response.Redirect(location, false);
                }
                else
                {
                    using (var conn = new DataConnection())
                    {
                        var hostnameId = conn.Get<IHostnameBinding>().Single(x => x.Hostname == httpApplication.Request.Url.Host).Id;
                        var matchingUrlAlias =
                            conn.Get<IUrlAlias>()
                                .SingleOrDefault(x => x.UrlAlias.ToLower() == incomingUrlPath.ToLower() && (x.Hostname == hostnameId || x.Hostname == Guid.Empty));

                        if (matchingUrlAlias != null
                            && matchingUrlAlias.Enabled)
                        {
                            matchingUrlAlias.LastUse = DateTime.Now;
                            matchingUrlAlias.UseCount++;
                            conn.Update(matchingUrlAlias);

                            httpApplication.Response.Clear();
                            httpApplication.Response.StatusCode = matchingUrlAlias.HttpStatusCode;
                            httpApplication.Response.RedirectLocation = matchingUrlAlias.RedirectLocation;
                            httpApplication.Response.End();
                        }
                    }
                }
            }
            catch (ThreadAbortException ex)
            {
                // Do nothing, this is expected
            }
            catch (Exception ex)
            {
                LoggingService.LogError("Url Aliases", ex);
            }
        }

        public void Dispose() { }
    }
}
