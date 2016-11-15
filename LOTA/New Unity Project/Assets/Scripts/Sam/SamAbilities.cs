using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SamAbilities : NetworkBehaviour {

    private Animator anim;

    //SPINNING
    public bool isSpin = false;
    private bool isSpinning = false;

    public float spinTime;
    private float timeSpin;

    //ULTIMATE
    public bool isUlt = false;
    private bool isUlting = false;

    public float ultTime;
    public float timeUlt;

    //
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
        timeUlt = ultTime;
    }

    // Update is called once per frame
    void Update()
    {
        Spin();

        if (Input.GetMouseButtonDown(0) && isSpin == false)
        {
            anim.SetTrigger("isAttack");
        }
    }

    void Spin()
    {
        if (isSpinning == true)
        {
            transform.Rotate(0f, -20f, 0f);
            spinTime = spinTime - Time.deltaTime;

            anim.SetBool("isMoving", false);
        }

        if (spinTime <= 0)
        {
            isSpinning = false;
            isSpin = false;
            sammove.speed = 30f;
            trail.SetActive(false);
            spinTime = timeSpin;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isSpin = true;
            isSpinning = true;
            sammove.speed = 15f;
            trail.SetActive(true);
        }
    }

    void Ult()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
        }
    }
}
