using GeneratedServerAPI;
using GGJ19.Scripts.GameLogic;
using GGJ19.Scripts.Server_Api;
using UnityEngine;
using XNode;

[CreateNodeMenu(nameof(SendCreateRoomMessage), "Request", "Send", "Room", "Create")]
public class SendCreateRoomMessage : FlowNode
{
    public override object GetValue(NodePort port)
    {
        return null;
    }

    public override void ExecuteNode()
    {
        SendServerRequest();
    }

    private async void SendServerRequest()
    {
        string allThreats = GameLogicManager.instance.allThreats.ToString();
        string playerId = GameLogicManager.instance.PlayerId;
        if (string.IsNullOrEmpty(allThreats) && string.IsNullOrEmpty(playerId))
        {
            var serverApi = ServerApi.Instance;
            Room joinRoomAsync = await serverApi.CreateRoomAsync(playerId, allThreats, null, null);
            GameLogicManager.instance.UpdateGameState(null, joinRoomAsync);
        }
    }
}
