using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSeverPos : NetworkBehaviour
{
    [SyncVar]
    private Quaternion syncPlayerRot;



    [SyncVar] private Vector3 syncPos;
    [SyncVar] public bool n_isMoving;
    [SyncVar] public bool n_isAttacking;

    public Animator c_anim;
    [SerializeField] float LerpRate = 15;


    [SerializeField] private Transform myTransform;

   
    void Start()
    {
        c_anim = GetComponent<Animator>();
    }



    void Update()
    {
        

        PushVars();
        LerpPos();
        LerpRot();

        if (!isLocalPlayer)
        {
            if (n_isMoving)
            {
               c_anim.SetBool("IsMoving", true);
            }
            else
            {
                c_anim.SetBool("IsMoving", false);
            }
        
            if(n_isAttacking)
            {
                c_anim.SetTrigger("IsAttacking");
                n_isAttacking = false;
            }
        }
    }


    void LerpRot()
    {
        if (!isLocalPlayer)
        {
            myTransform.rotation = Quaternion.Lerp(myTransform.rotation, syncPlayerRot, Time.deltaTime * LerpRate);
        }
    }


    void LerpPos()
    {
        if (!isLocalPlayer)
        {
            myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * LerpRate);
        }
    }


    // all commands push varabiles to the other clinets




    [Command]
    void CmdPlayerRot(Quaternion y)
    {
        syncPlayerRot = y;
    }

    [Command]
    void CmdPlayerPos(Vector3 pos)
    {
        syncPos = pos;
    }

    [Command]
    void CmdAnimMoving(bool x)
    {
        n_isMoving = x;
    }

    [Command]
    void CmdAnimAttacking(bool z)
    {
        n_isAttacking = z;
    }


    [ClientCallback]
    void PushVars() // this pushes to the other clients
    {
        if(isLocalPlayer)
        {
            CmdPlayerPos(myTransform.position);
            CmdAnimMoving(n_isMoving);
            CmdAnimAttacking(n_isAttacking);
            CmdPlayerRot(myTransform.rotation);
        }
    }


}