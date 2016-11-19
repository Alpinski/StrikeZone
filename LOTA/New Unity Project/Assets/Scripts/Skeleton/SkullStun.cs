using UnityEngine;
using UnityEngine.Networking;

public class SkullStun : NetworkBehaviour
{


    [SyncVar]
    public GameObject tar;


    private Vector3 vel = Vector3.zero;
    public float Speed = 0.5f;
    public float stunTime;
    public float Damage;

   

    void Start()
    {

        Debug.Log(tar);
         
    }

        void Update()
    {

        transform.position = Vector3.SmoothDamp(transform.position, tar.transform.position, ref vel, Speed);

    }
   
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Health>().Stuned = stunTime;
            col.gameObject.GetComponent<Health>().TakeDamage(Damage);
            Debug.Log("stuned");
            Destroy(gameObject);
        }
    }

    
    

    

}
