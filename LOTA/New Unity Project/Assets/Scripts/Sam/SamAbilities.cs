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

    private GameObject[] otherPlayers;
    private GameObject nearPlayer;

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

            otherPlayers = GameObject.FindGameObjectsWithTag("Player");

            GameObject min = null;
            float mindis = Mathf.Infinity;
            Vector3 currentPos = transform.position;

            foreach (GameObject t in otherPlayers)
            {
                if (t != gameObject)
                {
                    float distance = Vector3.Distance(t.transform.position, currentPos);

                    if (distance < mindis)
                    {
                        min = t;
                        nearPlayer = min;

                        mindis = distance;
                    }
                }
            }
        }

        if (isUlt == true)
        {
            transform.position = new Vector3 (nearPlayer.transform.position.x + 5, nearPlayer.transform.position.y, nearPlayer.transform.position.z);

            ultTime -= Time.deltaTime;
        }

        if (ultTime <= 0)
        {
            isUlt = false;

            ultTime = timeUlt;
        }
    }
}
