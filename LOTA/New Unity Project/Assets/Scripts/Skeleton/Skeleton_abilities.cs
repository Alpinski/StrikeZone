using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Skeleton_abilities : NetworkBehaviour
{

    private Animator anim;
    public float smoothTime = 50f;
    private Vector3 velocity = Vector3.zero;
    private Vector3 tragetPos;
    private bool QAbillityUseBool = false;
    private bool mouseReady = false;
    private float QAblittyUseTimer = 3f;
    private float QAblittyCD;


    void Start ()
    {
        anim = GetComponent<Animator>();
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
    }
	

	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("IsAttacking");
        }

        QAbility();

        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("RightClick");
        }
    }

    private void QAbility()
    {
        QAblittyUseTimer -= Time.deltaTime;
        QAblittyCD -= Time.deltaTime;


        if (QAblittyUseTimer <= 0)
        {
            if (Input.GetKeyDown("q"))
            {
                mouseReady = true;
            }
        }
        if (mouseReady == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                tragetPos = transform.position + transform.forward * 30;
                QAbillityUseBool = true;
                QAblittyCD = 0.4f;
                mouseReady = false;

            }
            else if (Input.GetButtonDown("q"))
            {
                mouseReady = false;
            }
        }



        if (QAbillityUseBool == true)
        {

            transform.position = Vector3.SmoothDamp(transform.position, tragetPos, ref velocity, smoothTime, 50, 50);
            velocity = Vector3.zero;

            if (QAblittyCD <= 0)
            {
                QAbillityUseBool = false;
                QAblittyUseTimer = 3.0f;
            }
        }
    }






}
