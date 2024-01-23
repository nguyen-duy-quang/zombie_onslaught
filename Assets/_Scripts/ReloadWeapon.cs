using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ReloadWeapon : MonoBehaviour
{
    public Animator rigController;
    public WeaponAnimationEvents animationEvents;
    public ActiveWeapon activeWeapon;
    public Transform leftHand;

    //public AmmoWidget ammoWidget;

    public bool isReloading;

    GameObject magazineHand;

    private void Start()
    {
        animationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
    }

    private void Update()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        if(weapon)
        {
            if (Input.GetKeyDown(KeyCode.R) || weapon.ammoCount <= 0)
            {
                isReloading = true;
                rigController.SetTrigger("reload_weapon");

                AudioManager._instance.ShootSound(false);
                AudioManager._instance.GunReloadingSound();
            }

            if(weapon.isFiring)
            {
                activeWeapon.UpdateAmmoUI();
                //ammoWidget.Refresh(weapon.ammoCount);
            }    
        }    
    }

    public void ReloadingOnMobile()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        if (weapon)
        {
            isReloading = true;
            rigController.SetTrigger("reload_weapon");

            AudioManager._instance.ShootSound(false);
            AudioManager._instance.GunReloadingSound();

            if (weapon.isFiring)
            {
                activeWeapon.UpdateAmmoUI();
                //ammoWidget.Refresh(weapon.ammoCount);
            }
        }
    }    

    void OnAnimationEvent(string eventName)
    {
        Debug.Log(eventName);
        switch(eventName)
        {
            case "detach_magazine":
                DetachMagazine();
                break;
            case "drop_magazine":
                DropMagazine();
                break;
            case "refill_magazine":
                RefillMagazine();
                break;
            case "attach_magazine":
                AttachMagazine();
                break;
        }    
    }    

    void DetachMagazine()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        magazineHand = Instantiate(weapon.magazine, leftHand, true);
        weapon.magazine.SetActive(false);
    }   
    
    void DropMagazine()
    {
        GameObject droppedMagazine = Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();

        BoxCollider boxCollider = droppedMagazine.GetComponent<BoxCollider>(); // Lấy tham chiếu đến BoxCollider

        if (boxCollider != null)
        {
            boxCollider.center = new Vector3(-0.002f, -0.08f, 0.015f); // Đặt kích thước BoxCollider
            boxCollider.size = new Vector3(0.044f, 0.2f, 0.12f); // Đặt kích thước BoxCollider
        }

        magazineHand.SetActive(false);
        Destroy(droppedMagazine, 6f);
    }    

    void RefillMagazine()
    {
        magazineHand.SetActive(true);
    }   
    
    void AttachMagazine()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        weapon.magazine.SetActive(true);
        Destroy(magazineHand);
        weapon.ammoCount = weapon.clipSize;
        rigController.ResetTrigger("reload_weapon");

        //ammoWidget.Refresh(weapon.ammoCount);
        activeWeapon.UpdateAmmoUI();

        isReloading = false;
    }
}
