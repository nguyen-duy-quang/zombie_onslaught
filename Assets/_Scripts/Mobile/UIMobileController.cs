using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMobileController : MonoBehaviour
{
    public GameMenuBase gameMenuBase;
    public MouseLook mouseLook;
    private CharacterLocomotion characterLocomotion;
    private ReloadWeapon reloadWeapon;
    private ActiveWeapon activeWeapon;
    public UIMedpack uIMedpack;

    public Button pause;
    public Button scope;
    public Button jumping;
    public Button reloading;
    public Button unarmed;
    public Button medpack;
    public Button selectPisol;
    public Button selectRifle;

    private void Awake()
    {
        characterLocomotion = GetComponentInParent<CharacterLocomotion>();
        reloadWeapon = GetComponentInParent<ReloadWeapon>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void Start()
    {
        pause.onClick.AddListener(gameMenuBase.Pause);
        scope.onClick.AddListener(mouseLook.OnClickScopeMobile);
        jumping.onClick.AddListener(characterLocomotion.Jump);
        reloading.onClick.AddListener(reloadWeapon.ReloadingOnMobile);
        unarmed.onClick.AddListener(activeWeapon.ToogleActiveWeapon);
        medpack.onClick.AddListener(uIMedpack.UsingMedpackOnMobile);
        selectPisol.onClick.AddListener(activeWeapon.SetActiveSecondary);
        selectRifle.onClick.AddListener(activeWeapon.SetActivePrimary);
    }
}
