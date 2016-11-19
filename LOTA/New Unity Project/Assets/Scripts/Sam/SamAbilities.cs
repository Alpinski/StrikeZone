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
    private bool isTele = false;

    public float ultTime;
    private float timeUlt;
    private float teleTime = 0.25f;

    //
    private SamMove sammove;

    public GameObject trail;

    private GameObject[] otherPlayers;
    private GameObject nearPlayer;

    private float xRand;
    private float zRand;

    PlayerUIController uiControl;



    void Awake()
    {
        uiControl = GetComponent<PlayerUIController>();
    }





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
            transform.position = new Vector3 (nearPlayer.transform.position.x + xRand, 
                                                nearPlayer.transform.position.y,
                                                nearPlayer.transform.position.z + zRand);

            if (isTele == true)
            {
                xRand = RandRand(Random.Range(-4f, -2f), Random.Range(2f, 4f));
                zRand = RandRand(Random.Range(-4f, -2f), Random.Range(2f, 4f));

                isTele = false;
            }

            if (teleTime >= 0.25f)
            {
                isTele = true;

                teleTime = 0;
            }

            if (teleTime <= 0.25f)
            {
                teleTime += Time.deltaTime;
            }

            ultTime -= Time.deltaTime;
        }

        if (ultTime <= 0)
        {
            isUlt = false;

            ultTime = timeUlt;
        }
    }

    private float RandRand(float x, float y)
    { 
        int stuff = Random.Range(0, 2);
        if (stuff == 0)
        {
            return x;
        }
        else
        {
            return y;
        }
    }



    void LateUpdate()
    {
        uiControl.UpdatePosition(transform.position);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        CmdFetchPlayerInfo();
    }


    [Command]
    void CmdFetchPlayerInfo()
    {
        var info = GameSettings.Instance.GetPlayerInfo(connectionToClient.connectionId);
        uiControl.SetPlayerName(info.userName);
        RpcUpdatePlayerUI(info.userName);
    }

    [ClientRpc]
    void RpcUpdatePlayerUI(string name)
    {
        uiControl.SetPlayerName(name);
    }




}
