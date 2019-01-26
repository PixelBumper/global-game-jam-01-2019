using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GeneratedServerAPI
{
    public partial class Client
    {
        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, string url)
        {
            var requestInfo = $"{request.Method} {request.RequestUri}\n";
        }

        partial void PrepareRequest(HttpClient client, HttpRequestMessage request, StringBuilder urlBuilder)
        {
            var requestInfo = $"{request.Method} {request.RequestUri}\n";
        }

        partial void ProcessResponse(HttpClient client, HttpResponseMessage response)
        {
            LogResponse(response.Content.ReadAsStringAsync());
        }

        private async void LogResponse(Task<string> readAsStringAsync)
        {
            try
            {
                Debug.Log(await readAsStringAsync);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
