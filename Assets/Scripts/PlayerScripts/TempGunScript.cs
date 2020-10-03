using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGunScript : MonoBehaviour
{
    [SerializeField] AudioClip _gunShot = null;

    public ParticleSystem _TempMuzzleFlash = null;
    public bool canFire;

    void Update()
    {
        fire();
    }

    public void fire()
    {
        if (canFire == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                OneShotSoundManager.PlayClip2D(_gunShot, 1);
                _TempMuzzleFlash.Play();
                Debug.Log("gun fired");
            }
        } 
    }
}
