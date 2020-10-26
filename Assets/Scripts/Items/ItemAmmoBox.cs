using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemAmmoBox : MonoBehaviour
{
    [SerializeField] GunScript _gunToAddAmmo = null;
    [SerializeField] Text _totalAmmo = null;
    [SerializeField] AudioClip _ammoPickUp = null;

    private int maxGunAmmoCurrent;
    private int maxgunAmmoReserve;

    void Start()
    {
        

    }
        
    void Update()
    {
        //Debug.Log();
    }
    




    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player collided with ammobox");
        }
        RefillAmmo();
        OneShotSoundManager.PlayClip2D(_ammoPickUp, 1);
        this.gameObject.SetActive(false);
    }

    void RefillAmmo()
    {
        GunScript gunToAddAmmo = new GunScript();

        int maxGunAmmoCurrent = gunToAddAmmo.MagazineCurrent;
        int maxGunAmmoReserve = gunToAddAmmo.magazineReserve;
        int RemainingAmmoToAdd;
        int ExcessAmmoToAdd;
        int ammoToAdd = 35;

        if (_gunToAddAmmo.magazineReserve == maxGunAmmoReserve)
        {
            return;
        }

        //if (_gunToAddAmmo.magazineReserve < maxGunAmmoReserve)
        //{
        //    RemainingAmmoToAdd = maxGunAmmoReserve - _gunToAddAmmo.magazineReserve;
        //    _gunToAddAmmo.magazineReserve += RemainingAmmoToAdd;
        //    _totalAmmo.text = "/ " + _gunToAddAmmo.magazineReserve.ToString();
        //}

        //ExcessAmmoToAdd = (ammoToAdd + _gunToAddAmmo.magazineReserve) - maxGunAmmoReserve;
        //_gunToAddAmmo.magazineReserve += (ammoToAdd - ExcessAmmoToAdd);

        //else if(ammoToAdd > maxGunAmmoReserve)
        //{
        //    _gunToAddAmmo.magazineReserve = maxGunAmmoReserve;
        //    _totalAmmo.text = "/ " + _gunToAddAmmo.magazineReserve.ToString();
        //}
        //else
        //{
        //    _gunToAddAmmo.magazineReserve += ammoToAdd;
        //    _totalAmmo.text = "/ " + _gunToAddAmmo.magazineReserve.ToString();
        //}

        if (_gunToAddAmmo.magazineReserve < maxGunAmmoReserve)
        {
            if (ammoToAdd >= maxGunAmmoReserve)
            {
                _gunToAddAmmo.magazineReserve = maxGunAmmoReserve;
                _totalAmmo.text = "/ " + _gunToAddAmmo.magazineReserve.ToString();
            }
            else if (ammoToAdd < maxGunAmmoReserve)
            {
                RemainingAmmoToAdd = maxGunAmmoReserve - _gunToAddAmmo.magazineReserve;
                ExcessAmmoToAdd = (ammoToAdd + _gunToAddAmmo.magazineReserve) - maxGunAmmoReserve;
                _gunToAddAmmo.magazineReserve += (ammoToAdd - ExcessAmmoToAdd);
                _totalAmmo.text = "/ " + _gunToAddAmmo.magazineReserve.ToString();
            }
        }
    }
}
