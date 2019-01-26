using GeneratedServerAPI;
using GGJ19.Scripts.GameLogic;
using GGJ19.Scripts.Server_Api;
using HalfBlind.ScriptableVariables;
using UnityEngine;
using UnityEngine.UI;
using XNode;

[CreateNodeMenu(nameof(SendJoinRoomMessage), "Request", "Send", "Room", "Join")]
public class SendJoinRoomMessage : FlowNode
{
    [Input]
    public string roomName;
    [Input]
    public string myPlayerId;

    public override object GetValue(NodePort port)
    {
        return null;
    }

    public override void ExecuteNode()
    {
        Debug.Log("Execute Node");
        var roomNameInput = GetInputValue(nameof(roomName), roomName);
        var playerIdInput = GetInputValue(nameof(myPlayerId), myPlayerId);
            SendServerRequest(roomNameInput, playerIdInput);
    }

    private async void SendServerRequest(string roomNameInput, string playerIdInput)
    {
        Debug.Log("Sending Join Request:");
        var serverApi = ServerApi.Instance;
        RoomInformation joinRoomAsync = await serverApi.JoinRoomAsync(roomNameInput, playerIdInput);
        GameLogicManager.instance.UpdateGameState(joinRoomAsync.Playing, joinRoomAsync.Waiting);
    }
}
