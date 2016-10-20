﻿using UnityEngine;
using System.Collections;

public class Skeleton_movement : MonoBehaviour
{
    private Animator anim;
    public float speed;
    public GameObject cam;


    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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

        transform.position = transform.position + Vector3.right * H * speed * Time.deltaTime;
        transform.position = transform.position + Vector3.forward * V * speed * Time.deltaTime;

        cam.transform.position = transform.position + new Vector3(0, 50, 0);


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
