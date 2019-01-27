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

        private async Task LogResponse(Task<string> readAsStringAsync)
        {
            try
            {
                var log = await readAsStringAsync;
                Debug.Log($"[{DateTime.Now:HH:mm:ss}]{log}");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}
