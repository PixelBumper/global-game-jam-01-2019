
using GGJ19.Scripts.GameLogic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyJoinManager : MonoBehaviour
{
    public string ReadyRoomSceneName;
    // Start is called before the first frame update
    void Start()
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
