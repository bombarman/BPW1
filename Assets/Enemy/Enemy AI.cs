using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyState
    {
        Patroling,
        Chasing,
        Attacking
    }

    public EnemyState CurrentState = EnemyState.Patroling;

    private NavMeshAgent agent;

    private Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    [SerializeField] private Transform _firepoint;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        ExecuteBehaviour();
    }

    private void ExecuteBehaviour()
    {
        switch (CurrentState)
        {
            case EnemyState.Patroling:
                Patroling();
                break;

            case EnemyState.Chasing:
                ChasePlayer();
                break;

            case EnemyState.Attacking:
                AttackPlayer();
                break;
        }
    }

    private void Patroling()
    {
        if (PlayerInSightRange())
        {
            CurrentState = EnemyState.Chasing;
            return;
        }

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }
        else
        {
            SearchWalkPoint();
        }


        Vector3 distanceToWalk = transform.position - walkPoint;

        //Walkpoint reached 
        if (distanceToWalk.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        if (PlayerInAttackRange())
        {
            CurrentState = EnemyState.Attacking;
            return;
        }

        if (!PlayerInSightRange())
        {
            CurrentState = EnemyState.Patroling;
            return;
        }

        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        if (!PlayerInAttackRange())
        {
            CurrentState = EnemyState.Chasing;
            return;
        }

        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Attack code here
            Rigidbody rb = Instantiate(projectile, _firepoint.position, _firepoint.rotation).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private bool PlayerInSightRange()
    {
        return Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
    }

    private bool PlayerInAttackRange()
    {
        return Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}

