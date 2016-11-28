using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AxeAbilities : NetworkBehaviour
{
    private float timeStamp;
    private float coolDownPeriodofInSeconds;
    private Animator anim;
    public float smoothTime = 50f;
    public GameObject Brute;
    private float Buffed;
    private float buffTime;
    private bool IsBuffed = false;
    private float M1;
    private float M2;
    private float Q;
    private float E;
    private float LS;

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

    private Vector3 point;
    private float distance;

    public GameObject sword;

    [SyncVar]
    public Quaternion dir;

    PlayerUIController uiControl;



    void Awake()
    {
        uiControl = GetComponent<PlayerUIController>();
    }



    void Start()
    {
        anim = GetComponent<Animator>();

        timeStamp = coolDownPeriodofInSeconds;
    }


    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane mouseplane = new Plane(transform.up, transform.position);

        M1 -= Time.deltaTime;
        M2 -= Time.deltaTime;
        Q -= Time.deltaTime;
        E -= Time.deltaTime;
        LS -= Time.deltaTime;

        buffTime -= Time.deltaTime;
        Debug.Log(buffTime);
        if (IsBuffed == true)
        {
            Buffed = 250;
            Debug.Log("Buffed");
            gameObject.GetComponent<AxeMovement>().speed = 80;
        }
        if (buffTime < 0)
        {
            Buffed = 0;
            IsBuffed = false;
            gameObject.GetComponent<AxeMovement>().speed = 50;
            Debug.Log("done");
        }

        if (mouseplane.Raycast(ray, out distance))
        {
            point = ray.GetPoint(distance);

        }

        dir = transform.rotation;
        if (isLocalPlayer)
        {


            if (Input.GetButtonDown("Fire1") && M1 <= 2)
            {
                anim.SetTrigger("LeftClick");
                sword.GetComponent<SwordDamage>().takedamage = true;
                sword.GetComponent<SwordDamage>().Damage = 300 + Buffed;
            }

            if (Input.GetButtonDown("Fire2") && M2 <= 4)
            {
                anim.SetTrigger("RightClick");
                sword.GetComponent<SwordDamage>().takedamage = true;
                sword.GetComponent<SwordDamage>().Damage = 500 + Buffed;
            }

            if (Input.GetButtonDown("Q") && Q <= 7)
            {
                EAbilityCD -= Time.deltaTime;
                EButtonDown = true;

                if (EAbilityCD <= 0)
                {
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
                                    EAbilityCD = 10;
                                    EButtonDown = false;
                                    hitInfo.transform.gameObject.GetComponent<Health>().Stuned = 2;
                                    // fix This Fgt pLS
                                    anim.SetTrigger("Kick");
                                    sword.GetComponent<SwordDamage>().takedamage = true;
                                    sword.GetComponent<SwordDamage>().Damage = 300 + Buffed;
                                }
                            }
                        }
                    }
                }
            }

            if (Input.GetButtonDown("E") && E <= 12)
            {
                anim.SetTrigger("Taunt");
                IsBuffed = true;
                buffTime = 4;
            }

            if (Input.GetButtonDown("LeftShift") && LS <= 60)
            {
                anim.SetTrigger("Combo");
                sword.GetComponent<SwordDamage>().takedamage = true;
                sword.GetComponent<SwordDamage>().Damage = 750 + Buffed;
            }
        }
    }
}
