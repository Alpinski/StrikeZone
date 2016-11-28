using UnityEngine;
using System.Collections;

public class DeathCloud : MonoBehaviour
{

    void OnTriggerStay(Collider col)
    {

        if (col.gameObject.tag == "Player" && col.gameObject != transform.parent.gameObject)
        {
            col.gameObject.GetComponent<Health>().TakeDamage(10);
 
        }


    }
    void OnTriggerEnter(Collider col)
    {

        if (gameObject.GetComponent<DragonMovement>() != null)
        {
            Debug.Log("dragon slowed");
            col.gameObject.GetComponent<DragonMovement>().speed = 15;
        }
        if (gameObject.GetComponent<Skeleton_movement>() != null)
        {
            Debug.Log("skeleton slowed");
            col.gameObject.GetComponent<Skeleton_movement>().speed = 15;
        }
        if (gameObject.GetComponent<SamMove>() != null)
        {
            Debug.Log("Samurai slowed");
            col.gameObject.GetComponent<SamMove>().speed = 15;
        }
        if (gameObject.GetComponent<AxeMovement>() != null)
        {
            Debug.Log("Brute slowed");
            col.gameObject.GetComponent<AxeMovement>().speed = 15;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (gameObject.GetComponent<DragonMovement>() != null)
        {
            Debug.Log("dragon slowed");
            col.gameObject.GetComponent<DragonMovement>().speed = 30;
        }
        if (gameObject.GetComponent<Skeleton_movement>() != null)
        {
            Debug.Log("skeleton slowed");
            col.gameObject.GetComponent<Skeleton_movement>().speed = 30;
        }
        if (gameObject.GetComponent<SamMove>() != null)
        {
            Debug.Log("Samurai slowed");
            col.gameObject.GetComponent<SamMove>().speed = 30;
        }
        if (gameObject.GetComponent<AxeMovement>() != null)
        {
            Debug.Log("Brute slowed");
            col.gameObject.GetComponent<AxeMovement>().speed = 30;
        }
    }


    void Update()
    {
        transform.rotation = Quaternion.identity;
    }





}


