using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempEnemy : MonoBehaviour
{

    int enemyHealth = 100;

    void Update()
    {
        killTempEnemy();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player collided with enemy");
        }
       
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            //Debug.Log("enemy damage player");
            playerHealth.hurtPlayer(30);
        }
    }

    public void TakeDamage(int _damageToTake)
    {
        enemyHealth -= _damageToTake;
    }

    void killTempEnemy()
    {
        if(enemyHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
