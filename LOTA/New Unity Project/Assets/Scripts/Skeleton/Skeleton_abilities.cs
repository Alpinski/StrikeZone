using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Skeleton_abilities : NetworkBehaviour
{

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
        dir = transform.rotation;
        if (isLocalPlayer)
        {


            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger("IsAttacking");
            }

            if (Input.GetMouseButtonDown(1))
            {
                anim.SetTrigger("RightClick");
            }

            QAbility();

            EAbility();

        }
    
    }


    private void EAbility()
    {

        
        EAbilityCD -= Time.deltaTime;


        if (EAbilityCD <= 0)
        {
            if (Input.GetButtonDown("E"))
            {
                EButtonDown = true;
            }
        }

        if (EButtonDown == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hitInfo = new RaycastHit();
                bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

                if (hit == true)
                {
                    Debug.Log(hit);   
                    if (hitInfo.transform.gameObject.tag == "Player" && hitInfo.transform.gameObject != gameObject)
                    {
                        EAbilityTarget = hitInfo.transform.gameObject;
                        Debug.Log(EAbilityTarget);
                        EAbilityCD = 10;
                        CmdSpawnEAbillity(dir);
                        EButtonDown = false;
                    }
                }
            }
        }
    }


    [Command]
    void CmdSpawnEAbillity(Quaternion rot)
    {
        GameObject X = Instantiate(skullStun, transform.position + transform.forward * 3, rot) as GameObject;
        var ohBehave = X.GetComponentInChildren<RotationCorrector>();
        ohBehave.dir = rot;
        X.GetComponent<SkullStun>().tar = EAbilityTarget;
        NetworkServer.Spawn(X);
    }





    private void QAbility()
    {
        
       
        QAblittyUseTimer -= Time.deltaTime;

        if (QAblittyUseTimer <= 0)
        {
            if (Input.GetButton("Q"))
            {
                Debug.Log("mouse is ready");
                mouseReady = true;
            }
        }
        if (mouseReady == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("mouse is ready and clicked");
                targetpos = transform.position + (transform.forward * 60) + (transform.up * 20);
                
                QAbillityUseBool = true;
                QAblittyCD = 0.4f;
                mouseReady = false;

            }
        }



        if (QAbillityUseBool == true)
        {
            Debug.Log("moving player");
            if (targetpos.x < 120 && targetpos.x > -180 && targetpos.z < 170 && targetpos.z > -100)
            {
                transform.position = Vector3.SmoothDamp(transform.position, targetpos, ref velocity, smoothTime);
            }
            QAblittyCD -= Time.deltaTime;

            if (QAblittyCD <= 0)
            {
                Debug.Log("stoped moving the player");
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


}
