using UnityEngine;
using System.Collections;

public class SwordDamage : MonoBehaviour {

    public float Damage;
    public bool takedamage = false;
    public bool isInColider = false;
    private GameObject gameobjectHitting;

    void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.tag == "Player" && collision.gameObject != transform.parent.gameObject)
        {
            isInColider = true;
            gameobjectHitting = collision.gameObject;
        }
    }

    void OnTriggerExit(Collider collision)
    {
        isInColider = false;
    }


    void Update()
    {
        if (isInColider == true && takedamage == true)
        {
            Debug.Log("hitting " + gameobjectHitting + " for " + Damage);
            gameobjectHitting.GetComponent<Health>().TakeDamage(Damage);
            Damage = 0;
            takedamage = false;
        }
        else
        {
            takedamage = false;
        }
    }

}
