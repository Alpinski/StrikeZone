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

    private float startDelay = 2;

    private int prevPlayerCount = 0;

    NetworkLobbyManager lobbyMan;

    [HideInInspector]
    public bool playerIsOnSever = false;

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
        temp = true;
 
    }


    [Command]
    void CmdGetAllUserNames()
    {
        Debug.Log(OccupiedSlots());
        for (int i = 0; i < OccupiedSlots(); ++i)
        {
            //**********************************
            Debug.Log("i = " + i);
            var ls = lobbyMan.lobbySlots[i];
            Debug.Log("ls = " + ls);
            var info = GameSettings.Instance.GetPlayerInfo(ls.connectionToClient.connectionId);
            Debug.Log(GameSettings.Instance.GetPlayerInfo(0).userName);
            if (info != null && ls != null)
            {
                RpcPlaceUsername(i, info.userName);
                Debug.Log("id " + i + " has placed username " + info.userName);
            }
            else if (info == null || ls == null)
            {
                Debug.Log("cant update ui, null");
            }
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
        startDelay -= Time.deltaTime;
        if (temp && isClient && playerIsOnSever) 
        {

            int filledSlotCount = OccupiedSlots();
            if (prevPlayerCount != filledSlotCount)
            {
                Debug.Log(" someone has joined");
                prevPlayerCount = filledSlotCount;
                CmdGetAllUserNames();
                playerIsOnSever = false;
            }
        }
    }






}
