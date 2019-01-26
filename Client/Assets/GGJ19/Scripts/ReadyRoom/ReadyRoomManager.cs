using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using HalfBlind.ScriptableVariables;

public class ReadyRoomManager : MonoBehaviour
{
    // A Server has been Created/Joined
    // Constantly query the server : Do we have 4 players?

    // Public Objects
    public Text roomIdDisplay;
    public GameObject playerDisplayContianer;

    // Global Variables
    public GlobalString _serverRoomName;
    public GlobalString _myPlayerId;
    public GlobalFloat _readyRoomPollTime;

    // Internal
    [SerializeField]
    private ReadyRoomPlayerDisplay[] playerDisplays;

    // From Server
    private string[] _knownPlayerIds;

    private void Awake()
    {
        // Set Room Text
        roomIdDisplay.text = "Room ID: " + _serverRoomName.Value;
        initKnownPlayers();
        initPlayerDisplays();

        FAKE_SetVarsFromServer();
//        Invoke(roomInformationRequest, _readyRoomPollTime.Value);
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
        _myPlayerId.Value = "Andy";
        _serverRoomName.Value = "Ga4CxP";
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
            playerDisplays[i].setPlayerId(_knownPlayerIds[i], _knownPlayerIds[i] == _myPlayerId.Value);
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
