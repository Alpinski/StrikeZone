using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DragonAbilities : NetworkBehaviour
{
    private float timeStamp;
    private float coolDownPeriodofInSeconds;

    private Animator anim;

    public float distanceQ;

    public float Damage;

    public GameObject BigFB;
    public GameObject FireBolt;
    public GameObject FlameStrike;
    public GameObject FlameThrower;
    public GameObject MeteorUltimate;

    public float bulletSpeed = 10f;
   
    private Vector3 point;   

    public float m_LaunchForce = 30f;

    void Start ()
    {
        anim = GetComponent<Animator>();

        timeStamp = coolDownPeriodofInSeconds;

        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
    }
	

	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane mouseplane = new Plane(transform.up, transform.position);

        timeStamp -= Time.deltaTime;

        float distance;
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

           Quaternion targrot = transform.rotation;

            GameObject X = Instantiate(FireBolt, transform.position + transform.forward * distanceQ + transform.up * 4.5f, targrot) as GameObject;
            X.transform.position = X.transform.position + transform.forward * bulletSpeed;
        }

        if (Input.GetButtonDown("Fire2") && timeStamp <= 0)
        {
            timeStamp = coolDownPeriodofInSeconds;
            
            GameObject X = Instantiate(BigFB, transform.position + transform.forward * distanceQ + transform.up * 4.5f, transform.rotation) as GameObject;
            X.transform.parent = transform;
        }

        if (Input.GetButtonDown("Q") && timeStamp <= 0)
        {
            timeStamp = coolDownPeriodofInSeconds;

            GameObject X = Instantiate(FlameThrower, transform.position + transform.forward * distanceQ + transform.up * 4.5f, transform.rotation) as GameObject;
            X.transform.parent = transform;
        }

        if (Input.GetButtonDown("E"))
        {
            timeStamp = coolDownPeriodofInSeconds;

            Instantiate(FlameStrike, point, Quaternion.Euler(0, 0, 0));
        }

        if (Input.GetButtonDown("LeftShift"))
        {
            Instantiate(MeteorUltimate, point, Quaternion.Euler(140, 0, 0));
        }

        if (Input.GetButtonDown("Space"))
        {
            anim.SetTrigger("IsJumping");
        }
    }

   void CalculateDamage()
    {

    } 
}