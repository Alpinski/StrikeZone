using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    public float HP = 10000;
    private float CurrentHp;
    private bool isDead;

    private void OnEnable()
    {
        CurrentHp = HP;
        isDead = false;
    }

    private void TakeDamage(float amount)
    {
        CurrentHp -= amount;

        if (CurrentHp <= 0 && !isDead)
        {
            Dead();
        }
    }

    private void Dead()
    {
        isDead = true;
        gameObject.SetActive(false);
    }
}
