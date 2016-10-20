using UnityEngine;
using System.Collections;

public class Skeleton_abilities : MonoBehaviour {

    private Animator anim;

	void Start ()
    {
        anim = GetComponent<Animator>();
	}
	

	void Update ()
    {
	
        if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("IsAttacking");
        }

        if(Input.GetKeyDown("q"))
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward * 5, 5);
        }




	}
}
