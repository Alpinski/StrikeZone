using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManger : NetworkBehaviour
{
    public Canvas pauseCanvas;

    void Update()
    {
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

        }
    }

}
      

