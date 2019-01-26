using GeneratedServerAPI;
using GGJ19.Scripts.GameLogic;
using GGJ19.Scripts.Server_Api;
using HalfBlind.ScriptableVariables;
using UnityEngine.UI;
using XNode;

[CreateNodeMenu(nameof(SendJoinRoomMessage), "Request", "Send", "Room", "Join")]
public class SendJoinRoomMessage : FlowNode
{
    [Input]
    public InputField roomNameInputField;
    public GlobalString myPlayerId;

    public override object GetValue(NodePort port)
    {
        return null;
    }

    public override void ExecuteNode()
    {
        var roomNameInput = GetInputValue(nameof(roomNameInputField.text), "");
        var playerIdInput = GetInputValue(nameof(myPlayerId.Value), "");
        if(roomNameInput != "" && playerIdInput != "")
        {
            SendServerRequest(roomNameInput, playerIdInput);
        }
    }

    private async void SendServerRequest(string roomNameInput, string playerIdInput)
    {
        var serverApi = ServerApi.Instance;
        RoomInformation joinRoomAsync = await serverApi.JoinRoomAsync(roomNameInput, playerIdInput);
        GameLogicManager.instance.JoinReadyRoom(joinRoomAsync);
    }
}
