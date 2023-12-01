using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerAI : MonoBehaviour
{

    public Transform Enemy;
    public Transform Player;
    private Transform ball;
    [SerializeField] float MoveSpeed;
    int MaxDist = 10;
    int MinDist = 3;
    bool controller;
    private float timeSlide;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = false;
        ball = GameObject.Find("Soccer Ball").transform;
        //Player = 
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("AllyBullet")))
        {
            PlayerTakeDmg(20);
        }
    }*/
    void Update()
    {
        if (Time.time - timeSlide > 1.23)
        {
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            controller = false;
        }

        if (Vector3.Distance(transform.position, Enemy.position) >= MinDist && controller == false)
        {
            //transform.LookAt(Player);
            Quaternion targetRotation = Quaternion.LookRotation(Enemy.position - transform.position);
            targetRotation.z = 0;
            transform.rotation = targetRotation;
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        }
        if (Vector3.Distance(transform.position, Enemy.position) < MinDist && controller == false)
        {
            controller = true;
            timeSlide = Time.time;
            animator.Play("Slide", 1, 0f);
            animator.SetLayerWeight(1, 1f);
        }
    }

}