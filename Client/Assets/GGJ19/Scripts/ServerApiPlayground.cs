using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using GeneratedServerAPI;
using GGJ19.Scripts.Server_Api;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class ServerApiPlayground: MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine("DispatchRequests");
            
        }

    }

    IEnumerator DispatchRequests()
    {
        var serverApi = ServerApi.Instance;
        var request = serverApi.PostBodyAsync(new GameStateDTO());
        serverApi.GetAsync("test");
        serverApi.PostQueryAsync("wapow");
        var awaiter = request.GetAwaiter();
        Debug.Log(awaiter.GetResult().ToJson());
        yield return null;
    }
}
