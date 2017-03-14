using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameSettings : MonoBehaviour
{
    //
    const string resourceName = "Server-side Info";
    static GameSettings instance;
    
    ///Do not access from Clients. Server-Only!
    public static GameSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameSettings>();
                if (instance == null)
                {
                    var settingsPrefab = Resources.Load<GameObject>(resourceName);
                    if (settingsPrefab == null)
                    {
                        throw new System.Exception("The resource with name " + resourceName + " was not found in a Resource folder.");
                    }
                    var settingsInstance = Instantiate(settingsPrefab);
                    instance = settingsInstance.GetComponent<GameSettings>();
                    if (instance == null)
                    {
                        throw new MissingComponentException("GameSettings is missing on the resource " + resourceName + ".");
                    }

                    DontDestroyOnLoad(settingsInstance);
                }
            }
            return instance;
        }
    }
    //
    public class PlayerInfo
    {
        public int heroChoice;
        public string userName;
    }

    Dictionary<int, PlayerInfo> info = new Dictionary<int, PlayerInfo>();

    PlayerInfo CreatePlayerEntry(int id, int heroChoice = 0)
    {
        var player = new PlayerInfo();
        player.heroChoice = heroChoice;
        info.Add(id, player);

        return player;
    }

    public bool AddPlayerName(int id, string name)
    {
        if(!info.ContainsKey(id))
        {
            CreatePlayerEntry(id);
        }
        info[id].userName = name;

        Debug.Log("Player with id " + id.ToString() + " now has username " + name);

        return true;
    }

    public void ClearPlayerInfo(int id)
    {
        if (!info.ContainsKey(id))
        {
            print("not a good id");
        }
        else
        {
            info[id] = null;
        }
    }



    public PlayerInfo GetPlayerInfo(int id)
    {
        if(!info.ContainsKey(id))
        {
            return null;
        }
        return info[id];
    }
}
