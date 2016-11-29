using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Skeleton_movement : NetworkBehaviour
{

    // Declearing variables
    private Animator anim;
    public float speed;
    private GameObject cam;
    public PlayerSeverPos c_networkScript;
    public float stunTime;

    // Use this for initialization
    void Start()
    {
        c_networkScript = GetComponent<PlayerSeverPos>();
        cam = Camera.main.gameObject;
        anim = GetComponent<Animator>();
        //checks to see if this script is on this client
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // gets any input to do with Horizontal and Vertical movement
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");


        // if the player is moving then play the isMoving animation else if the playe is not moving then play the ideal animation.
        if (H != 0 || V != 0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }


        //if the player is not stunned then the player can move
        if (gameObject.GetComponent<Health>().Stuned <= 0)
        {
            transform.position = transform.position + Vector3.right * H * speed * Time.deltaTime;
            transform.position = transform.position + Vector3.forward * V * speed * Time.deltaTime;
        }

        //move the Camera above the player
        cam.transform.position = transform.position + new Vector3(0, 50, -7);

        // sends a raycast from the mouses postion to the world 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane mouseplane = new Plane(transform.up, transform.position);

        float distance;
        // make the player look at the mouse where the raycast landed 
        if (mouseplane.Raycast(ray, out distance))
        {
            Vector3 point = ray.GetPoint(distance);
            transform.LookAt(point);
        }
    }
}
