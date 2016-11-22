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

    private int prevPlayerCount = 0;

    NetworkLobbyManager lobbyMan;

    void Start()
    {
        lobbyMan = FindObjectOfType<NetworkLobbyManager>();
    }

    int OccupiedSlots()
    {
        int count = 0;
        foreach(var ls in lobbyMan.lobbySlots)
        {
            if(ls != null)
            {
                ++count;
            }
        }
        return count;
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        temp = true;
    }


    [Command]
    void CmdGetAllUserNames()
    {
        for (int i = 0; i < lobbyMan.lobbySlots.Length; ++i)
        {
            var ls = lobbyMan.lobbySlots[i];
            var info = GameSettings.Instance.GetPlayerInfo(ls.connectionToClient.connectionId);
            RpcPlaceUsername(i, info.userName);
            Debug.Log("Updating UI for user " + info.userName);
        }

      


    }

    [ClientRpc]
    void RpcPlaceUsername(int id, string username)
    {
        Debug.Log("Updating UI for user " + id.ToString() + username);
        if (id == 0)
        {
            transform.GetChild(0).FindChild("Player1").GetComponent<Text>().text = username;
        }
        else if (id == 1)
        {
            transform.GetChild(0).FindChild("Player2").GetComponent<Text>().text = username;
        }
        else if (id == 2)
        {
            transform.GetChild(0).FindChild("Player3").GetComponent<Text>().text = username;
        }
        else if (id == 3)
        {
            transform.GetChild(0).FindChild("Player4").GetComponent<Text>().text = username;
        }


    }

 
    // Update is called once per frame
    void Update()
    {
        if (temp && isClient)
        {
            int filledSlotCount = OccupiedSlots();
            if (prevPlayerCount != filledSlotCount)
            {
                prevPlayerCount = filledSlotCount;
                CmdGetAllUserNames();
            }
        }
    }






}
