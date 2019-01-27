using GGJ19.Scripts.GameLogic;
using GGJ19.Scripts.Server_Api;
using System.Threading.Tasks;
using UnityEngine;
using XNode;

[CreateNodeMenu(nameof(SendRoleChosenMessage), "Request", "Send", "Role")]
public class SendRoleChosenMessage : FlowNode {
    [Input]
    public string roleChosen;

    public override object GetValue(NodePort port) {
        return null;
    }

    public override void ExecuteNode() {
        var currentRoleChosen = GetInputValue(nameof(roleChosen), roleChosen);
        SendServerRequest(currentRoleChosen);
    }

    private async Task SendServerRequest(string currentRoleChosen) {
        Debug.Log("Role choosen");
        
        var serverApi = ServerApi.Instance;
        var setRoleResponse = await serverApi.SetRoleAsync("", "", currentRoleChosen);
        var roomName = GameLogicManager.instance.serverRoomName?.Value;
        var playerId = GameLogicManager.instance.MyPlayerId;
        if (string.IsNullOrEmpty(roomName)) {
            UnityEngine.Debug.LogError("Room name is empty, wont set role on serverside");
            return;
        }

        GameLogicManager.instance.UpdateGameState(setRoleResponse.Playing, setRoleResponse.Waiting);
    }
}
