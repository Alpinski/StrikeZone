using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

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
}