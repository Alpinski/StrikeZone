using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SamMove : NetworkBehaviour {

    public Animator anim;
    public float speed;
    private GameObject cam;
    private SamAbilities samabilities;
    public float stunTime;

    // Use this for initialization
    void Start()
    {
        cam = Camera.main.gameObject;
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }

        samabilities = gameObject.GetComponent<SamAbilities>();
    }

    // Update is called once per frame
    void Update()
    {
        float H = Input.GetAxis("Horizontal");
        float V = Input.GetAxis("Vertical");

        if (H != 0 || V != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (gameObject.GetComponent<Health>().Stuned <= 0)
        {
            transform.position = transform.position + Vector3.right * H * speed * Time.deltaTime;
            transform.position = transform.position + Vector3.forward * V * speed * Time.deltaTime;
        }

        cam.transform.position = transform.position + new Vector3(0, 50, -7);


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane mouseplane = new Plane(transform.up, transform.position);

        float distance;
        if (mouseplane.Raycast(ray, out distance) && samabilities.isSpin == false && samabilities.isUlt == false)
        {
            Vector3 point = ray.GetPoint(distance);
            transform.LookAt(point);
        }
    }
}