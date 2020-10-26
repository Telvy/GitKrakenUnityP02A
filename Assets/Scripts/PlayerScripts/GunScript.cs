using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    [SerializeField] AudioClip _gunShot = null;
    [SerializeField] AudioClip _dryFire = null;
    [SerializeField] Camera _cameraController = null;
   // [SerializeField] Transform _rayOrigin = null;
    [SerializeField] float _shootDistance = 30f;
    //[SerializeField] LayerMask _hitLayers;
    public LayerMask _hitLayers;
    public bool gameIsPaused = false;

    public ParticleSystem _TempMuzzleFlash = null;
    public GameObject _hitSplash;
    private GameObject splashClone;
    RaycastHit objectHit;

    //Gun Information
    public int weaponDamage = 15;
    public const int magazineMax = 12;
    public const int magazineReserveMax = 36;

    // Let's say the player starts off with all ammo
    public int magazineCurrent = magazineMax;
    public int magazineReserve = magazineReserveMax;

    //set/get stuff for gun
    public int MagazineCurrent
    {
        get
        {
            return magazineCurrent;
        }
        set
        {
            magazineCurrent = value;
        }
    }

    public int MagazineReserve
    {
        get
        {
            return magazineReserve;
        }
        //set
        //{
        //    magazineReserve = value;
        //}
    }

    [SerializeField] Text _ammoCount = null;
    [SerializeField] Text _totalAmmo = null;


    void Awake()
    {
   
    }
    void Start()
    {
        _ammoCount.text = magazineCurrent.ToString();
        _totalAmmo.text = "/ " + magazineReserve.ToString();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TempGunFire();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            TempGunReload();
        }
    }

    private bool canGunFire()
    {
        return magazineCurrent > 0;
    }

    public void TempGunFire()
    {
        if (!gameIsPaused)
        {
            if (canGunFire())
            {
                magazineCurrent--;
                _ammoCount.text = magazineCurrent.ToString();
                //Vector3 rayDirection = _cameraController.transform.forward;
                Debug.DrawRay(_cameraController.transform.position, _cameraController.transform.forward * _shootDistance, Color.blue, 5f);
                OneShotSoundManager.PlayClip2D(_gunShot, 1);
                _TempMuzzleFlash.Play();
                if (Physics.Raycast(_cameraController.transform.position, _cameraController.transform.forward, out objectHit, _shootDistance, _hitLayers))
                {
                    splashClone = Instantiate(_hitSplash, objectHit.point, Quaternion.LookRotation(objectHit.normal));
                    Destroy(splashClone, 1f);
                    BaseEnemyAI _enemy = objectHit.transform.gameObject.GetComponent<BaseEnemyAI>();
                    if (_enemy != null)
                    {
                        _enemy.EnemyAItakeDamage(weaponDamage);
                        _enemy.setToAttack();
                        Debug.Log("Enemy took damage");
                    }
                }
                else
                {

                }
            }
            else
            {
                OneShotSoundManager.PlayClip2D(_dryFire, 1);
            }
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
}
