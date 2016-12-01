using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Choice_Manager : NetworkLobbyManager
{
    // this script changes the networkmanger

    public GameObject Lobby;

    Dictionary<int, int> currentPlayers = new Dictionary<int, int>();


    //called to add a player when joined
    public bool RegisterPlayerJoin(int id)
    {
        if (!currentPlayers.ContainsKey(id))
        {
            currentPlayers.Add(id, 0);
            return true;
        }

        return false;
    }

    void Start()
    {
    }

    public Dictionary<int, int> PlayerChoices
    {
        get { return currentPlayers; }
    }


    public void SetPlayerTypeLobby(int id, int _type)
    {
        if (currentPlayers.ContainsKey(id))
            currentPlayers[id] = _type;
    }

    public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
    {
        int index = currentPlayers[conn.connectionId];
        GameObject _temp = (GameObject)GameObject.Instantiate(spawnPrefabs[index],
            startPositions[conn.connectionId].position,
            Quaternion.identity);

        return _temp;
    }

    //starts a server only game 
    public void StartUpHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();

    }

    //starts a host game 
    public void startSever()
    {
        NetworkManager.singleton.StartServer();
    }

    //joins a game
    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }

    //looks for the ip
    void SetIPAddress()
    {
        string ipAddress = GameObject.Find("inputFieldIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    //looks for the port
    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }


    void OnClientSceneChanged ()
    {
        
            SetupMenuSceneButtons();
            SetupOtherScenenButtons();
    }

    void SetupMenuSceneButtons()
    {
        GameObject.Find("LanHost").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("LanHost").GetComponent<Button>().onClick.AddListener(StartUpHost);

        GameObject.Find("joinGame").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("joinGame").GetComponent<Button>().onClick.AddListener(JoinGame);

    }

    void SetupOtherScenenButtons()
    {
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.RemoveAllListeners();
        GameObject.Find("ButtonDisconnect").GetComponent<Button>().onClick.AddListener(NetworkManager.singleton.StopHost);
    }

}