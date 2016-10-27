using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SamAbilities : NetworkBehaviour {

    private Animator anim;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("attacking");
            anim.SetTrigger("isAttack");
        }

        transform.Rotate(0, 0, 50 * Time.deltaTime);

    }
}
