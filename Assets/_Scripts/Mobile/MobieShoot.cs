using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MobieShoot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isHoldingButton = false; // Biến xác định xem nút button có đang được nhấn giữ hay không

    private ActiveWeapon activeWeapon;

    private void Start()
    {
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    void Update()
    {
        if (isHoldingButton == true)
        {
            activeWeapon.ShootingOnMobile();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Khi nút button được nhấn giữ
        isHoldingButton = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Khi nút button được nhả ra
        isHoldingButton = false;
    }
}
