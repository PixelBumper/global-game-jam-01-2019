using UnityEngine;

public class ReadyRoomPlayerDisplay : MonoBehaviour
{
    // Public Objects

    public GameObject visual_present;
    public GameObject visual_absent;
    public GameObject visual_myCharacter;

    // Internal
   
    private void Awake()
    {
        visual_absent.SetActive(true);
        visual_present.SetActive(false);
        visual_myCharacter.SetActive(false);
    }

    public void setPlayerId(string value, bool isMyPlayer)
    {
        visual_absent.SetActive(value == "");
        visual_present.SetActive(value != "");
        visual_myCharacter.SetActive(isMyPlayer);
    }
}
