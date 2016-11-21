using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LobbyScript : NetworkBehaviour {
    
    
    private GameObject player;

    private string username;

    private Text playerText;

    [SyncVar]
    private int NumberofPlayers;

    public bool temp = false;

    private float loadTimer = 2;

    void OnEnable()
    {

        temp = true;

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

                if (isLocalPlayer)
                {
                 CmdGetPlayerInfo();
                    Debug.Log("running cmd");
                }
                
                playerText.text = username;
                Debug.Log(username);
                Debug.Log(playerText.text);
                Debug.Log(player);
                temp = false;
            }
        }
    }
}
