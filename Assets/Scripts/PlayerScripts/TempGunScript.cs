using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempGunScript : MonoBehaviour
{
    [SerializeField] AudioClip _gunShot = null;
    [SerializeField] Camera _cameraController;
    [SerializeField] Transform _rayOrigin;
    [SerializeField] float _shootDistance = 10f;
    [SerializeField] int weaponDamage = 15;
    [SerializeField] LayerMask _hitLayers;
    

    public ParticleSystem _TempMuzzleFlash = null;
    public GameObject _hitSplash;
    private GameObject splashClone;
    RaycastHit objectHit;
    public bool canFire;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            fire();
        }
    }

    public void fire()
    {
        if (canFire == true)
        {
           Vector3 rayDirection = _cameraController.transform.forward;
           Debug.DrawRay(_rayOrigin.position, rayDirection * _shootDistance, Color.blue, 5f);
           if (Physics.Raycast(_rayOrigin.position, rayDirection, out objectHit, _shootDistance, _hitLayers))
           {
                //Debug.Log("HIT" + objectHit.point);
                splashClone = Instantiate(_hitSplash, objectHit.point, Quaternion.LookRotation(objectHit.normal));
                Destroy(splashClone, 1f);
                BaseEnemyAI _enemy = objectHit.transform.gameObject.GetComponent<BaseEnemyAI>();
                if (_enemy != null)
                {
                    _enemy.TakeDamage(weaponDamage);
                    Debug.Log("Enemy took damage");
                }
           }
           else
           {
                //Debug.Log("MISS");
           }
           OneShotSoundManager.PlayClip2D(_gunShot, 1);
           _TempMuzzleFlash.Play();
        } 
    }
}
