using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public float HP = 2000;
    public float CurrentHp;
    private bool isDead;
    


    void Start()
    {
        
    }

    private void OnEnable()
    {
        CurrentHp = HP;
        isDead = false;
    }

   public void TakeDamage(float amount)
    {
        CurrentHp -= amount;
        Debug.Log("Hit" +CurrentHp);
        if (CurrentHp <= 0 && !isDead)
        {
            Dead();
        }
    }

    private void Dead()
    {
        isDead = true;
        gameObject.SetActive(false);
        Debug.Log("DED");
    }
}