using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerSeverPos : NetworkBehaviour
{
    [SyncVar]
    private Quaternion syncPlayerRot;



    [SyncVar] private Vector3 syncPos;

   
    [SerializeField] float LerpRate = 15;


    [SerializeField]
    Transform myTransform;

   



    void Update()
    {
        

        PushVars();
        LerpPos();
        LerpRot();

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




    [ClientCallback]
    void PushVars() // this pushes to the other clients
    {
        if(isLocalPlayer)
        {
            CmdPlayerPos(myTransform.position);
            CmdPlayerRot(myTransform.rotation);
        }
    }


}