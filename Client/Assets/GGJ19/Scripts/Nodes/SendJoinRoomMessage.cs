using GeneratedServerAPI;
using GGJ19.Scripts.GameLogic;
using GGJ19.Scripts.Server_Api;
using UnityEngine;
using XNode;

[CreateNodeMenu(nameof(SendJoinRoomMessage), "Request", "Send", "Room", "Join")]
public class SendJoinRoomMessage : FlowNode
{
    [Input]
    public string roomInput;

    [Input]
    public string playerIdInput;

    public override object GetValue(NodePort port)
    {
        return null;
    }

    public override void ExecuteNode()
    {
        string roomName = GetInputValue(nameof(roomInput), roomInput);
        string playerId = GetInputValue(nameof(playerIdInput), playerIdInput);
        SendServerRequest(roomName, playerId);
    }

    private async void SendServerRequest(string roomName, string playerId)
    {
        Debug.Log("Sending Join Request: " + roomName + " : " + playerId);
        if (!string.IsNullOrEmpty(roomName) && !string.IsNullOrEmpty(playerId))
        {
            // No Nulls in request room!
            Debug.Log("Valid. Sent.");

            var serverApi = ServerApi.Instance;
            RoomInformation joinRoomAsync = await serverApi.JoinRoomAsync(roomName, playerId);
            GameLogicManager.instance.UpdateGameState(joinRoomAsync.Playing, joinRoomAsync.Waiting);
        }
    }
}
