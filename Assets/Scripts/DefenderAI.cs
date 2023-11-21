using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DefenderAI : MonoBehaviour
{

    public Transform ball;
    private Transform Player;
    //private Vector3 targetGoalPosition;
    [SerializeField] float MoveSpeed;
    //[SerializeField] private float movementSpeed = 4.0f;
    //[SerializeField] private float shootingPower = 0.7f;
    //private Transform playerBallPosition;
    int MaxDist = 10;
    int MinDist = 3;
    bool controller;
    private float timeSlide;
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        controller = false;
        ball = GameObject.Find("Soccer Ball").transform;
        //Player = 
    }

    void Update()
    {
        if (Time.time - timeSlide > 1.23)
        {
            animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
            controller = false;
        }

        if (Vector3.Distance(transform.position, ball.position) >= MinDist && controller == false)
        {
            //transform.LookAt(Player);
            Quaternion targetRotation = Quaternion.LookRotation(ball.position - transform.position);
            targetRotation.z = 0;
            transform.rotation = targetRotation;
            transform.position += transform.forward * MoveSpeed * Time.deltaTime;



            if (Vector3.Distance(transform.position, ball.position) <= MaxDist)
            {
                //Here Call any function U want Like Shoot at here or something
            }

        }
        if(Vector3.Distance(transform.position,ball.position) < MinDist && controller == false)
        {
            controller = true;
            timeSlide = Time.time;
            animator.Play("Slide", 1, 0f);
            animator.SetLayerWeight(1, 1f);
        }
    }
}
