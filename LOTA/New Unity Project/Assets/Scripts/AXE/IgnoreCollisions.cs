using UnityEngine;
using System.Collections;

public class IgnoreCollisions : MonoBehaviour
{
    public Collider toIgnore;

    void Awake()
    {
        
        Physics.IgnoreCollision(GetComponent<Collider>(), toIgnore);
    }
}
