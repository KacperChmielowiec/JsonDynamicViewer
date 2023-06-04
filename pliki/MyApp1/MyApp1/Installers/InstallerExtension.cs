using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MyApp1.Installers
{
    public static class InstallerExtension
    {
        public static void InstallServices(this IServiceCollection services, IConfiguration conf, IEnumerable<IInstaller> collection)
        {
            foreach(var installer in collection)
            {
                installer.InstallServices(services,conf);
            }
        }
    }
}
