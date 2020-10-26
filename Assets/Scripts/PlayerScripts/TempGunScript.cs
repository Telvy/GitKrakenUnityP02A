using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempGunScript : MonoBehaviour
{
    [SerializeField] AudioClip _gunShot = null;
    [SerializeField] AudioClip _dryFire = null;
    [SerializeField] Camera _cameraController = null;
    [SerializeField] Transform _rayOrigin = null;
    [SerializeField] float _shootDistance = 10f;
    [SerializeField] LayerMask _hitLayers;
    public bool gameIsPaused = false;

    public ParticleSystem _TempMuzzleFlash = null;
    public GameObject _hitSplash;
    private GameObject splashClone;
    RaycastHit objectHit;

    //Gun Information
    
    //public bool GunIsEmpty = false;
    public int weaponDamage = 15;
    //public int weaponAmmoCount; //current ammo
    //public int maxMagCount = 12; //max mag bullet count
    //private int timesShot = 0;
    public const int magazineMax = 12;
    public const int magazineReserveMax = 36;

    // Let's say the player starts off with all ammo
    public int magazineCurrent = magazineMax;
    public int magazineReserve = magazineReserveMax;

    //public int weaponAmmoReserve = 60;

    [SerializeField] Text _ammoCount = null;
    [SerializeField] Text _totalAmmo = null;


    void Awake()
    {
        _ammoCount.text = magazineCurrent.ToString();
        _totalAmmo.text = "/ " + magazineReserve.ToString();
    }
    void Start()
    {
       // weaponAmmoCount = maxMagCount;
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TempGunFire();
            //if (canGunFire())
            //{
            //    weaponAmmoCount--;
            //    timesShot++;
            //    _ammoCount.text = (weaponAmmoCount - timesShot).ToString();
            //}
            //else 
            //{
                
            //}
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            TempGunReload();
        } 
    }
    
   //private bool canGunFire()
   // {
   //     return weaponAmmoCount > 0 && !gameIsPaused;
   // }
    
    public void TempGunFire()
    {
        if (magazineCurrent > 0)
        {
           magazineCurrent--;
           _ammoCount.text = magazineCurrent.ToString();
           Vector3 rayDirection = _cameraController.transform.forward;
           Debug.DrawRay(_rayOrigin.position, rayDirection * _shootDistance, Color.blue, 5f);
           OneShotSoundManager.PlayClip2D(_gunShot, 1);
           _TempMuzzleFlash.Play();
            if (Physics.Raycast(_rayOrigin.position, rayDirection, out objectHit, _shootDistance, _hitLayers))
            {
                //Debug.Log("HIT" + objectHit.point);
                splashClone = Instantiate(_hitSplash, objectHit.point, Quaternion.LookRotation(objectHit.normal));
                Destroy(splashClone, 1f);
                BaseEnemyAI _enemy = objectHit.transform.gameObject.GetComponent<BaseEnemyAI>();
                if (_enemy != null)
                {
                    _enemy.EnemyAItakeDamage(weaponDamage);
                    Debug.Log("Enemy took damage");
                }
            }
            else
            {
                //Debug.Log("MISS");
            }
        }
        else
        {
            OneShotSoundManager.PlayClip2D(_dryFire, 1);
        }
    }

    private bool haveMagazineReserve()
    {
        return magazineReserve > 0;
    }



    public void TempGunReload()

    {

        if (haveMagazineReserve())

        {

            // Do nothing if we already have max bullets

            if (magazineCurrent == magazineMax)

            {

                return;

            }



            // find difference in bullets we're missing



            int reloadAmount = magazineMax - magazineCurrent;



            if (reloadAmount > magazineReserve)

            {

                magazineCurrent = magazineReserve;

                magazineReserve = 0;

                _totalAmmo.text = "/ " + magazineReserve.ToString();

                _ammoCount.text = magazineCurrent.ToString();

                return;

            }



            magazineReserve -= reloadAmount;

            _totalAmmo.text = "/ " + magazineReserve.ToString();

            magazineCurrent = magazineMax;

            _ammoCount.text = magazineCurrent.ToString();

        }

    }

    //public void TempGunReload()
    //{
    //    if (haveAmmoReserve())
    //    {

    //        if (Input.GetKeyDown(KeyCode.R))
    //        {

    //            //if (weaponAmmoCount <= 0 && GunIsEmpty)
    //            //{

    //            //    GunIsEmpty = false;
    //            //}
    //            weaponAmmoReserve -= timesShot;
    //            weaponAmmoCount = magazineReserve();
    //            _ammoCount.text = weaponAmmoCount.ToString();

    //            Debug.Log(weaponAmmoCount);


    //            _totalAmmo.text = "/ " + weaponAmmoReserve.ToString();
    //            timesShot = 0;

    //            //int spentAmmo = maxMagCount - weaponAmmoCount;
    //            //int currentMagCount = 

    //            //weaponAmmoCount = ;
    //            //weaponAmmoReserve -= spentAmmo;
    //            //_totalAmmo.text = "/ " + weaponAmmoReserve.ToString();
    //            //_ammoCount.text = weaponAmmoCount.ToString();
    //        }
    //    }
    //}


    /*
     * public void TempGunReload(int ammoPool)
    {
        //bool canReload = true;
        weaponAmmoCount = ammoPool;
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (weaponAmmoCount <= 0 && GunIsEmpty)
            {
                canFire = true;
                GunIsEmpty = false;
            }
            int spentAmmo = ammoPool - weaponAmmoCount;
            Debug.Log(spentAmmo);
            weaponAmmoCount = maxMagCount;
            weaponAmmoReserve -= spentAmmo;
            _totalAmmo.text = "/ " + weaponAmmoReserve.ToString();
            _ammoCount.text = weaponAmmoCount.ToString();

            if (weaponAmmoReserve <= 0)
            {
                weaponAmmoReserve = 0;
                //canReload = false;
            }
            Debug.Log(weaponAmmoReserve);
        }
    }
     **/
}
