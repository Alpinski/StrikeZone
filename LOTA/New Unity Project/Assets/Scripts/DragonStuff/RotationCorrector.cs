using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class RotationCorrector : NetworkBehaviour {

    [SyncVar]
    [HideInInspector]
    public Quaternion dir;

    public override void OnStartClient()
    {
        base.OnStartClient();

        transform.rotation = dir;
    }
}
