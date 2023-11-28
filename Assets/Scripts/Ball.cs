using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool stickToPlayer;
    [SerializeField] private Transform transformPlayer;
    [SerializeField] private Transform transformEnemy;
    private Transform playerBallPosition;
    private Transform enemyBallPosition;
    float speed;
    Vector3 previousLocation;
    Player scriptPlayer;
    ScorerAI scriptScorer;

    public bool StickToPlayer { get => stickToPlayer; set => stickToPlayer = value; }

    // Start is called before the first frame update
    void Start()
    {
        playerBallPosition = transformPlayer.Find("BallLocation");
        scriptPlayer = transformPlayer.GetComponent<Player>();

        enemyBallPosition = transformEnemy.Find("BallLocation");
        scriptScorer = transformEnemy.GetComponent<ScorerAI>();
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (!stickToPlayer)
        {
            float distanceToPlayer = 0;
            float distanceToEnemy = 0;
            speed = 0;
            distanceToPlayer = Mathf.Abs(Vector3.Distance(transformPlayer.position, transform.position));
            distanceToEnemy = Mathf.Abs(Vector3.Distance(transformEnemy.position, transform.position));
            if (distanceToPlayer < 1)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                stickToPlayer = true;
                scriptPlayer.BallAttachedToPlayer = this;
            }
            if (distanceToEnemy < 1)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;
                stickToPlayer = true;
                scriptScorer.BallAttachedToEnemy = this;
            }
        }
        else
        {
            if (scriptPlayer.BallAttachedToPlayer == true)
            {
                Vector2 currentLocation = new Vector2(transform.position.x, transform.position.z);
                speed = Vector2.Distance(currentLocation, previousLocation) / Time.deltaTime;
                transform.position = playerBallPosition.position;
                transform.Rotate(new Vector3(transformPlayer.right.x, 0, transformPlayer.right.z), speed, Space.World);
                previousLocation = currentLocation;
            }
            else
            {
                Vector2 currentLocation = new Vector2(transform.position.x, transform.position.z);
                speed = Vector2.Distance(currentLocation, previousLocation) / Time.deltaTime;
                transform.position = enemyBallPosition.position;
                transform.Rotate(new Vector3(transformEnemy.right.x, 0, transformEnemy.right.z), speed, Space.World);
                previousLocation = currentLocation;
            }
        }
        if (transform.position.z > 27 || transform.position.z < -27 || transform.position.x > 16.78 || transform.position.x < -16.78)
        {
            transform.position = new Vector3(0, -9.608f, 0);
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
            stickToPlayer  = false;
        }


    }
}
