using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class ScorerAI : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform ball;
    [SerializeField] private Transform goal;
    [SerializeField] float helath, maxHealth = 100;
    [SerializeField] EnemyHealthScript healthbar;
    private Ball ballAttachedToEnemy;
    public Animator animator;
    private Transform playerBallPosition;
    Vector3 previousLocation;

    public Ball BallAttachedToEnemy { get => ballAttachedToEnemy; set => ballAttachedToEnemy = value; }

    private void Awake()
    {
        healthbar = GetComponentInChildren<EnemyHealthScript>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {

        if (ball != null && ballAttachedToEnemy == false)
        {
            transform.LookAt(ball);
            getBall();
        }
        if(ball != null && ballAttachedToEnemy == true)
        {
            Score();
            transform.LookAt(goal);
        }
        animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
    }

    public void getBall()
    {
        agent.SetDestination(ball.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Bullet")))
        {
            PlayerTakeDmg(20);
        }
    }

    public void Score()
    {
        agent.SetDestination(goal.position);
        float distanceToGoal = 0;
        distanceToGoal = Vector3.Distance(goal.position, transform.position);
        if(distanceToGoal < 10)
        {
            animator.Play("Shoot", 1, 0f);
            animator.SetLayerWeight(1, 1f);

            BallAttachedToEnemy.StickToPlayer = false;
            Rigidbody rigidbody = ballAttachedToEnemy.gameObject.GetComponent<Rigidbody>();
            Vector3 shootDirection = transform.forward;
            shootDirection.y += 0.2f;
            rigidbody.AddForce(shootDirection * (4f), ForceMode.Impulse);
            ballAttachedToEnemy = null;
        }
    }
    private void PlayerTakeDmg(float dmg)
    {
        helath -= dmg;
        healthbar.UpdateHealthBar(helath, maxHealth);
    }


}