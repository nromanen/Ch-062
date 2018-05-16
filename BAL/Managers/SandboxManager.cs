using Model.Entity;
using BAL.Interfaces;
using RestSharp;
using System;

namespace BAL.Managers
{
    public class SandboxManager : ISandboxManager
    {
        public ExecutionResult Execute(string code)
        {
            var client = new RestClient("http://localhost:62543/");
            var request = new RestRequest("api/values/validate", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(code);
            IRestResponse response = client.Execute(request);
            var content = response.Content;

            return new ExecutionResult();
        }
    }
}
