using System.Collections;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GoldShipAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    public NavMeshSurface surface;
    public Transform player;
    public Transform[] patrolPoints;

    public enum ENEMY_STATE
    {
        Idle,
        Walking,
        Chasing,
        Searching
    }
    [SerializeField] private ENEMY_STATE currentState;

    public Vector2 minMaxIdleTime;
    private float idleTime;
    private float elapsedIdleTime;

    private bool playerInRange;
    private bool playerVisible;
    [SerializeField] private LayerMask vision;

    public Vector2 minMaxSearchTime;
    private float searchTime;
    private float elapsedSearchTime;

    [SerializeField] private Animator golshiAnim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        golshiAnim = GetComponent<Animator>();
    }

    private void Start()
    {
        idleTime = Random.Range(minMaxIdleTime.x, minMaxIdleTime.y);
        searchTime = Random.Range(minMaxSearchTime.x, minMaxSearchTime.y);

    }

    private void Update()
    {

        switch (currentState)
        {
            case ENEMY_STATE.Idle:
                elapsedIdleTime += Time.deltaTime;
                if (elapsedIdleTime >= idleTime)
                {
                    elapsedIdleTime = 0;
                    ChangeEnemyState(ENEMY_STATE.Walking);
                }
                break;

            case ENEMY_STATE.Walking:
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    ChangeEnemyState(ENEMY_STATE.Idle);
                }
                break;

            case ENEMY_STATE.Chasing:
                if (playerVisible)
                    agent.SetDestination(player.position);
                else
                    ChangeEnemyState(ENEMY_STATE.Searching);
                break;

            case ENEMY_STATE.Searching:
                elapsedSearchTime += Time.deltaTime;
                if (elapsedSearchTime >= searchTime)
                {
                    elapsedSearchTime = 0;
                    if (playerInRange)
                    {
                        ChangeEnemyState(ENEMY_STATE.Chasing);
                    }
                    else
                    {
                        ChangeEnemyState(ENEMY_STATE.Idle);
                    }
                }
                break;
        }

        RaycastHit hit;

        Vector3 origin = transform.position + Vector3.up * 1.5f;

        Vector3 direction = (player.position - origin).normalized;

        float angleVision = Vector3.Angle(transform.forward, direction);

        if (angleVision < 60f && Physics.Raycast(origin, direction, out hit, 6f, vision))
        {
            if (hit.transform.CompareTag("Player"))
            {
                playerVisible = true;
                ChangeEnemyState(ENEMY_STATE.Chasing);
            }
            else
            {
                playerVisible = false;
            }
        }
    }

    private void ChangeEnemyState(ENEMY_STATE newState)
    {
        golshiAnim.SetBool("Idle", false);
        golshiAnim.SetBool("Walking", false);
        golshiAnim.SetBool("Chasing", false);
        golshiAnim.SetBool("Searching", false);

        currentState = newState;
        switch(currentState)
        {
            case ENEMY_STATE.Idle:
                golshiAnim.SetBool("Idle", true);
                golshiAnim.SetBool("Chasing", false);
                golshiAnim.SetBool("Walking", false);
                golshiAnim.SetBool("Searching", false);
                break;

            case ENEMY_STATE.Walking:
                golshiAnim.SetBool("Walking", true);
                golshiAnim.SetBool("Chasing", false);
                golshiAnim.SetBool("Searching", false);
                golshiAnim.SetBool("Idle", false);
                agent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Length)].position);
                break;

            case ENEMY_STATE.Chasing:
                golshiAnim.SetBool("Chasing", true);
                golshiAnim.SetBool("Searching", false);
                golshiAnim.SetBool("Walking", false);
                golshiAnim.SetBool("Idle", false);
                break;

            case ENEMY_STATE.Searching:
                golshiAnim.SetBool("Searching", true);
                golshiAnim.SetBool("Chasing", false);
                golshiAnim.SetBool("Walking", false);
                golshiAnim.SetBool("Idle", false);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PlayerRange"))
        {
            playerInRange = true;
            playerVisible = true;
            ChangeEnemyState(ENEMY_STATE.Chasing);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("PlayerRange"))
        {
            playerVisible = false;
            playerInRange = false;
        }

        if(currentState == ENEMY_STATE.Chasing)
        {
            ChangeEnemyState(ENEMY_STATE.Searching);
        }
    }

}

