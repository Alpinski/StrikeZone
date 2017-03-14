using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerCleanUp : MonoBehaviour {

    private NetworkManager nm;

	// Use this for initialization
	void Start () {
        nm = GameObject.FindObjectOfType<NetworkManager>();
	}

   public void DestroyServer()
    {
        nm.StopHost();
    }
}
