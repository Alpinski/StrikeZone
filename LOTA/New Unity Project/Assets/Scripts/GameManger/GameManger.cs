using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;



public class GameManger : NetworkBehaviour
{

    private float Players;
    public Canvas pauseCanvas;
    private NetworkManager nm;
    

    void Start()
    {
        nm = FindObjectOfType<NetworkManager>();
    }

    void Update()
    {
        Players = GameObject.FindGameObjectsWithTag("Player").Length;
        if (Input.GetButtonDown("Cancel"))
        {
            if (pauseCanvas.enabled == false)
            {
                pauseCanvas.enabled = true;
            }

            else if (pauseCanvas.enabled == true)
            {
                pauseCanvas.enabled = false;
            }

          if(Players <= 0)
            {
                pauseCanvas.enabled = true;
            }
        }
    }


    void Disconnect()
    {
        Destroy(nm);
        NetworkManager.Shutdown();

        SceneManager.LoadScene("Menu");
    }


}
      

