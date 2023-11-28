using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class ShooterAi : MonoBehaviour
{

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform attackPosition;
    [SerializeField]
    private Transform bulletparent;
    public Transform rayPosition;
    public LayerMask IgnoreMe;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField]
    private Transform player;
    [SerializeField] float helath, maxHealth = 100;
    [SerializeField] EnemyHealthScript healthbar;
    public Animator animator;
    private float timeShot;
    //private Transform closestEnemy
    //Temporarly for now they target just player however, will be changed when we add other guys
    // Start is called before the first frame update

    private void Awake()
    {
        healthbar = GetComponentInChildren<EnemyHealthScript>();
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        agent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Bullet")))
        {
            PlayerTakeDmg(20);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //closestEnemy = Closest(team);
        if (timeShot == 0)
        {
            if (Vector3.Distance(player.position, transform.position) < 15)
            {
                //agent.SetDestination(transform.position);
                //transform.LookAt(player.position);
                ShootGun();
                timeShot = Time.time;
            }
        }

        if (Time.time - timeShot > 1.5)
        {
            timeShot = 0;
        }
        /*if (Vector3.Distance(player.position, transform.position) > 15)
        {
            agent.SetDestination(player.position);
        }*/
    }

    private void ShootGun()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, attackPosition.position, Quaternion.identity, bulletparent);
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (Physics.Raycast(rayPosition.position, rayPosition.forward, out hit, 300f))
        {
            bulletController.target = hit.point;
            bulletController.hit = true;
        }
        else
        {
            bulletController.target = rayPosition.position + rayPosition.forward * 25f;
            bulletController.hit = false;
        }
    }
    private void PlayerTakeDmg(float dmg)
    {
        helath -= dmg;
        healthbar.UpdateHealthBar(helath, maxHealth);
    }
}
