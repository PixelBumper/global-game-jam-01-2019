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

    // Global Variables
    public GlobalFloat _readyRoomPollTime;

    // Internal
    [SerializeField]
    private ReadyRoomPlayerDisplay[] playerDisplays;

    // From Server
    private string[] _knownPlayerIds;

    private void Awake()
    {
        // Set Room Text
        roomIdDisplayField.text = "Room ID: " + GameLogicManager.instance.serverRoomName.Value;
        initKnownPlayers();
        initPlayerDisplays();

        FAKE_SetVarsFromServer();
        requestRoomInformation();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            roomInformationResponse();
        }
    }

    private void FAKE_SetVarsFromServer()
    {
        _knownPlayerIds = new string[] { "", "Andy", "", "Frank" };
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
    }

    private void checkForGameStart()
    {
        foreach (string id in _knownPlayerIds)
        {
            if (string.IsNullOrEmpty(id))
            {
                Invoke("requestRoomInformation", GameLogicManager.instance.serverPollTimeMs.Value);
                return;
            }
        }

        // ALL PLAYERS FOUND! READY TO START!!!
        Invoke("StartGame", 2000);
    }

    // Client-Server Communication

    private void requestRoomInformation()
    {

    }

    private async void SendRoomInfoRequest()
    {

    }

        private void roomInformationResponse()
    {

        // Dispatch Updates
        updateKnownPlayers();
        checkForGameStart();
    }

    private void startGame()
    {

    }

}
