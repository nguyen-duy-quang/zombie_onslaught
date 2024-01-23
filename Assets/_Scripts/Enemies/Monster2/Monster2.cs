using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster2 : MonoBehaviour
{
    [Header("Monster1 Health and Damage")]
    private float monsterHealth = 100f;
    public float presentHealth;
    public float giveDamge = 5f;
    public float timeDestroy = 4f;

    [Header("Monster1 Things")]
    public NavMeshAgent monsterAgent;
    public Transform lookPoint;
    public Camera AttackingRaycastArea;
    public Transform playerBody;
    public LayerMask PlayerLayer;

    [Header("Monster1 Attacking Var")]
    public float timeBtwAttack;
    private bool previouslyAttack;

    [Header("Monster1 Animation")]
    public Animator animator;

    [Header("Monster1 mode/states")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInvisionRadius;
    public bool playerInattackingRadius;

    public Monster2 monster2;
    public SphereCollider sphereCollider;
    private bool isMoving = false; // Thêm biến để theo dõi trạng thái di chuyển
    private float moveDuration = 1f; // Thời gian di chuyển
    private float restDuration = 2f; // Thời gian nghỉ
    private bool isResting = false;

    [Header("Monster2 Health")]
    [SerializeField] private Monster2Health monster2Health;

    [Header("Enemy Spawn")]
    public EnemySpawn enemySpawn;

    public delegate void MonsterDeathAction(); // Delegate định nghĩa hành động khi monster chết
    public static event MonsterDeathAction OnMonsterDeath; // Sự kiện xảy ra khi monster chết

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

        monsterAgent.speed = 2f;

        monster2Health = GetComponentInChildren<Monster2Health>();
    }

    private void Start()
    {
        enemySpawn = GetComponentInParent<EnemySpawn>();
    }

    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInattackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);

        if (!playerInvisionRadius && !playerInattackingRadius) Guard();
        if (playerInvisionRadius && !playerInattackingRadius) PersuePlayer();
        if (playerInvisionRadius && playerInattackingRadius) AttackPlayer();
    }

    private void Guard()
    {
        if (playerInvisionRadius || playerInattackingRadius)
        {
            // Nếu người chơi ở trong tầm nhìn hoặc tầm tấn công, không thực hiện di chuyển ngẫu nhiên
            return;
        }

        if (!isMoving && !isResting)
        {
            StartCoroutine(MoveAndRest());
        }
    }

    private IEnumerator MoveAndRest()
    {
        // Di chuyển
        MoveToRandomPosition();

        yield return new WaitForSeconds(moveDuration);
        isMoving = false; // Đặt lại trạng thái di chuyển để chuẩn bị cho lần di chuyển tiếp theo

        // Nghỉ
        isResting = true;
        yield return new WaitForSeconds(restDuration);
        isResting = false;
    }


    private void MoveToRandomPosition()
    {
        // Tạo một vị trí ngẫu nhiên trong một khoảng phạm vi 500 mét
        Vector3 randomPosition = transform.position + UnityEngine.Random.onUnitSphere * 500f;

        // Đảm bảo rằng vị trí mới nằm trên NavMesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, 500f, NavMesh.AllAreas))
        {
            // Di chuyển đến vị trí mới trên NavMesh
            monsterAgent.SetDestination(hit.position);

            // Đảm bảo monster đang di chuyển
            animator.SetBool("Walking", true);
            animator.SetBool("Running", false);
            animator.SetBool("Attacking", false);
            animator.SetBool("Died", false);

            isMoving = true; // Đặt trạng thái di chuyển
        }
    }

    private void PersuePlayer()
    {
        if (monsterAgent.SetDestination(playerBody.position))
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", true);
            animator.SetBool("Attacking", false);
            animator.SetBool("Died", false);
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Attacking", false);
            animator.SetBool("Died", true);

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

                animator.SetBool("Walking", false);
                animator.SetBool("Running", false);
                animator.SetBool("Attacking", true);
                animator.SetBool("Died", false);
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
        monster2Health.ReducedHealthSlider(takeDamage);

        if (presentHealth <= 0)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Running", false);
            animator.SetBool("Attacking", false);
            animator.SetBool("Died", true);

            monsterDie();

            // Khi monster chết, gửi thông điệp qua sự kiện
            if (OnMonsterDeath != null)
            {
                OnMonsterDeath(); // Gửi sự kiện khi monster chết
            }
        }
    }

    private void monsterDie()
    {
        monsterAgent.SetDestination(transform.position);
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInattackingRadius = false;
        playerInvisionRadius = false;
        monster2.enabled = false;
        sphereCollider.enabled = false;
        Destroy(gameObject, timeDestroy);

        // Gọi phương thức để xóa quái vật khỏi danh sách khi chết
        enemySpawn.RemoveEnemyFromList(gameObject);
    }
}
