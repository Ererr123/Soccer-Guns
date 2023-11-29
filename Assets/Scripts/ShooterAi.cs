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
    public Transform bulletparent;
    public Transform rayPosition;
    public LayerMask IgnoreMe;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField]
    public Transform player;
    //[SerializeField] float helath, maxHealth = 100;
    //[SerializeField] EnemyHealthScript healthbar;
    public Animator animator;
    private float timeShot;
    //private Transform closestEnemy
    //Temporarly for now they target just player however, will be changed when we add other guys
    // Start is called before the first frame update

    private void Awake()
    {
        //healthbar = GetComponentInChildren<EnemyHealthScript>();
        animator = GetComponent<Animator>();
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("AllyBullet")))
        {
            PlayerTakeDmg(20);
        }
    }*/
    // Update is called once per frame
    void Update()
    {
        if (timeShot == 0)
        {
            if (Vector3.Distance(player.position, transform.position) < 15)
            {
                animator.SetLayerWeight(1, 1);
                agent.SetDestination(transform.position);
                transform.LookAt(player.position);
                ShootGun();
                timeShot = Time.time;
            }
            else
            {
                agent.SetDestination(player.position);
            }
        }

        if (Time.time - timeShot > 1.5)
        {
            animator.SetLayerWeight(1, 0);
            agent.SetDestination(player.position);
            timeShot = 0;
        }
    }

    private void ShootGun()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, attackPosition.position, Quaternion.identity, bulletparent);
        EnemyBulletController bulletController = bullet.GetComponent<EnemyBulletController>();
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
    /*private void PlayerTakeDmg(float dmg)
    {
        helath -= dmg;
        healthbar.UpdateHealthBar(helath, maxHealth);
    }*/
}

