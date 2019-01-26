using GGJ19.Scripts.GameLogic;
using HalfBlind.ScriptableVariables;
using UnityEngine;
using UnityEngine.UI;

public class ReadyRoomManager : MonoBehaviour
{
    // A Server has been Created/Joined
    // Constantly query the server : Do we have 4 players?

    // Public Objects
    public Text roomIdDisplay;
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
        roomIdDisplay.text = "Room ID: ";
        initKnownPlayers();
        initPlayerDisplays();

        FAKE_SetVarsFromServer();
        Invoke("roomInformationRequest", (float)_readyRoomPollTime.Value);
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
        _knownPlayerIds = new string[4] { "", "", "", "" };
    }


    // Update Internal
    private void updatePlayerIds()
    {
        for (int i = 0; i < playerDisplays.Length; i++)
        {
            playerDisplays[i].setPlayerId(_knownPlayerIds[i], _knownPlayerIds[i] == GameLogicManager.instance.PlayerId);
        }
    }

    // Client-Server Communication

    private void roomInformationRequest()
    {

    }

    private void roomInformationResponse()
    {

        // Dispatch Updates
        updatePlayerIds();
    }

    private void startGame()
    {

    }

}
