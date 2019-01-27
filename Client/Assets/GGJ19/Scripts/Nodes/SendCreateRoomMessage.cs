using System;
using System.Threading.Tasks;
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

    private async Task SendServerRequest()
    {
        try
        {
            GameLogicManager logicManager = GameLogicManager.instance;
            string[] allThreats = new string[]{
                logicManager.threat1.GetValue().ToString(),
                logicManager.threat2.GetValue().ToString(),
                logicManager.threat3.GetValue().ToString(),
                logicManager.threat4.GetValue().ToString(),
                logicManager.threat5.GetValue().ToString(),
                logicManager.threat6.GetValue().ToString()
             };
            string threatString = "";
            for (int i = 0; i < allThreats.Length; i++)
            {
                threatString = threatString + allThreats[i];
                if (i < allThreats.Length - 1)
                {
                    threatString = threatString + ", ";
                }
            }

            string playerId = logicManager.MyPlayerId;

            Debug.Log(threatString);
            Debug.Log(playerId);
            if (!string.IsNullOrEmpty(threatString) && !string.IsNullOrEmpty(playerId))
            {
                Debug.Log("Sending Create Room Request");
                var serverApi = ServerApi.Instance;
                Room createRoomResponse = await serverApi.CreateRoomAsync(playerId, threatString, 5, 15, 6, null);
                Debug.Log($"{nameof(createRoomResponse)}:{createRoomResponse.ToJson()}");
                GameLogicManager.instance.UpdateGameState(null, createRoomResponse);
            }
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogException(e);
        }
    }
}
