using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Text _healthPoints = null;
    [SerializeField] GameObject _youDied = null;
    [SerializeField] GameObject Player = null;
    [SerializeField] AudioClip _deathSound = null;
    [SerializeField] AudioClip _painImpactSound = null;

    public int playerHealth = 100;
   // public int currentHealth;
    bool dead = false;

    public HealthBar _healthBar;

    void Start()
    {
        //currentHealth = playerHealth;
        _healthBar.SetMaxHealth(playerHealth);
    }

    public void hurtPlayer(int damage)
    {
        if (!dead)
        {
            playerHealth -= damage;
            _healthPoints.text = playerHealth.ToString();
            _healthBar.SetHealth(playerHealth);
            OneShotSoundManager.PlayClip2D(_painImpactSound, 1);
           

            if (playerHealth < 0)
            {
                playerHealth = 0;
                _healthPoints.text = playerHealth.ToString();
                dead = true;
                killPlayer();
            }
        }
    }

    public void killPlayer()
    {
        Debug.Log("Player is kil");
        OneShotSoundManager.PlayClip2D(_deathSound, 1);
        Player.SetActive(false);
        _youDied.SetActive(true);
    }

    
}
