using GeneratedServerAPI;
using GGJ19.Scripts.GameLogic;
using GGJ19.Scripts.Server_Api;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

[CreateNodeMenu(nameof(SendStartGameMessage), "Request", "Start", "Room", "Game")]
public class SendStartGameMessage : FlowNode {

    public override object GetValue(NodePort port) {
        return null;
    }

    public override void ExecuteNode() {
        string roomName = GameLogicManager.instance.serverRoomName.Value;
        string playerId = GameLogicManager.instance.MyPlayerId;
        SendServerRequest(roomName, playerId);
    }

    public override void TriggerFlow() {
        //base.TriggerFlow();
    }

    private async Task SendServerRequest(string roomName, string playerId) {
        Debug.Log("Sending StartGame Request: " + roomName + " : " + playerId);
        if (!string.IsNullOrEmpty(roomName) && !string.IsNullOrEmpty(playerId)) {
            // No Nulls in request room!
            Debug.Log("Valid. Sent.");
            var serverApi = ServerApi.Instance;
            var startRoomResponse = await serverApi.StartRoomAsync(roomName, playerId);
            Debug.Log($"{nameof(startRoomResponse)}:{startRoomResponse.ToJson()}");
            GameLogicManager.instance.UpdateGameState(startRoomResponse.Playing, startRoomResponse.Waiting);
            base.TriggerFlow();
        }
    }
}
