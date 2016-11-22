using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyScript : NetworkBehaviour
{


    private GameObject player;

    private Text playerText;

    [SyncVar]
    private int NumberofPlayers;

    public bool temp = false;

    private float loadTimer = 2;

    private int prevPlayerCount;

    NetworkLobbyManager lobbyMan;

    void Start()
    {
        lobbyMan = FindObjectOfType<NetworkLobbyManager>();
        prevPlayerCount = lobbyMan.lobbySlots.Length;
    }

    void OnEnable()
    {

        temp = true;

    }




    [Command]
    void CmdGetAllUserNames()
    {
        for (int i = 0; i < lobbyMan.lobbySlots.Length; ++i)
        {
            var ls = lobbyMan.lobbySlots[i];
            var info = GameSettings.Instance.GetPlayerInfo(ls.connectionToClient.connectionId);
        }




    }

    [ClientRpc]
    void RpcPlaceUsername(int id, string info)
    {
        if (id == 1)
        {
            GameObject.Find("Player1").GetComponent<Text>().text = info;
        }
        else if (id == 2)
        {
            GameObject.Find("Player2").GetComponent<Text>().text = info;
        }
        else if (id == 3)
        {
            GameObject.Find("Player3").GetComponent<Text>().text = info;
        }
        else if (id == 4)
        {
            GameObject.Find("Player4").GetComponent<Text>().text = info;
        }


    }

 
    // Update is called once per frame
    void Update()
    {
        if (prevPlayerCount != lobbyMan.lobbySlots.Length)
        {
            prevPlayerCount = lobbyMan.lobbySlots.Length;
        }
    }




    void OnLobbyClientConnect()
    {
        CmdGetAllUserNames();
    }

    void OnLobbyClientDisconnect()
    {
        CmdGetAllUserNames();
    }


}
