using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Hero_Choice : MonoBehaviour
{
    static Hero_Choice instance;

    private NetworkLobbyManager network;
    public GameObject dragonPrefab;
    public GameObject SkeletonPrefab;
    public GameObject samuraiPrefab;

    public GameObject CharacterChoice;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Debug.Log("Object Started");
        network = FindObjectOfType<NetworkLobbyManager>();
    }

    public void Dragon()
    {
        CharacterChoice = dragonPrefab;
        network.showLobbyGUI = true;
    }

  public void Skeleton()
    {
        CharacterChoice = SkeletonPrefab;
        network.showLobbyGUI = true;
    }


   public void Samurai()
    {
        CharacterChoice = samuraiPrefab;
        network.showLobbyGUI = true;
    }





}
