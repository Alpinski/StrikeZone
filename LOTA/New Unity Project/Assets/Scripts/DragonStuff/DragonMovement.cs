using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class DragonMovement : NetworkBehaviour
{
    private Animator anim;
    public float speed;
    private GameObject cam;
    public float stunTime;

    void Start ()
    {
        anim = GetComponent<Animator>();
        cam = Camera.main.gameObject;

        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
    }
	
	void Update ()
    {
        // on button press changes characters vertical and horizontal position and plays animation
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");

        if (H != 0 || V != 0)
        {
            anim.SetBool("IsMoving", true);
        }
        else
        {
            anim.SetBool("IsMoving", false);
        }
        //refers to stun
        if (gameObject.GetComponent<Health>().Stuned <= 0)
        {
            transform.position = transform.position + Vector3.right * H * speed * Time.deltaTime;
            transform.position = transform.position + Vector3.forward * V * speed * Time.deltaTime;
        }

        // sets cam postion
        cam.transform.position = transform.position + new Vector3(0, 110, -5);
        //raycasting player rotation

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane mouseplane = new Plane(transform.up, transform.position);

        float distance;
        if (mouseplane.Raycast(ray, out distance))
        {
            Vector3 point = ray.GetPoint(distance);
            transform.LookAt(point);
        }
    }
}
