using System.IO;
using Microsoft.Extensions.Configuration;

namespace Keycloak.Net.Tests
{
    public partial class KeycloakClientShould
    {
        private readonly KeycloakClient _client;

        public KeycloakClientShould()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            string url = "https://local-nm-hyperv"; //;
            string userName = "admin"; //configuration["userName"];
            string password = "admin"; //configuration["password"];

            _client = new KeycloakClient(url, userName, password);
        }
    }
}