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

        M1 += Time.deltaTime;
        M2 += Time.deltaTime;
        Q += Time.deltaTime;
        E += Time.deltaTime;
        LS += Time.deltaTime;
        //buff type ability which gives a burst of power to character
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

            //on button press plays animations and deals damage
            if (Input.GetButtonDown("Fire1") && M1 >= 6)
            {
                anim.SetTrigger("LeftClick");
                sword.GetComponent<SwordDamage>().takedamage = true;
                sword.GetComponent<SwordDamage>().Damage = 300 + Buffed;
                M1 = 0;
            }

            if (Input.GetButtonDown("Fire2") && M2 >= 8)
            {
                anim.SetTrigger("RightClick");
                sword.GetComponent<SwordDamage>().takedamage = true;
                sword.GetComponent<SwordDamage>().Damage = 500 + Buffed;
                M2 = 0;
            }
            // meant to have a stun not ready yet
            if (Input.GetButtonDown("Q") && Q >= 8)
            {
                /*
                                    // fix This  pLS
                                    anim.SetTrigger("Kick");
                                    sword.GetComponent<SwordDamage>().takedamage = true;
                                    sword.GetComponent<SwordDamage>().Damage = 300 + Buffed;
    */
            }

            if (Input.GetButtonDown("E") && E >= 12)
            {
                anim.SetTrigger("Taunt");
                IsBuffed = true;
                buffTime = 4;
                E = 0;
            }

            if (Input.GetButtonDown("LeftShift") && LS >= 30)
            {
                anim.SetTrigger("Combo");
                sword.GetComponent<SwordDamage>().takedamage = true;
                sword.GetComponent<SwordDamage>().Damage = 750 + Buffed;
                LS = 0;
            }
        }
    }
}
