using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SamAbilities : NetworkBehaviour {

    private Animator anim;

    public bool isSpin = false;

    public float spinTime;

    private SamMove sammove;
    public GameObject trail;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }

        sammove = gameObject.GetComponent<SamMove>();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0) && isSpin == false)
        {
            anim.SetTrigger("isAttack");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            do
            {
                isSpin = true;
                sammove.speed = 15f;
                transform.Rotate(0f, -20f, 0f);
                trail.SetActive(true);
            } while (spinTime <= 0);
        }
        else
        {
            isSpin = false;
            sammove.speed = 30f;
            trail.SetActive(false);
        }
    }
}
