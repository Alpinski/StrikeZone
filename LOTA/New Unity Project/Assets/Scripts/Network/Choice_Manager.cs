using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Choice_Manager : NetworkLobbyManager
{
    Dictionary<int, int> currentPlayers = new Dictionary<int, int>();

    public bool RegisterPlayerJoin(int id)
    {
        if (!currentPlayers.ContainsKey(id))
        {
            currentPlayers.Add(id, 0);
            return true;
        }

        return false;
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


    public void StartUpHost()
    {
        SetPort();
        NetworkManager.singleton.StartHost();

    }


    public void JoinGame()
    {
        SetIPAddress();
        SetPort();
        NetworkManager.singleton.StartClient();
    }


    void SetIPAddress()
    {
        string ipAddress = GameObject.Find("inputFieldIPAddress").transform.FindChild("Text").GetComponent<Text>().text;
        NetworkManager.singleton.networkAddress = ipAddress;
    }

    void SetPort()
    {
        NetworkManager.singleton.networkPort = 7777;
    }


    void OnLevelWasLoaded (int level)
    {
        if(level == 0)
        {
            SetupMenuSceneButtons();
        }
        else
        {
            SetupOtherScenenButtons();
        }
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