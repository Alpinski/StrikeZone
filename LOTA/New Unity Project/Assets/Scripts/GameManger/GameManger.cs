using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManger : MonoBehaviour {
    private Canvas pauseCanvas;

    void Start()
    {
        pauseCanvas = GameObject.FindObjectOfType<Canvas>();
    }


	void Update ()
    {
       if (Input.GetKeyDown(KeyCode.Escape) && pauseCanvas.enabled == false)
        {
            pauseCanvas.enabled = true;
        }
       else
        {
            pauseCanvas.enabled = false;
        }

    }
}
