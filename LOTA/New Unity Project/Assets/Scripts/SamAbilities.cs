using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SamAbilities : MonoBehaviour {

    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("attacking");
            anim.SetTrigger("isAttack");
        }

    }
}
