using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SamAbilities : NetworkBehaviour {

    private Animator anim;

    //SPINNING
    public bool isSpin = false;

    public float spinTime;
    private float timeSpin;

    //ULTIMATE
    public bool isUlt = false;

    public float ultTime;
    private float timeUlt;

    //
    private SamMove sammove;
    public GameObject trail;

    private GameObject otherPlayer;

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

        Ult();

        if (Input.GetMouseButtonDown(0) && isSpin == false && isUlt == false)
        {
            anim.SetTrigger("isAttack");
        }
    }

    void Spin()
    {
        if (isSpin == true)
        {
            transform.Rotate(0f, -20f, 0f);
            spinTime = spinTime - Time.deltaTime;

            if (sammove.speed <= 45f)
            {
                sammove.speed = sammove.speed + 0.1f;
            }

            anim.SetBool("isMoving", false);
        }

        if (spinTime <= 0)
        {
            isSpin = false;
            sammove.speed = 30f;
            trail.SetActive(false);
            spinTime = timeSpin;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            isSpin = true;
            trail.SetActive(true);
            sammove.speed = 20f;
        }
    }

    void Ult()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isUlt = true;
            otherPlayer = GameObject.FindWithTag("Player");
            Debug.Log(":p");
        }

        if (isUlt == true)
        {
            transform.position = new Vector3 (otherPlayer.transform.position.x + 5, otherPlayer.transform.position.y, otherPlayer.transform.position.z);

            ultTime -= Time.deltaTime;
        }

        if (ultTime <= 0)
        {
            isUlt = false;

            ultTime = timeUlt;
        }
    }
}
