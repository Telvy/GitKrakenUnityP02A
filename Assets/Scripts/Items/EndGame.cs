using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] GameObject _youWin = null;
    [SerializeField] AudioClip _winSound = null;

    private void OnTriggerEnter(Collider other)
    {
        //PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        //if (playerHealth != null)
        //{
        //    playerHealth.killPlayer();
        //}
        _youWin.SetActive(true);
        OneShotSoundManager.PlayClip2D(_winSound, 1);
    }
}
