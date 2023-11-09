using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalieController : MonoBehaviour
{
    private Animator animator;
    public Transform ball;
    private int MinDist = 6;
    private float timeDive;
    private void Start()
    {
        animator = GetComponent<Animator>();
           
    }

    private void Update()
    {
        // Randomly decide the dive direction and set the corresponding boolean.

        // Set the boolean parameters in the Animator.
        if (timeDive <= 0)
        {
            if (Vector3.Distance(ball.position, transform.position) <= MinDist)
            {
                if (ball.position.x > 0.86)
                {
                    timeDive = Time.time;
                    animator.Play("diveRight", 1, 0f);
                    animator.SetLayerWeight(1, 1f);
                }
                else
                {
                    timeDive = Time.time;
                    animator.Play("diveLeft", 2, 0f);
                    animator.SetLayerWeight(2, 2f);
                }
            }
        }
        if (Time.time - timeDive >= 3.4)
        {
            if (ball.position.x < 0.86)
            {
                animator.SetLayerWeight(2, Mathf.Lerp(animator.GetLayerWeight(2), 0f, Time.deltaTime * 10f));
                timeDive = 0;
                transform.position = new Vector3(0.919f, -9.608f, 25.17f);
            }
            else
            {
                animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 10f));
                timeDive = 0;
                transform.position = new Vector3(0.919f, -9.608f, 25.17f);
            }
        }
    }
}





