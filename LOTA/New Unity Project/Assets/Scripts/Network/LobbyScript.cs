using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyScript : NetworkBehaviour {
    
    
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
        for(int i = 0; i < lobbyMan.lobbySlots.Length; ++i)
        {
            var ls = lobbyMan.lobbySlots[i];
            var info = GameSettings.Instance.GetPlayerInfo(ls.connectionToClient.connectionId);
        }

       

       
    }

    [ClientRpc]
    void  RpcPlaceUsername(int id, string info)
    {
        if(id == 1)
        {
            GameObject.Find("Player1").GetComponent<Text>().text = info;
        }
  
    }

    [Command]
    void CmdGetPlayerInfo()
    {
        var info = GameSettings.Instance.GetPlayerInfo(connectionToClient.connectionId);
        SetName(info.userName);
        RpcUpdateUI(info.userName);
    }

    [ClientRpc]
    void RpcUpdateUI(string name)
    {
        SetName(name);
    }


    public void SetName(string x)
    {
        playerText.text = x;
        Debug.Log(x);
    }

	
	// Update is called once per frame
	void Update ()

    {
        if(prevPlayerCount != lobbyMan.lobbySlots.Length)
        {
            prevPlayerCount = lobbyMan.lobbySlots.Length;

        }

        loadTimer -= Time.deltaTime;
        if (temp == true)
        {
            if (loadTimer <= 0)
            {
                NumberofPlayers = NumberofPlayers + 1;
                if (NumberofPlayers > 0 && NumberofPlayers < 5)
                {
                    if (NumberofPlayers == 1)
                    {
                        player = GameObject.Find("Player1");
                    }
                    if (NumberofPlayers == 2)
                    {
                        player = GameObject.Find("Player2");
                    }
                    if (NumberofPlayers == 3)
                    {
                        player = GameObject.Find("Player3");
                    }
                    if (NumberofPlayers == 4)
                    {
                        player = GameObject.Find("Player4");
                    }

                    if (player != null)
                    {
                        playerText = player.GetComponent<Text>();
                    }
                }
                Debug.Log(NumberofPlayers);

                if (isClient)
                {
                    CmdGetPlayerInfo();
                    Debug.Log("running cmd");
                }
                
                Debug.Log(playerText.text);
                Debug.Log(player);
                temp = false;
            }
        }
    }
}
