using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Skeleton_abilities : NetworkBehaviour
{
    // Declearing variables
    private Animator anim;
    public float smoothTime = 50f;
    public GameObject skullStun;

    [HideInInspector]
    public GameObject EAbilityTarget;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetpos;
    private bool QAbillityUseBool = false;
    private bool mouseReady = false;
    private float QAblittyUseTimer = 3f;
    private float QAblittyCD;
    private float EAbilityCD = 0;
    private bool EButtonDown = false;
    private bool colliderInfront = false;
    private float LSAbilityTimer;
    private float LSAbilityUseTimer;
    private bool LSAbilityIsInUse = false;


    public GameObject sword;

    [SyncVar]
    public Quaternion dir;

    PlayerUIController uiControl;



    void Awake()
    {
        uiControl = GetComponent<PlayerUIController>();
    }



    void Start ()
    {
        anim = GetComponent<Animator>();
    }
	

	void Update ()
    {
        //dir is the players trasform
        dir = transform.rotation;
        //checks if this is the local client
        if (isLocalPlayer)
        {

            // play swing animation M1 animation if the left mouse button is pressed and do damage to the target 
            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("IsAttacking");
                sword.GetComponent<SwordDamage>().takedamage = true;
                sword.GetComponent<SwordDamage>().Damage = 500;
            }

            // play swing animation M2 animation if the left mouse button is pressed and do damage to the target 
            if (Input.GetMouseButtonDown(1))
            {
                anim.SetTrigger("RightClick");
                sword.GetComponent<SwordDamage>().takedamage = true;
                sword.GetComponent<SwordDamage>().Damage = 750;
            }


            QAbility();

            EAbility();

            LSAbility();
        }
    
    }


    private void LSAbility()
    {
        // turns theses flaots into timers 
        LSAbilityTimer -= Time.deltaTime;
        LSAbilityUseTimer -= Time.deltaTime;
        if (LSAbilityTimer < 0)
        {
            if (Input.GetButton("LeftShift"))
            {
                transform.FindChild("DeathCloud").gameObject.SetActive(true);
                LSAbilityUseTimer = 15;
                LSAbilityIsInUse = true;
            }
        }
        if(LSAbilityUseTimer < 0 && LSAbilityIsInUse == true)
        {
            LSAbilityIsInUse = false;
            transform.FindChild("DeathCloud").gameObject.SetActive(false);
            LSAbilityTimer = 20;
        }
    }



    private void EAbility()
    {


        EAbilityCD -= Time.deltaTime;


        if (EAbilityCD <= 0)
        {

            if (Input.GetButton("E"))
            {

                    EAbilityCD = 10;
                    CmdSpawnEAbillity();
                    Debug.Log("spawned");

            }
        }
    }


    [Command]
    void CmdSpawnEAbillity()
    {
        GameObject X = Instantiate(skullStun, transform.position + transform.forward * 3, transform.rotation) as GameObject;
        var ohBehave = X.GetComponentInChildren<RotationCorrector>();
        NetworkServer.Spawn(X);
    }





    private void QAbility()
    {
        
       
        QAblittyUseTimer -= Time.deltaTime;

        if (QAblittyUseTimer <= 0)
        {
            if (Input.GetButton("Q"))
            {

                targetpos = transform.position + (transform.forward * 60) + (transform.up * 10);

                QAbillityUseBool = true;
                QAblittyCD = 0.3f;

            }

        }


        if (QAbillityUseBool == true)
        {
            if (colliderInfront == false)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetpos, ref velocity, smoothTime);
            }
            else
            {
                transform.position = transform.position + transform.forward *-2;  
            }
            QAblittyCD -= Time.deltaTime;

            if (QAblittyCD <= 0)
            {
                anim.SetTrigger("IsAttacking");
                QAbillityUseBool = false;
                QAblittyUseTimer = 3.0f;
            }
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

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject != gameObject && col.gameObject.tag != "Terrain")
        {

            colliderInfront = true;
        }
    }
    void OnCollisionExit(Collision col)
    {
        if (col.gameObject != gameObject && col.gameObject.tag != "Terrain")
        {
            colliderInfront = false;
        }
    }
}
