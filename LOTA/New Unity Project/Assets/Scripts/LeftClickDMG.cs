using UnityEngine;
using System.Collections;

public class LeftClickDMG : MonoBehaviour
{
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Dragon" || col.gameObject.tag == "Skeleton" || col.gameObject.tag == "Samurai")
        {
            col.gameObject.GetComponent<Health>().TakeDamage(5000);  
            Destroy(gameObject);       
        }
    }
}
