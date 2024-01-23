using System.Collections;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    public enum WeaponSlot
    {
        Primary = 0,
        Secondary = 1,
    }

    public Transform crossHairTarget;
    public Animator rigController;
    public Transform[] weaponSlots;

    public AmmoWidget ammoWidgetPrimary;
    public AmmoWidget ammoWidgetSecondary;
    //public AmmoWidget ammoWidget;

    public SelectedGunBackground selectedGunBackgroundPrimary;
    public SelectedGunBackground selectedGunBackgroundSecondary;

    public bool isChangingWeapon;

    RaycastWeapon[] equipped_weapons = new RaycastWeapon[2];
    int activeWeaponIndex;
    bool isHolstered = false;

    public float oldTime;
    public float nextTime = 0.2f;

    //Mobile 
    public GameManager gameManager;

    private void Start()
    {
        RaycastWeapon existingWeapon = GetComponentInChildren<RaycastWeapon>();
        if (existingWeapon)
        {
            Equip(existingWeapon);
        }    
    }

    public bool IsFiring()
    {
        RaycastWeapon currentWeapon = GetActiveWeapon();
        if (!currentWeapon)
        {
            return false;
        }
        return currentWeapon.isFiring;
    }

    public RaycastWeapon GetActiveWeapon()
    {
        return GetWeapon(activeWeaponIndex);
    }

    RaycastWeapon GetWeapon(int index)
    {
        if(index < 0 || index >= equipped_weapons.Length)
        {
            return null;
        }    
        return equipped_weapons[index];
    }    

    private void Update()
    {
        var weapon = GetWeapon(activeWeaponIndex);
        //bool notSprinting = rigController.GetCurrentAnimatorStateInfo(1).shortNameHash == Animator.StringToHash("notSprinting"); // nếu muốn vừa chạy vừa bắn thì bỏ dòng này
        //if(weapon && !isHolstered && notSprinting)
        if (weapon && !isHolstered)
        {
            if (Input.GetButton("Fire1") && gameManager.mobileInputs == false)
            {
                if (Time.time > oldTime + nextTime)
                {
                    weapon.StartFiring();
                    oldTime = Time.time;

                    if (weapon.isFiring)
                    {
                        weapon.UpdateFiring(Time.deltaTime);
                    }

                    AudioManager._instance.ShootSound(true);
                }
            }

            weapon.UpdateBullets(Time.deltaTime);
            if (Input.GetButtonUp("Fire1"))
            {
                weapon.StopFiring();
            }

            // Cập nhật số đạn trên giao diện người chơi
            UpdateAmmoUI();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            ToogleActiveWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetActivePrimary();
            /*SetActiveWeapon(WeaponSlot.Primary);*/

            //ammoWidgetPrimary.Refresh(weapon.ammoCount);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetActiveSecondary();
            /*SetActiveWeapon(WeaponSlot.Secondary);*/

            //ammoWidgetSecondary.Refresh(weapon.ammoCount);
        }
    }

    // Mobile
    public void SetActivePrimary()
    {
        SetActiveWeapon(WeaponSlot.Primary);
    }
    public void SetActiveSecondary()
    {
        SetActiveWeapon(WeaponSlot.Secondary);
    }

    public void ShootingOnMobile()
    {
        var weapon = GetWeapon(activeWeaponIndex);
        if (weapon && !isHolstered)
        {
            if (Time.time > oldTime + nextTime)
            {
                weapon.StartFiring();
                oldTime = Time.time;

                if (weapon.isFiring)
                {
                    weapon.UpdateFiring(Time.deltaTime);
                }

                AudioManager._instance.ShootSound(true);
            }
        }    
    }    

    public void UpdateAmmoUI()
    {
        RaycastWeapon weapon = GetWeapon(activeWeaponIndex);

        if (weapon != null)
        {
            if (weapon.weaponSlot == WeaponSlot.Primary)
            {
                ammoWidgetPrimary.Refresh(weapon.ammoCount, weapon.clipSize);
                selectedGunBackgroundPrimary.ShowGunBackground();
                selectedGunBackgroundSecondary.InitGunBackground();
            }
            else if (weapon.weaponSlot == WeaponSlot.Secondary)
            {
                ammoWidgetSecondary.Refresh(weapon.ammoCount, weapon.clipSize);
                selectedGunBackgroundPrimary.InitGunBackground();
                selectedGunBackgroundSecondary.ShowGunBackground();
            }
        }
    }

    public void Equip(RaycastWeapon newWeapon)
    {
        int weaponSlotIndex = (int)newWeapon.weaponSlot;
        var weapon = GetWeapon(weaponSlotIndex);
        if (weapon)
        {
            Destroy(weapon.gameObject);
        }    
        weapon = newWeapon;
        weapon.raycastDestination = crossHairTarget;
        weapon.transform.SetParent(weaponSlots[weaponSlotIndex], false);
        
        rigController.updateMode = AnimatorUpdateMode.AnimatePhysics;
        rigController.cullingMode = AnimatorCullingMode.CullUpdateTransforms;
        rigController.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        rigController.updateMode = AnimatorUpdateMode.Normal;
        equipped_weapons[weaponSlotIndex] = weapon;

        SetActiveWeapon(newWeapon.weaponSlot);

        UpdateAmmoUI();
        //ammoWidget.Refresh(weapon.ammoCount);
    }

    public void ToogleActiveWeapon()
    {
        bool isHolstered = rigController.GetBool("holster_weapon");
        if (isHolstered)
        {
            StartCoroutine(ActivateWeapon(activeWeaponIndex));
        }    
        else
        {
            StartCoroutine(HolsterWeapon(activeWeaponIndex));
        }
        AudioManager._instance.GunPickupSound();
    }

    void SetActiveWeapon(WeaponSlot weaponSlot)
    {
        int holsterIndex = activeWeaponIndex;
        int activateIndex = (int)weaponSlot;

        if(holsterIndex == activateIndex)
        {
            holsterIndex -= 1;
        }    

        StartCoroutine(SwitchWeapon(holsterIndex, activateIndex));
    }

    IEnumerator SwitchWeapon(int holsterIndex, int activateIndex)
    {
        rigController.SetInteger("weapon_index", activateIndex);
        yield return StartCoroutine(HolsterWeapon(holsterIndex));
        yield return StartCoroutine(ActivateWeapon(activateIndex));
        activeWeaponIndex = activateIndex;
    }

    IEnumerator HolsterWeapon(int index)
    {
        isChangingWeapon = true;
        isHolstered = true;
        var weapon = GetWeapon(index);
        if(weapon)
        {
            rigController.SetBool("holster_weapon", true);
            AudioManager._instance.GunPickupSound();
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
        }
        isChangingWeapon = false;
    }

    IEnumerator ActivateWeapon(int index)
    {
        isChangingWeapon = true;
        var weapon = GetWeapon(index);
        if (weapon)
        {
            rigController.SetBool("holster_weapon", false);
            rigController.Play("equip_" + weapon.weaponName);
            AudioManager._instance.GunPickupSound();
            do
            {
                yield return new WaitForEndOfFrame();
            }
            while (rigController.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f);
            isHolstered = false;
        }
        isChangingWeapon = false;
    }
}
