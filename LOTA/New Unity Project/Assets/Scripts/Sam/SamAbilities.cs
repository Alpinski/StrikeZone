using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SamAbilities : NetworkBehaviour {

    private Animator anim;

    public bool isSpin = false;
    private bool isSpinning = false;

    public float spinTime;
    private float timeSpin;

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

        trail.SetActive(false);

        sammove = gameObject.GetComponent<SamMove>();

        timeSpin = spinTime;
    }

    // Update is called once per frame
    void Update()
    {
        spinTime = spinTime - Time.deltaTime;

        if (isSpinning == true)
        {
            transform.Rotate(0f, -20f, 0f);
        }

        if (spinTime <= 0)
        {
            isSpinning = false;
            isSpin = false;
            sammove.speed = 30f;
            trail.SetActive(false);
            spinTime = timeSpin;
        }

        if (Input.GetMouseButtonDown(0) && isSpin == false)
        {
            anim.SetTrigger("isAttack");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isSpin = true;
            isSpinning = true;
            sammove.speed = 15f;
            trail.SetActive(true);
        }
    }
}
