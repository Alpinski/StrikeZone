using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticalDMG : MonoBehaviour
{
    //Special Code for dragon flamethrower damag, deals damage through particles
    ParticleSystem ps;
    List<ParticleCollisionEvent> collisions = new List<ParticleCollisionEvent>();
    public float damage = 10;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag("Player")) return;
        Health h = other.GetComponent<Health>();
        if (h == null) return;

        int hits = ps.GetCollisionEvents(other, collisions);
        collisions.Clear();

        h.TakeDamage(damage * hits);
    }
}
