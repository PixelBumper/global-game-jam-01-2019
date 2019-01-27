using System.Collections;
using System.Collections.Generic;
using GGJ19.Scripts.GameLogic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("requestRoomInformation", 0, GameLogicManager.instance.serverPollTimeMs.Value / 1000f);
//        GameLogicManager.instance.onRoomInfoChanged.AddListener(onRoomInfoChange);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        CancelInvoke();
        //GameLogicManager.instance.onRoomInfoChanged.RemoveListener(onRoomInfoChange);
        //GameLogicManager.instance.onPlayerCountChanged.RemoveListener(onPlayerNumberUpdate);
    }

    // Client-Server Communication

    private void requestRoomInformation()
    {
        GameLogicManager.instance.SendRoomInfoRequest();
    }
}
