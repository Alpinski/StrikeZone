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

        timeStamp -= Time.deltaTime;


        if (mouseplane.Raycast(ray, out distance))
        {
            point = ray.GetPoint(distance);

        }


        if (Input.GetKeyDown("return"))
        {
            timeStamp = 0;
        }

        dir = transform.rotation;
        if (isLocalPlayer)
        {


            if (Input.GetButtonDown("Fire1") && timeStamp <= 0)
            {
                anim.SetTrigger("LeftClick");
                sword.GetComponent<SwordDamage>().Damage = 500;
            }

            if (Input.GetButtonDown("Fire2"))
            {
                anim.SetTrigger("RightClick");
                sword.GetComponent<SwordDamage>().Damage = 750;
            }

            if (Input.GetButtonDown("Q"))
            {
                anim.SetTrigger("JumpAtt");
                sword.GetComponent<SwordDamage>().Damage = 750;
            }

        }

    }
}
