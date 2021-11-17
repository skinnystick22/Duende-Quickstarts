using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace Duende.Quickstart.Client;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .WriteTo.Console(
                outputTemplate:
                "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                theme: AnsiConsoleTheme.Code)
            .CreateLogger();

        var client = new HttpClient();
        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
        if (disco.IsError)
        {
            Log.Error(disco.Exception, "Issue getting discovery document");
            return 1;
        }

        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "client",
            ClientSecret = "secret",
            Scope = "api1"
        });
        if (tokenResponse.IsError)
        {
            Log.Error(tokenResponse.Exception, "Error getting access_token: {Error}", tokenResponse.Error);
            return 1;
        }

        Log.Information("Response: {Token}", tokenResponse.Json);

        var apiClient = new HttpClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        try
        {
            var response = await apiClient.GetAsync("https://localhost:6001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Log.Error("An error occured calling the API, {ApiError}", response.StatusCode);
                return 1;
            }

            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));

            return 0;
        }
        catch (HttpRequestException rE)
        {
            Log.Error(rE, "An error occured in HTTP Request Message");
            return 1;
        }
        catch (Exception e)
        {
            Log.Error(e, "An unexpected error occured");
            return 1;
        }
    }
}