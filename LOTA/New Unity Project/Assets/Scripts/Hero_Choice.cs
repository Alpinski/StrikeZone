using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Hero_Choice : MonoBehaviour
{
    private NetworkLobbyManager network;
    public GameObject dragonPrefab;
    public GameObject SkeletonPrefab;
    public GameObject samuraiPrefab;

    void Start()
    {
        Debug.Log("Object Started");
        network = FindObjectOfType<NetworkLobbyManager>();

    }

    public void Dragon()
    {
        network.gamePlayerPrefab = dragonPrefab;

    }

  public void Skeleton()
    {
        network.gamePlayerPrefab = SkeletonPrefab;
        network.showLobbyGUI = true;
    }


   public void Samurai()
    {
        network.gamePlayerPrefab = samuraiPrefab;
        network.showLobbyGUI = true;
    }





}
