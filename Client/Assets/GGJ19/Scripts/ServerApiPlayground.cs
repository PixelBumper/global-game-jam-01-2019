using GGJ19.Scripts.Server_Api;
using UnityEngine;

public class ServerApiPlayground: MonoBehaviour
{
    [ContextMenu(nameof(DispatchRequests))]
    private async void DispatchRequests()
    {
        var serverApi = ServerApi.Instance;
        var request = await serverApi.CreateRoomAsync("name");
        Debug.Log(request.ToJson());
    }
}
