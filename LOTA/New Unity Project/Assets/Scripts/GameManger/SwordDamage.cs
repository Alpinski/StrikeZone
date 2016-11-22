using UnityEngine;
using System.Collections;

public class SwordDamage : MonoBehaviour {

    public float Damage;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && collision.gameObject != transform.parent.gameObject)
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(Damage);
            Damage = 0;
        }
    }
}
