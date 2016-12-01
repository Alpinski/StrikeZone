using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DragonAbilities : NetworkBehaviour
{
    private float M1;
    private float M2;
    private float Q;
    private float E;
    private float LS;

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
    }

    void Update()
    {

        dir = transform.rotation;


        if (isLocalPlayer)
        {

            //raycasting where the player faces
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Plane mouseplane = new Plane(transform.up, transform.position);
            //Cooldowns for abilities
            M1 += Time.deltaTime;
            M2 += Time.deltaTime;
            Q += Time.deltaTime;
            E += Time.deltaTime;
            LS += Time.deltaTime;


            if (mouseplane.Raycast(ray, out distance))
            {
                point = ray.GetPoint(distance);

            }
            //spawns projectiles
            if (Input.GetButtonDown("Fire1") && M1 >= 0.5f)
            {
                M1 = 0;
                CmdSpawnM1FireBlot(dir);
            }

            if (Input.GetButtonDown("Fire2") && M2 >= 2)
            {
                M2 = 0;
                CmdSpawnM2FireBall(dir);
            }

            if (Input.GetButtonDown("Q") && Q >= 7)
            {
                Q = 0;
                CmdSpawnQFlameThrower(dir);
            }

            if (Input.GetButtonDown("E") && E >= 12)
            {
                E = 0;
                CmdSpawnEFlameStrike(point);
            }

            if (Input.GetButtonDown("LeftShift") && LS >= 30)
            {
                LS = 0;
                CmdSpawnLShiftMeteorStrike(point);

            }
        }
    }
    //networking
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
    }
    //spawns abilities over the network
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