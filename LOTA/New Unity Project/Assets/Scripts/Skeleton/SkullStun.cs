using UnityEngine;
using UnityEngine.Networking;

public class SkullStun : NetworkBehaviour
{

    // Declearing variables
    [SyncVar]
    public GameObject tar;


    private Vector3 vel = Vector3.zero;
    public float Speed = 0.5f;
    public float stunTime;
    public float Damage;

   

    void Start()
    {
        // logs the target that the stun is going to
        Debug.Log(tar);
         
    }

        void Update()
    {
        // moves the stun to the position of the stun
        transform.position = Vector3.SmoothDamp(transform.position, tar.transform.position, ref vel, Speed);

    }
   
    void OnCollisionEnter(Collision col)
    {
        //if one of the thigs the skull colides with has the tag player then stun it, make it take damage and destroy the skull
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Health>().Stuned = stunTime;
            col.gameObject.GetComponent<Health>().TakeDamage(Damage);
            Debug.Log("stuned");
            Destroy(gameObject);
        }
    }

    
    

    

}
