using System;
using System.Collections;
using System.Collections.Generic;
using GGJ19.Scripts.GameLogic;
using GGJ19.Scripts.Server_Api;
using UnityEngine;
using XNode;

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
        sendEmojis(emojiKey);
    }

    private async void sendEmojis(String emojis)
    {
        var gameLogicManager = GameLogicManager.instance;
        var roomInformation = await ServerApi.Instance.SendEmojisAsync(gameLogicManager.serverRoomName.name, gameLogicManager.PlayerId, emojis);
        gameLogicManager.UpdateGameState(roomInformation.Playing, roomInformation.Waiting);
    }
}
