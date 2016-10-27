using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Hero_Choice : MonoBehaviour
{
    public GameObject network;
    public GameObject dragonPrefab;
    public GameObject SkeletonPrefab;
    public GameObject samuraiPrefab;

    void Start()
    {
        Debug.Log("Object Started");
    }

    public void Dragon()
    {
        network.GetComponent<NetworkLobbyManager>().gamePlayerPrefab = dragonPrefab;

    }

  public void Skeleton()
    {
        network.GetComponent<NetworkLobbyManager>().gamePlayerPrefab = SkeletonPrefab;
        network.GetComponent<NetworkLobbyManager>().showLobbyGUI = true;
    }


   public void Samurai()
    {
        network.GetComponent<NetworkLobbyManager>().gamePlayerPrefab = samuraiPrefab;
        network.GetComponent<NetworkLobbyManager>().showLobbyGUI = true;
    }





}
