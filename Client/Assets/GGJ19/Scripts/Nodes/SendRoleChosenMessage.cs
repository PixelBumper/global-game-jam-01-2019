using GGJ19.Scripts.GameLogic;
using GGJ19.Scripts.Server_Api;
using XNode;

[CreateNodeMenu(nameof(SendRoleChosenMessage), "Request", "Send", "Role")]
public class SendRoleChosenMessage : FlowNode
{
    [Input]
    public string roleChosen;
    
    public override object GetValue(NodePort port)
    {
        return null;
    }

    public override void ExecuteNode()
    {
        var currentRoleChosen = GetInputValue(nameof(roleChosen), roleChosen);
        SendServerRequest(currentRoleChosen);
    }

    private async void SendServerRequest(string currentRoleChosen)
    {
        var serverApi = ServerApi.Instance;
        // var joinRoomAsync = await serverApi.SetRoleAsync(GameLogicManager.instance.serverRoomName.Value, GameLogicManager.instance.PlayerId, currentRoleChosen);
        // GameLogicManager.instance.UpdateGameState(joinRoomAsync.Playing, joinRoomAsync.Waiting);
    }
}
