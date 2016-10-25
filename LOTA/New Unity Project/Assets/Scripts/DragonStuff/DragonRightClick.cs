using UnityEngine;
using System.Collections;

public class DragonRightClick : MonoBehaviour
{
    private float timeStamp;
    public float coolDownPeriodofInSeconds;

    public GameObject m_BigFB;

    public Transform m_FireTransform;

    public float m_LaunchForce = 30f;

    private void Start()
    {
        timeStamp = coolDownPeriodofInSeconds;
    }


    void Update()
    {
        timeStamp -= Time.deltaTime;

        if (Input.GetKeyDown("return"))
        {
            timeStamp = 0;
        }

        if (Input.GetButtonDown("Fire2") && timeStamp <= 0)
        {
            timeStamp = coolDownPeriodofInSeconds;
            Fire();
        }
    }

    private void Fire()
    {
        GameObject shellInstance = Instantiate(m_BigFB, m_FireTransform.position, m_FireTransform.rotation) as GameObject;
        Rigidbody[] Rb = shellInstance.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody R in Rb) { R.velocity = m_LaunchForce * m_FireTransform.forward; }
    }
}
