using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GoldShipAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    public NavMeshSurface surface;
    public Transform player;
    public List<Transform> patrolPoints = new List<Transform>();

    public enum ENEMY_STATE
    {
        Idle,
        Walking,
        Chasing,
        Searching,
        Stunned,
        Attack
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

    private float stunnedTime = 5f;
    private float elapsedStunnedTime;

    private float canKill;

    private AudioManager audioManager;


    [SerializeField] private Animator golshiAnim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        golshiAnim = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
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
            case ENEMY_STATE.Stunned:
                elapsedStunnedTime += Time.deltaTime;
                if (elapsedStunnedTime >= stunnedTime)
                {
                    elapsedStunnedTime = 0;
                    agent.isStopped = false;

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

        if (currentState == ENEMY_STATE.Stunned) return;

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

    public void ChangeEnemyState(ENEMY_STATE newState)
    {
        golshiAnim.SetBool("Idle", false);
        golshiAnim.SetBool("Walking", false);
        golshiAnim.SetBool("Chasing", false);
        golshiAnim.SetBool("Searching", false);

        if (newState == ENEMY_STATE.Chasing)
        {
            audioManager.PlayChaseMusic();
        }
        else if (currentState == ENEMY_STATE.Chasing && newState != ENEMY_STATE.Chasing)
        {
            audioManager.PlayNormalMusic();
        }

        currentState = newState;

        switch(currentState)
        {
            case ENEMY_STATE.Idle:
                golshiAnim.SetBool("Idle", true);
                golshiAnim.SetBool("Chasing", false);
                golshiAnim.SetBool("Walking", false);
                golshiAnim.SetBool("Searching", false);
                golshiAnim.SetBool("Pain", false);
                break;

            case ENEMY_STATE.Walking:
                golshiAnim.SetBool("Walking", true);
                golshiAnim.SetBool("Chasing", false);
                golshiAnim.SetBool("Searching", false);
                golshiAnim.SetBool("Idle", false);
                golshiAnim.SetBool("Pain", false);
                agent.SetDestination(patrolPoints[Random.Range(0, patrolPoints.Count)].position);
                break;

            case ENEMY_STATE.Chasing:
                golshiAnim.SetBool("Chasing", true);
                golshiAnim.SetBool("Searching", false);
                golshiAnim.SetBool("Walking", false);
                golshiAnim.SetBool("Idle", false);
                golshiAnim.SetBool("Pain", false);
                break;

            case ENEMY_STATE.Searching:
                golshiAnim.SetBool("Searching", true);
                golshiAnim.SetBool("Chasing", false);
                golshiAnim.SetBool("Walking", false);
                golshiAnim.SetBool("Idle", false);
                golshiAnim.SetBool("Pain", false);
                break;
            case ENEMY_STATE.Stunned:
                golshiAnim.SetBool("Pain", true);
                golshiAnim.SetBool("Searching", false);
                golshiAnim.SetBool("Chasing", false);
                golshiAnim.SetBool("Walking", false);
                golshiAnim.SetBool("Idle", false);

                agent.isStopped = true;
                break;
            case ENEMY_STATE.Attack:
                SceneManager.LoadScene("Escena Muerte");
                break;

        }
    }

    public void AddPatrolPoints(Transform[] newPoints)
    {
        for (int i = 0; i < newPoints.Length; i++)
        {
            if (!patrolPoints.Contains(newPoints[i]))
            {
                patrolPoints.Add(newPoints[i]);
            }
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

        if (other.CompareTag("Player"))
        {
            if (other.CompareTag("Player") && currentState != ENEMY_STATE.Stunned && currentState != ENEMY_STATE.Searching)
            {
                ChangeEnemyState(ENEMY_STATE.Attack);
            }
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

