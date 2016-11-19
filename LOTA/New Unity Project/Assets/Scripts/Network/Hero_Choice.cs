using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hero_Choice : NetworkBehaviour
{
    private Choice_Manager network;
    public GameObject dragonPrefab;
    public GameObject SkeletonPrefab;
    public GameObject samuraiPrefab;
    private string userName;
    public GameObject CharacterChoice;

    Scene lobbyScene;

    void Start()
    {
        network = FindObjectOfType<Choice_Manager>();
        lobbyScene = SceneManager.GetActiveScene();
    }

    void PlayerJoinInit()
    {
        if (network == null)
            network = FindObjectOfType<Choice_Manager>();

        if (isClient)
            CmdPlayerRequestChoices();
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        var input = FindObjectOfType<UsernameChoice>();
        userName = input.nameBox.text;
        SceneManager.activeSceneChanged += SceneChanged;
        transform.GetChild(0).gameObject.SetActive(true);

        CmdPushUsernameToServer(userName);

        PlayerJoinInit();
    }

    void OnDestroy()
    {
        SceneManager.activeSceneChanged -= SceneChanged;
    }

    void SceneChanged(Scene start, Scene end)
    {
        if (end == lobbyScene)
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
    void CmdPushUsernameToServer(string x)
    {
        GameSettings.Instance.AddPlayerName(connectionToClient.connectionId, x);

    }

    [Command]
    void CmdPlayerRequestChoices()
    {
        network.RegisterPlayerJoin(connectionToClient.connectionId);
        string logMsg = "Player with ID " + connectionToClient.connectionId.ToString() + " added themselves.\n";

        logMsg += "State of dictionary:\n";
        var choices = new Dictionary<int, int>(network.PlayerChoices);
        foreach (var choice in choices)
        {
            RpcUpdateFromConnected(choice.Key, choice.Value);
            logMsg += "{"+choice.Key.ToString()+", "+choice.Value.ToString()+"}\n";
        }
        Debug.Log(logMsg);
    }

    [ClientRpc]
    void RpcUpdateFromConnected(int id, int choice)
    {
        network.RegisterPlayerJoin(id);
        network.SetPlayerTypeLobby(id, choice);

        Debug.Log("Player " + id.ToString() + "was added with choice " + choice.ToString());
    }

    [Command]
    void CmdSetPlayerChoice(int choiceIdx)
    {
        network.SetPlayerTypeLobby(connectionToClient.connectionId, choiceIdx);

        RpcSetChoice(connectionToClient.connectionId, choiceIdx);

        var choice = network.spawnPrefabs[choiceIdx];
    }

    [ClientRpc]
    void RpcSetChoice(int id, int choice)
    {
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
