using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastWeapon weaponFab;

    public GunsDisplay gunsDisplay;

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if(activeWeapon)
        {
            RaycastWeapon newWeapon = Instantiate(weaponFab);
            activeWeapon.Equip(newWeapon);

            gunsDisplay.ColorDisplay();

            AudioManager._instance.GunPickupSound();
        }    
    }
}
