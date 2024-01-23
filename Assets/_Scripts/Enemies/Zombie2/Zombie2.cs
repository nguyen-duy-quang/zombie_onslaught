using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Zombie2 : MonoBehaviour
{
    [Header("Zombie Health and Damage")]
    private float zombieHealth = 100f;
    public float presentHealth;
    public float giveDamge = 5f;
    public float timeDestroy = 4f;

    [Header("Zombie Things")]
    public NavMeshAgent zombieAgent;
    public Transform lookPoint;
    public Camera AttackingRaycastArea;
    public Transform playerBody;
    public LayerMask PlayerLayer;

    [Header("Zombie Attacking Var")]
    public float timeBtwAttack;
    private bool previouslyAttack;

    [Header("Zombie Animation")]
    public Animator animator;

    [Header("Zombie mode/states")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInvisionRadius;
    public bool playerInattackingRadius;

    public Zombie2 zombie2;
    public CapsuleCollider capsuleCollider;
    private bool isMoving = false; // Thêm biến để theo dõi trạng thái di chuyển
    private float moveDuration = 1f; // Thời gian di chuyển
    private float restDuration = 2f; // Thời gian nghỉ
    private bool isResting = false;

    [Header("Zombie2 Health")]
    [SerializeField] private Zombie2Health zombie2Health;

    [Header("Enemy Spawn")]
    public EnemySpawn enemySpawn;

    // Do Zombie1 là prefabs nên không thể kéo các GameObject chứa script của GameObject đó nên cần phải dùng event để thực thi
    public delegate void ZombieDeathAction(); // Delegate định nghĩa hành động khi zombie chết
    public static event ZombieDeathAction OnZombieDeath; // Sự kiện xảy ra khi zombie chết

    private void Awake()
    {
        CharacterLocomotion characterLocomotion = FindObjectOfType<CharacterLocomotion>();
        if (characterLocomotion != null)
        {
            playerBody = characterLocomotion.transform;
            lookPoint = playerBody.FindChild("LookPoint");
        }

        presentHealth = zombieHealth;
        zombieAgent = GetComponent<NavMeshAgent>();

        zombieAgent.speed = 1.2f;

        zombie2Health = GetComponentInChildren<Zombie2Health>();
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
        Debug.Log("Nghỉ rồi di chuyển tiếp");
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
            zombieAgent.SetDestination(hit.position);

            // Đảm bảo zombie đang di chuyển
            animator.SetBool("Walking", true);
            animator.SetBool("Attacking", false);
            animator.SetBool("Died", false);

            isMoving = true; // Đặt trạng thái di chuyển
        }
    }

    private void PersuePlayer()
    {
        if (zombieAgent.SetDestination(playerBody.position))
        {
            animator.SetBool("Walking", true);
            animator.SetBool("Attacking", false);
            animator.SetBool("Died", false);
        }
        else
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", false);
            animator.SetBool("Died", true);

        }
    }

    private void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
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

    public void zombieHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        zombie2Health.ReducedHealthSlider(takeDamage);

        if (presentHealth <= 0)
        {
            animator.SetBool("Walking", false);
            animator.SetBool("Attacking", false);
            animator.SetBool("Died", true);

            zombieDie();

            // Khi zombie chết, gửi thông điệp qua sự kiện
            if (OnZombieDeath != null)
            {
                OnZombieDeath(); // Gửi sự kiện khi zombie chết
            }
        }
    }

    private void zombieDie()
    {
        zombieAgent.SetDestination(transform.position);
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInattackingRadius = false;
        playerInvisionRadius = false;
        zombie2.enabled = false;
        capsuleCollider.enabled = false;
        Destroy(gameObject, timeDestroy);

        // Gọi phương thức để xóa zombie khỏi danh sách khi chết
        enemySpawn.RemoveEnemyFromList(gameObject);
    }
}
