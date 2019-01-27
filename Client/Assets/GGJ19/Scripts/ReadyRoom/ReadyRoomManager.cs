using GGJ19.Scripts.GameLogic;
using HalfBlind.ScriptableVariables;
using UnityEngine;
using UnityEngine.UI;

public class ReadyRoomManager : MonoBehaviour
{
    // A Server has been Created/Joined
    // Constantly query the server : Do we have 4 players?

    // Public Objects
    public Text roomIdDisplayField;
    public GameObject playerDisplayContianer;
    public GameObject startGameButton;

    // Global Variables
    public GlobalFloat _readyRoomPollTime;

    // Internal
    [SerializeField]
    private ReadyRoomPlayerDisplay[] playerDisplays;

    // From Server
    private string[] _knownPlayerIds;

    private void Start()
    {
        // Set Room Text
        roomIdDisplayField.text = "Room ID: " + GameLogicManager.instance.serverRoomName.Value;
        initKnownPlayers();
        initPlayerDisplays();
        updateKnownPlayers();
        GameLogicManager.instance.onPlayerCountChanged.AddListener(onPlayerNumberUpdate);
        GameLogicManager.instance.onRoomInfoChanged.AddListener(onRoomInfoChange);
        InvokeRepeating("requestRoomInformation", 0, GameLogicManager.instance.serverPollTimeMs.Value / 1000f);
    }

    private void OnDestroy()
    {
        CancelInvoke();
        GameLogicManager.instance.onRoomInfoChanged.RemoveListener(onRoomInfoChange);
        GameLogicManager.instance.onPlayerCountChanged.RemoveListener(onPlayerNumberUpdate);
    }

    private void initPlayerDisplays()
    {
        playerDisplays = playerDisplayContianer.GetComponentsInChildren<ReadyRoomPlayerDisplay>();
    }

    private void initKnownPlayers()
    {
        _knownPlayerIds = new string[4];
    }


    // Update Internal
    private void updateKnownPlayers()
    {
        _knownPlayerIds[0] = GameLogicManager.instance.player1Id.Value;
        _knownPlayerIds[1] = GameLogicManager.instance.player2Id.Value;
        _knownPlayerIds[2] = GameLogicManager.instance.player3Id.Value;
        _knownPlayerIds[3] = GameLogicManager.instance.player4Id.Value;

        startGameButton.SetActive(GameLogicManager.instance.MyPlayerId == GameLogicManager.instance.serverOwnerId.Value);

        for (int i = 0; i < _knownPlayerIds.Length; i++)
        {
            bool isMyPlayer = GameLogicManager.instance.MyPlayerId != null && 
                GameLogicManager.instance.MyPlayerId == _knownPlayerIds[i];

            playerDisplays[i].setPlayerId(_knownPlayerIds[i], isMyPlayer);
        }
    }

    // Client-Server Communication

    private void requestRoomInformation()
    {
        GameLogicManager.instance.SendRoomInfoRequest();
    }

    private void onPlayerNumberUpdate()
    {
        updateKnownPlayers();
    }

    private void onRoomInfoChange()
    {

    }

    private void startGame()
    {
        // Send StartGame Command
    }

}
