using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster3 : MonoBehaviour
{
    [Header("Monster Health and Damage")]
    private float monsterHealth = 100f;
    public float presentHealth;
    public float giveDamge = 5f;
    public float timeDestroy = 4f;

    [Header("Monster Things")]
    public NavMeshAgent monsterAgent;
    public Transform lookPoint;
    public Camera AttackingRaycastArea;
    public Transform playerBody;
    public LayerMask PlayerLayer;

    [Header("Monster Guarding Var")]
    public float monsterSpeed;

    [Header("Monster Attacking Var")]
    public float timeBtwAttack;
    private bool previouslyAttack;

    [Header("Monster Animation")]
    public Animator animator;

    [Header("Monster mode/states")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInvisionRadius;
    public bool playerInattackingRadius;

    public Monster3 monster3;
    public SphereCollider sphereCollider;

    [Header("Monster3 Health")]
    [SerializeField] private Monster3Health monster3Health;

    private void Awake()
    {
        CharacterLocomotion characterLocomotion = FindObjectOfType<CharacterLocomotion>();
        if (characterLocomotion != null)
        {
            playerBody = characterLocomotion.transform;
            lookPoint = playerBody.FindChild("LookPoint");
        }

        presentHealth = monsterHealth;
        monsterAgent = GetComponent<NavMeshAgent>();

        monster3Health = GetComponentInChildren<Monster3Health>();
    }

    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        if (!playerInvisionRadius && !playerInattackingRadius) Idle();
        if (playerInvisionRadius && !playerInattackingRadius) PersuePlayer();
        if (playerInvisionRadius && playerInattackingRadius) AttackPlayer();
    }

    private void Idle()
    {
        monsterAgent.SetDestination(transform.position);
        animator.SetBool("Idle", true);
        animator.SetBool("Running", false);
    }

    private void PersuePlayer()
    {
        if (monsterAgent.SetDestination(playerBody.position))
        {
            // animations
            animator.SetBool("Idle", false);
            animator.SetBool("Running", true);
            animator.SetBool("Attacking", false);
        }
    }

    private void AttackPlayer()
    {
        monsterAgent.SetDestination(transform.position);
        transform.LookAt(lookPoint);
        if (!previouslyAttack)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(AttackingRaycastArea.transform.position, AttackingRaycastArea.transform.forward, out hitInfo, attackingRadius))
            {
                Debug.Log("Attacking" + hitInfo.transform.name);

                PlayerScript playerBody = hitInfo.transform.GetComponent<PlayerScript>();

                if (playerBody != null)
                {
                    playerBody.playerHitDamage(giveDamge);
                }

                animator.SetBool("Running", false);
                animator.SetBool("Attacking", true);
            }

            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);
        }
    }

    private void ActiveAttacking()
    {
        previouslyAttack = false;
    }

    public void monsterHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        monster3Health.ReducedHealthSlider(takeDamage);

        if (presentHealth <= 0)
        {
            animator.SetBool("Died", true);

            zombieDie();
        }
    }

    private void zombieDie()
    {
        monsterAgent.SetDestination(transform.position);
        monsterSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInattackingRadius = false;
        playerInvisionRadius = false;
        monster3.enabled = false;
        sphereCollider.enabled = false;
        Destroy(gameObject, timeDestroy);
    }
}
