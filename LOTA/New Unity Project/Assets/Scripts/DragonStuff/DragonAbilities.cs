using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DragonAbilities : NetworkBehaviour
{
    private float timeStamp;
    private float coolDownPeriodofInSeconds;

    private Animator anim;

    PlayerUIController uiControl;

    public float distanceQ;

    public GameObject BigFB;
    public GameObject FireBolt;
    public GameObject FlameStrike;
    public GameObject FlameThrower;
    public GameObject MeteorUltimate;

    public GameObject face;

    private float distance;

    public float bulletSpeed = 10f;

    private Quaternion playerRot;

    [SyncVar]
    public Quaternion dir;

    [SyncVar]
    private Vector3 point;


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

        dir = transform.rotation;


        if (isLocalPlayer)
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

            if (Input.GetButtonDown("Fire1") && timeStamp <= 0)
            {

                timeStamp = coolDownPeriodofInSeconds;
                CmdSpawnM1FireBlot(dir);

            }

            if (Input.GetButtonDown("Fire2") && timeStamp <= 0)
            {

                timeStamp = coolDownPeriodofInSeconds;
                CmdSpawnM2FireBall(dir);
            }

            if (Input.GetButtonDown("Q") && timeStamp <= 0)
            {
                timeStamp = coolDownPeriodofInSeconds;
                CmdSpawnQFlameThrower(dir);
            }

            if (Input.GetButtonDown("E") && timeStamp <= 0)
            {
                timeStamp = coolDownPeriodofInSeconds;
                CmdSpawnEFlameStrike(point);
            }

            if (Input.GetButtonDown("LeftShift") && timeStamp <= 0)
            {
                timeStamp = coolDownPeriodofInSeconds;
                CmdSpawnLShiftMeteorStrike(point);

            }

            if (Input.GetButtonDown("Space") && timeStamp <= 0)
            {
                timeStamp = coolDownPeriodofInSeconds;
                anim.SetTrigger("IsJumping");
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

    //Command's 
    //Command's tell the sever to do somthing, in this case they tell the sever to spawn an instantiated prefab of the abillity that is called, 
    //eg. CmdSpawnM1FireBolt makes a instansated version of the prefab firebolt and makes it = to X, then the command tells the sever to spawn X 

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
        Debug.Log(name);
    }

    [Command]
    void CmdSpawnM1FireBlot(Quaternion rot)
    {
        GameObject X = Instantiate(FireBolt, face.transform.position, rot) as GameObject;
        var ohBehave = X.GetComponentInChildren<RotationCorrector>();
        ohBehave.dir = rot;
        NetworkServer.Spawn(X);
    }

    [Command]
    void CmdSpawnM2FireBall(Quaternion rot)
    {
        GameObject X = Instantiate(BigFB, face.transform.position, rot) as GameObject;
        var ohBehave = X.GetComponentInChildren<RotationCorrector>();
        ohBehave.dir = rot;
        NetworkServer.Spawn(X);
    }

    [Command]
    void CmdSpawnQFlameThrower(Quaternion rot)
    {
        GameObject X = Instantiate(FlameThrower, face.transform.position, rot) as GameObject;
        var ohBehave = X.GetComponentInChildren<RotationCorrector>();
        ohBehave.dir = rot;
        NetworkServer.Spawn(X);
    }

    [Command]
    void CmdSpawnEFlameStrike(Vector3 pos)
    {

        GameObject X = Instantiate(FlameStrike, pos, Quaternion.Euler(0, 0, 0)) as GameObject;
        NetworkServer.Spawn(X);
    }

    [Command]
    void CmdSpawnLShiftMeteorStrike(Vector3 pos)
    {
        GameObject X = Instantiate(MeteorUltimate, pos, Quaternion.Euler(140, 0, 0)) as GameObject;
        NetworkServer.Spawn(X);
    }

    [ClientRpc]
    void RpcRotFix(NetworkInstanceId y)
    {
        GameObject yy = NetworkServer.FindLocalObject(y);
        yy.transform.rotation = transform.rotation;
    }
}