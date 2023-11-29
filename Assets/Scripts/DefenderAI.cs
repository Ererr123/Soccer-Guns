using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class DefenderAI : MonoBehaviour
{
    public GameObject goalie;
    public GameObject ball;
    public float sideMovementSpeed = 5f;
    public float maxDistanceFromGoal = 10f;
    [SerializeField] float helath, maxHealth = 3000;
    public Animator animator;
    [SerializeField] EnemyHealthScript healthbar;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("AllyBullet")))
        {
            PlayerTakeDmg(20);
        }
    }
    void Update()
    {
        // Stay in line with the ball but within limits
        Vector3 ballPosition = ball.transform.position;
        Vector3 goaliePosition = goalie.transform.position;
        float targetX = Mathf.Clamp(ballPosition.x, goaliePosition.x - maxDistanceFromGoal, goaliePosition.x + maxDistanceFromGoal);
        // Move side to side
        Vector3 targetPosition = new Vector3(targetX, startPosition.y, startPosition.z);
        if (targetPosition.x > 0)
        {
            animator.SetBool("left", false);
            animator.SetBool("right", true);
        }
        else if (targetPosition.x < 0)
        {
            animator.SetBool("left", true);
            animator.SetBool("right", false);
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, sideMovementSpeed * Time.deltaTime);
    }

    private void PlayerTakeDmg(float dmg)
    {
        helath -= dmg;
        healthbar.UpdateHealthBar(helath, maxHealth);
    }
}
