using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyRoomPlayerDisplay : MonoBehaviour
{
    // Public Objects
    public TextMesh playerIdDisplay;
    public GameObject visual_present;
    public GameObject visual_absent;
    public GameObject visual_myCharacter;

    // Internal
    private string playerId;
    public string PlayerId
    {
        get => playerId;
    }

    private void Awake()
    {
        visual_absent.SetActive(true);
        visual_present.SetActive(false);
        visual_myCharacter.SetActive(false);
    }

    public void setPlayerId(string value, bool isMyPlayer)
    {
        playerId = value;
        playerIdDisplay.text = playerId;
        visual_absent.SetActive(playerId == "");
        visual_present.SetActive(playerId != "");
        visual_myCharacter.SetActive(isMyPlayer);
    }
}
