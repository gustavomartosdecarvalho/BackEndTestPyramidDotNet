using System;
using System.Net;
using Xunit;
using RestSharp;

namespace integration_tests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        var client = new RestClient("https://swapi.dev/api/");
        var request = new RestRequest("people/1/", Method.Get);
        var response = client.Execute(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    }

    [Fact]
    public async Task Test2Async()
    {
        ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        var client = new RestClient("https://localhost:7275/");
        var request = new RestRequest("petcontrol/alimentacao/Animal/1/", Method.Get);
        request.AddJsonBody("application/json");
        var response = await client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}