using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float dist;
    
    [Header("Enemy Attack")]
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private float atkSpeed = 1f;
    [SerializeField] private float atkCooldown = 0f;
    [SerializeField] private float atkDamage;

    
    public StateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent { get => agent; }
    [Header("State Machine")]
    [SerializeField] private string currentState;
    public Path path;

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise(); 
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(player.position, transform.position);
        
        atkCooldown -= Time.deltaTime;

        if(dist <= 1.5f && atkCooldown <= 0f)
        {
            FindObjectOfType<AudioManager>().Play("BullymongAttack");
            playerHealth.TakeDamage(atkDamage);
            atkCooldown = 1f / atkSpeed;
        }
    }
}
