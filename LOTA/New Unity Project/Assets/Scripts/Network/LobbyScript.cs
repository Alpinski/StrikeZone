using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyScript : NetworkBehaviour
{

    public Text p1, p2, p3, p4;

    private GameObject player;

    private Text playerText;

    [SyncVar]
    private int NumberofPlayers;

    [SerializeField]
    private Button dcButton;

    private float startDelay = 2;

    private int prevPlayerCount = 0;

    NetworkLobbyManager lobbyMan;

    Choice_Manager CM;


    [HideInInspector]
    public bool playerIsOnServer = false;

    void Start()
    {
        lobbyMan = FindObjectOfType<NetworkLobbyManager>();
        CM = lobbyMan.GetComponent<Choice_Manager>();

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

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Clean up after player " + player);
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
        GameSettings.Instance.ClearPlayerInfo(connectionToClient.connectionId);
        DestroyServer();
    }


    void DestroyServer()
    {
        NetworkManager.singleton.StopHost();
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
            p1.text = username;
        }
        else if (id == 1)
        {
            p2.text = username;
        }
        else if (id == 2)
        {
            p3.text = username;
        }
        else if (id == 3)
        {
            p4.text = username;
        }


    }

 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            dcButton = GameObject.Find("ButtonDisconnect").GetComponent<Button>();

        }
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
