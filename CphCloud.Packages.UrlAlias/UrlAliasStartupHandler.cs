using Composite.Core.Application;
using Composite.Data;
using Composite.Data.DynamicTypes;
using CphCloud.Packages.UrlAlias.Data;

namespace CphCloud.Packages
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
            var UrlAlias = eventArgs.Data as IUrlAlias;
            UrlAlias.UrlAlias = "/" + UrlAlias.UrlAlias.Trim(new[] { '/' });
            UrlAlias.RedirectLocation = UrlAlias.RedirectLocation.StartsWith("http") ? UrlAlias.RedirectLocation
                : "/" + UrlAlias.RedirectLocation.Trim(new[] { '/' });
        }
    }
}
