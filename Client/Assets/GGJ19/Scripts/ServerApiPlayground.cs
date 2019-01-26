using GGJ19.Scripts.Server_Api;
using UnityEngine;

public class ServerApiPlayground: MonoBehaviour
{
    [ContextMenu(nameof(DispatchRequests))]
    private async void DispatchRequests()
    {
        var serverApi = ServerApi.Instance;
        var request = await serverApi.CreateRoomAsync("carlossfasdfasdfadf", "1,2,3,4,5,6,7,8", null, null);
        Debug.Log(request.ToJson());
    }
}
