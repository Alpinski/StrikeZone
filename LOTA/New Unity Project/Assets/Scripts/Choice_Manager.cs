using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Choice_Manager : NetworkLobbyManager
{
    Dictionary<int, int> currentPlayers = new Dictionary<int, int>();

    public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (!currentPlayers.ContainsKey(conn.connectionId))
            currentPlayers.Add(conn.connectionId, 0);

        return base.OnLobbyServerCreateLobbyPlayer(conn, playerControllerId);
    }

    public void SetPlayerTypeLobby(int conn, int _type)
    {
        if (currentPlayers.ContainsKey(conn))
            currentPlayers[conn] = _type;
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