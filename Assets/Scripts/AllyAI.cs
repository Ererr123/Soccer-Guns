using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

public class AllyAI : MonoBehaviour
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
    [SerializeField] float helath, maxHealth = 100;
    [SerializeField] EnemyHealthScript healthbar;
    public GameObject WE;
    public Animator animator;
    public List<GameObject> enList = new List<GameObject>();
    private float timeShot;
    private Controller re;
    private Transform target;
    private float currentDistance;
    //private Transform closestEnemy
    //Temporarly for now they target just player however, will be changed when we add other guys
    // Start is called before the first frame update

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        re = WE.GetComponent<Controller>();
        enList = re.EnemyList;
        currentDistance = 1000;
        FindTarget();
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("EnemyBullet")))
        {
            PlayerTakeDmg(20);
        }
        if(helath <= 0)
        {
            UnityEngine.Object.Destroy(this.gameObject);
        }
    }
    void Update()
    {
        enList = re.EnemyList;
        FindTarget();
        if (timeShot == 0)
        {
            if (target != null)
            {
                if (Vector3.Distance(target.position, transform.position) < 15)
                {
                    animator.SetLayerWeight(1, 1);
                    agent.SetDestination(transform.position);
                    if (target != null)
                        transform.LookAt(target.position);
                    ShootGun();
                    timeShot = Time.time;
                }
                else
                {
                    if (target != null)
                        agent.SetDestination(target.position);
                }
            }
        }

        if (Time.time - timeShot > 1.5)
        {
            animator.SetLayerWeight(1, 0);
            if (target != null)
                agent.SetDestination(target.position);
            timeShot = 0;
        }
    }

    private void ShootGun()
    {
        RaycastHit hit;
        GameObject bullet = GameObject.Instantiate(bulletPrefab, attackPosition.position, Quaternion.identity, bulletparent);
        AllyBulletController bulletController = bullet.GetComponent<AllyBulletController>();
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

    void FindTarget()
    {

        float lowestDist = Mathf.Infinity;


        for (int i = 0; i < enList.Count; i++)
        {

            float dist = Vector3.Distance(enList[i].transform.position, transform.position);

            if (dist < lowestDist)
            {
                lowestDist = dist;
                target = enList[i].transform;
            }

        }
    }
}

