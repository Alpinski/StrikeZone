using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

using UnityEngine.SceneManagement;

public class Hero_Choice : NetworkBehaviour
{
    private Choice_Manager network;
    public GameObject dragonPrefab;
    public GameObject SkeletonPrefab;
    public GameObject samuraiPrefab;

    public GameObject CharacterChoice;

    Scene lobbyScene;

    void Start()
    {
        Debug.Log("Object Started");
        network = FindObjectOfType<Choice_Manager>();

        
        lobbyScene = SceneManager.GetActiveScene();
    }

    void PlayerJoinInit()
    {
        if (network == null)
            network = FindObjectOfType<Choice_Manager>();

        if(isServer)
            CmdPlayerRequestChoices();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        SceneManager.activeSceneChanged += SceneChanged;
        transform.GetChild(0).gameObject.SetActive(true);

        PlayerJoinInit();
    }

    void SceneChanged(Scene start, Scene end)
    {
        if(end == lobbyScene)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
            network.showLobbyGUI = false;
        }
    }

    void SetPlayerChoice(GameObject choice)
    {
        if (choice == null)
        {
            Debug.LogError("Choice is null!");
            return;
        }

        int choiceId = network.spawnPrefabs.IndexOf(choice);
        if (choiceId == -1)
        {
            Debug.LogError("Choice of character has not been registered. " + choice.name);
        }
        CmdSetPlayerChoice(choiceId);
    }

    [Command]
    void CmdPlayerRequestChoices()
    {
        network.RegisterPlayerJoin(connectionToClient.connectionId);

        var choices = new Dictionary<int, int>(network.PlayerChoices);
        foreach (var choice in choices)
        {
            RpcUpdateFromConnected(choice.Key, choice.Value);
        }
    }

    [ClientRpc]
    void RpcUpdateFromConnected(int id, int choice)
    {
        network.RegisterPlayerJoin(id);
        network.SetPlayerTypeLobby(id, choice);
    }

    [Command]
    void CmdSetPlayerChoice(int choiceIdx)
    {
        network.SetPlayerTypeLobby(connectionToClient.connectionId, choiceIdx);

        RpcSetChoice(connectionToClient.connectionId, choiceIdx);

        var choice = network.spawnPrefabs[choiceIdx];
        Debug.Log("Choice of " + choice.name + " was set at index " + choiceIdx.ToString());
    }

    [ClientRpc]
    void RpcSetChoice(int id, int choice)
    {
        Debug.Log(id.ToString() + " chose " + choice.ToString());
        network.SetPlayerTypeLobby(id, choice);
    }

    public void Dragon()
    {
        CharacterChoice = dragonPrefab;
        network.showLobbyGUI = true;
        SetPlayerChoice(CharacterChoice);
    }

  public void Skeleton()
    {
        CharacterChoice = SkeletonPrefab;
        network.showLobbyGUI = true;
        SetPlayerChoice(CharacterChoice);
    }

    //The Samurai prefab plugged into this script is causing problems
   public void Samurai()
    {
        CharacterChoice = samuraiPrefab;
        network.showLobbyGUI = true;
        SetPlayerChoice(CharacterChoice);
    }





}
