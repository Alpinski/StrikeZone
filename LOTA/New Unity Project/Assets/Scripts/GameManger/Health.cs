using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{
    public float HP = 2000;
    public float CurrentHp;
    private bool isDead;
    [HideInInspector]
    public float Stuned;
    


    void Update()
    {
        if (Stuned > 0)
        {
            Stuned -= Time.deltaTime;
        }
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
            if (isClient)
            {
                CmdCheckIsDead();
            }
        
        }
    }

    [Command]
    void CmdCheckIsDead()
    {
       RpcDestroy();
    }

    private void Dead()
    {

        isDead = true;
        gameObject.SetActive(false);
        Debug.Log("DED");
    }


    [ClientRpc]
    void RpcDestroy()
    {
        Destroy(gameObject);
    }




}