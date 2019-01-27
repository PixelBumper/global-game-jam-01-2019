using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using GGJ19.Scripts.GameLogic;
using GGJ19.Scripts.Server_Api;
using UnityEngine;
using XNode;

[CreateNodeMenu(nameof(SendChoosenEmojiMessage), "Send", "Chosen", "Emoji")]
public class SendChoosenEmojiMessage : FlowNode
{
    [Input]
    public string emojiKey;
    
    public override object GetValue(NodePort port)
    {
        return null;
    }

    public override void ExecuteNode()
    {
        var inputValue = GetInputValue(nameof(emojiKey), emojiKey);
        sendEmojis(inputValue);
    }

    private async Task sendEmojis(string emojis)
    {
        var gameLogicManager = GameLogicManager.instance;
        Debug.Log("dispatch request");
        try
        {
            var roomInformation = await ServerApi.Instance.SendEmojisAsync(gameLogicManager.serverRoomName.Value,
                gameLogicManager.MyPlayerId, emojis);
            Debug.Log($"{nameof(roomInformation)}:{roomInformation.ToJson()}");

            gameLogicManager.UpdateGameState(roomInformation.Playing, roomInformation.Waiting);
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        } 
    }
}
