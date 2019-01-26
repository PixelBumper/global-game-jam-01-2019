using System.Collections;
using System.Collections.Generic;
using GGJ19.Scripts.GameLogic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    public string ReadyRoomSceneName;

    void Awake()
    {
        GameLogicManager.instance.onRoomInfoChanged.AddListener(onRoomInfoChange);
    }

    private void OnDestroy()
    {
        GameLogicManager.instance.onRoomInfoChanged.RemoveListener(onRoomInfoChange);
    }

    void onRoomInfoChange()
    {
        string roomName = GameLogicManager.instance.serverRoomName.Value;
        if (!string.IsNullOrEmpty(roomName))
        {
            SceneManager.LoadScene(ReadyRoomSceneName);
        }
    }
}
