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


    private float startDelay = 2;

    private int prevPlayerCount = 0;

    NetworkLobbyManager lobbyMan;

    [HideInInspector]
    public bool playerIsOnServer = false;

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

        startDelay = 2;
 
    }


    [Command]
    void CmdBroadcastAllUserNames()
    {
        var allLobbyPlayers = FindObjectsOfType<LobbyScript>();
        foreach (var player in allLobbyPlayers)
        {
            for (int i = 0; i < OccupiedSlots(); ++i)
            {
                //**********************************
                var ls = lobbyMan.lobbySlots[i];
                var info = GameSettings.Instance.GetPlayerInfo(ls.connectionToClient.connectionId);
                if (info != null && ls != null)
                {
                    player.RpcPlaceUsername(i, info.userName);
                }
                else if (info == null || ls == null)
                {
                    continue;
                }
            }
        }
    }

    [ClientRpc]
    void RpcPlaceUsername(int id, string username)
    {
        if (!isLocalPlayer)
        {
            return;
        }

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
        startDelay -= Time.deltaTime;
        if (isClient && playerIsOnServer) 
        {

            int filledSlotCount = OccupiedSlots();
            if (prevPlayerCount != filledSlotCount)
            {
                Debug.Log(" someone has joined");
                prevPlayerCount = filledSlotCount;
                CmdBroadcastAllUserNames();
                playerIsOnServer = false;
            }
        }
    }






}
