using GeneratedServerAPI;
using GGJ19.Scripts.GameLogic;
using GGJ19.Scripts.Server_Api;
using System.Threading.Tasks;
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

    private async Task SendServerRequest(string roomName, string playerId)
    {
        Debug.Log("Sending Join Request: " + roomName + " : " + playerId);
        if (!string.IsNullOrEmpty(roomName) && !string.IsNullOrEmpty(playerId))
        {
            // No Nulls in request room!
            Debug.Log("Valid. Sent.");

            var serverApi = ServerApi.Instance;
            RoomInformation joinRoomResponse = await serverApi.JoinRoomAsync(roomName, playerId);
            Debug.Log($"{nameof(joinRoomResponse)}:{joinRoomResponse.ToJson()}");
            GameLogicManager.instance.UpdateGameState(joinRoomResponse.Playing, joinRoomResponse.Waiting);
            bool badResponse = joinRoomResponse.ToJson() == "{}";
            if (badResponse)
            {
                // Try room info as fallback. Maybe we're already in there.
                Debug.Log("Trying to get Room Info Anyway...");
                RoomInformation roomInfoResponse = await serverApi.RoomInformationAsync(roomName);
                Debug.Log($"{nameof(roomInfoResponse)}:{roomInfoResponse.ToJson()}");
                GameLogicManager.instance.UpdateGameState(roomInfoResponse.Playing, roomInfoResponse.Waiting);
            }
        }
    }
}