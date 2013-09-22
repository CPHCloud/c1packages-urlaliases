using Composite.Core.Application;
using Composite.Data;
using Composite.Data.DynamicTypes;
using CphCloud.Packages.UrlAlias.Data;

namespace CphCloud.Packages.UrlAlias
{
    [ApplicationStartup]
    public class UrlAliastartupHandler
    {
        public static void OnBeforeInitialize() {}

        public static void OnInitialized()
        {
            DynamicTypeManager.EnsureCreateStore(typeof(IUrlAlias));
            DataEvents<IUrlAlias>.OnBeforeAdd += (UrlAliasBeforeWrite);
            DataEvents<IUrlAlias>.OnBeforeUpdate += (UrlAliasBeforeWrite);
        }

        public static void UrlAliasBeforeWrite(object sender, DataEventArgs eventArgs)
        {
            var urlAlias = eventArgs.Data as IUrlAlias;
            urlAlias.UrlAlias = "/" + urlAlias.UrlAlias.Trim(new[] { '/' });
            urlAlias.RedirectLocation = urlAlias.RedirectLocation.StartsWith("http") ? urlAlias.RedirectLocation
                : "/" + urlAlias.RedirectLocation.Trim(new[] { '/' });
        }
    }
}
