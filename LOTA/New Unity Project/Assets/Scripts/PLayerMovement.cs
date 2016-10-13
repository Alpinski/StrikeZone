using UnityEngine;
using System.Collections;

public class PLayerMovement : MonoBehaviour {
    private Animator anim;
    public float speed;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
    {
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

        transform.position = transform.position + transform.right * H * speed * Time.deltaTime;
        transform.position = transform.position + transform.forward * V * speed * Time.deltaTime;
    }
}
