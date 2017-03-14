using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameManger : MonoBehaviour
{
    [SerializeField]
    private Canvas pauseCanvas;
    private bool enabled = false;

    string x = "hello";

    void Start()
    {


        print(x);

        pauseCanvas = GameObject.Find("pauseCanvas").GetComponent<Canvas>();
        pauseCanvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
         
            if (enabled == false)
            {
                pauseCanvas.enabled = true;
                enabled = true;
            }

         else
          {
              pauseCanvas.enabled = false;
              enabled = false;
          }

        }
    }

}
      

