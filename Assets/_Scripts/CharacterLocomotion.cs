using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLocomotion : MonoBehaviour
{
    public Animator rigController;
    public float jumpHeight;
    public float gravity;
    public float stepDown;
    public float airControl;
    public float jumpDamp;
    public float groundSpeed;
    public float pushPower;

    Animator animator;
    CharacterController cc;
    ActiveWeapon activeWeapon;
    ReloadWeapon reloadWeapon;
    Vector2 input;

    Vector3 rootMotion;
    Vector3 velocity;
    bool isJumping;

    int isSprintingParam = Animator.StringToHash("isSprinting");

    // Mobile
    public GameManager gameManager;
    public FixedJoystick joystick;
    public FixedJoystick sprintJoystick;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
        activeWeapon = GetComponent<ActiveWeapon>();
        reloadWeapon = GetComponent<ReloadWeapon>();
    } 

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        UpdateIsSprinting();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    /*private void PlayerMovement()
    {
        if(gameManager.mobileInputs)
        {
            input.x = joystick.Horizontal;
            input.y = joystick.Vertical;

            animator.SetFloat("InputX", input.x);
            animator.SetFloat("InputY", input.y);
        }    
        else
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            animator.SetFloat("InputX", input.x);
            animator.SetFloat("InputY", input.y);
        }    
    }*/
    private void PlayerMovement()
    {
        if (gameManager.mobileInputs)
        {
            input.x = joystick.Horizontal;
            input.y = joystick.Vertical;

            // Kiểm tra nếu sprintJoystick đang được sử dụng
            if (sprintJoystick != null && sprintJoystick.isActiveAndEnabled)
            {
                input.x += sprintJoystick.Horizontal;
                input.y += sprintJoystick.Vertical;
            }
            if (Mathf.Abs(input.x) > 0.3f || Mathf.Abs(input.y) > 0.3f)
            {
                animator.SetFloat("InputX", input.x);
                animator.SetFloat("InputY", input.y);
            }    
            else
            {
                animator.SetFloat("InputX", 0);
                animator.SetFloat("InputY", 0);
            }    
        }
        else
        {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            animator.SetFloat("InputX", input.x);
            animator.SetFloat("InputY", input.y);
        }
    }

    /* bool IsSprinting()
     {
         input.x = joystick.Horizontal;
         bool isSprinting = Input.GetKey(KeyCode.LeftShift);
         bool isFiring = activeWeapon.IsFiring();
         bool isReloading = reloadWeapon.isReloading;
         bool isChangingWeapon = activeWeapon.isChangingWeapon;
         return isSprinting && !isFiring && !isReloading && !isChangingWeapon;
     }    */
    bool IsSprinting()
    {
        bool isSprintingKey = Input.GetKey(KeyCode.LeftShift);

        // Kiểm tra nếu sprintJoystick đang được sử dụng và giá trị vượt quá 0.5
        bool isSprintingJoystick = sprintJoystick != null && sprintJoystick.isActiveAndEnabled &&
                                    (Mathf.Abs(sprintJoystick.Horizontal) > 0.5f || Mathf.Abs(sprintJoystick.Vertical) > 0.5f);

        bool isFiring = activeWeapon.IsFiring();
        bool isReloading = reloadWeapon.isReloading;
        bool isChangingWeapon = activeWeapon.isChangingWeapon;

        // Chỉ chạy khi giá trị của sprintJoystick vượt quá 0.5 và không có hành động nào khác đang xảy ra
        if (isSprintingKey || isSprintingJoystick)
        {
            if (!isFiring && !isReloading && !isChangingWeapon)
            {
                return true;
            }
        }

        return false;
    }


    private void UpdateIsSprinting()
    {
        bool isSprinting = IsSprinting();
        animator.SetBool(isSprintingParam, isSprinting);
        rigController.SetBool(isSprintingParam, isSprinting);
    }

    private void OnAnimatorMove()
    {
        rootMotion += animator.deltaPosition;
    }

    private void FixedUpdate()
    {
        if (isJumping) // IsInAir state
        {
            UpdateInAir();
        }
        else // IsGrounded state
        {
            UpdateOnGround();
        }
    }

    private void UpdateOnGround()
    {
        Vector3 stepForwardAmount = rootMotion * groundSpeed;
        Vector3 stepDownAmount = Vector3.down * stepDown;

        cc.Move(stepForwardAmount + stepDownAmount);
        rootMotion = Vector3.zero;

        if (!cc.isGrounded)
        {
            SetInAir(0);
        }
    }

    private void UpdateInAir()
    {
        velocity.y -= gravity * Time.fixedDeltaTime;
        Vector3 displacement = velocity * Time.fixedDeltaTime;
        displacement += CalculateAirControl();
        cc.Move(displacement);
        isJumping = !cc.isGrounded;
        rootMotion = Vector3.zero;
        animator.SetBool("isJumping", isJumping);
    }

    Vector3 CalculateAirControl()
    {
        return ((transform.forward * input.y) + (transform.right * input.x)) * (airControl / 100);
    }    

    public void Jump()
    {
        if(!isJumping)
        {
            float jumpVeclocity = Mathf.Sqrt(2 * gravity * jumpHeight);
            SetInAir(jumpVeclocity);

            AudioManager._instance.JumpSound();
        }
    }

    private void SetInAir(float jumpVeclocity)
    {
        isJumping = true;
        velocity = animator.velocity * jumpDamp * groundSpeed;
        velocity.y = jumpVeclocity;
        animator.SetBool("isJumping", true);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        // no rigidbody
        if (body == null || body.isKinematic)
            return;

        // We dont want to push objects below us
        if (hit.moveDirection.y < -0.3f)
            return;

        // Calculate push direction from move direction,
        // we only push objects to the sides never up and down
        Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        // If you know how fast your character is trying to move,
        // then you can also multiply the push velocity by that.

        // Apply the push
        body.velocity = pushDir * pushPower;
    }
}
