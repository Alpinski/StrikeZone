using UnityEngine;
using System.Collections;

public class DragonAbilities : MonoBehaviour
{
    private float timeStamp;
    public float coolDownPeriodofInSeconds;

    public float distanceQ;

    public GameObject MU;

    private Vector3 point;

    public GameObject FlameThrower;

    public float m_LaunchForce = 30f;


    // Use this for initialization
    void Start ()
    {
        timeStamp = coolDownPeriodofInSeconds;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane mouseplane = new Plane(transform.up, transform.position);

        float distance;
        if (mouseplane.Raycast(ray, out distance))
        {
            point = ray.GetPoint(distance);
 
        }

        if (Input.GetButtonDown("LeftShift"))
        {
            Instantiate(MU, point, Quaternion.Euler(90,0,0));
        }

        timeStamp -= Time.deltaTime;

        if (Input.GetKeyDown("return"))
        {
            timeStamp = 0;
        }

        if (Input.GetButtonDown("Q") && timeStamp <= 0)
        {
            timeStamp = coolDownPeriodofInSeconds;

           GameObject X =  Instantiate(FlameThrower, transform.position + transform.forward * distanceQ + transform.up * 4.5f, transform.rotation) as GameObject;
            X.transform.parent = transform; 
        }
    }   
}