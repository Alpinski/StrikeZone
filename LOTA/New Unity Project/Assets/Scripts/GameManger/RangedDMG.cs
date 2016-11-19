using UnityEngine;
using System.Collections;

public class RangedDMG : MonoBehaviour
{
    public float Damage = 500;

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Health>().TakeDamage(Damage);  
            Destroy(this);       
        }
    }
}
