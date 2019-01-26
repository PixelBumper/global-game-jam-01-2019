using GGJ19.Scripts.GameLogic;
using GGJ19.Scripts.Server_Api;
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

    private async void SendServerRequest(string currentRoleChosen) {
        var serverApi = ServerApi.Instance;
        var roomName = GameLogicManager.instance.serverRoomName?.Value;
        var playerId = GameLogicManager.instance.PlayerId;
        if (string.IsNullOrEmpty(roomName)) {
            UnityEngine.Debug.LogError("Room name is empty, wont set role on serverside");
            return;
        }
        var joinRoomAsync = await serverApi.SetRoleAsync(roomName, playerId, currentRoleChosen);
        GameLogicManager.instance.UpdateGameState(joinRoomAsync.Playing, joinRoomAsync.Waiting);
    }
}
