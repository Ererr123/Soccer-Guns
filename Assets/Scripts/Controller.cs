using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Cinemachine;

public class Controller : MonoBehaviour
{
    public static Controller gameManger { get; private set; }

    public Health _playerHealth = new Health(100, 100);

    Slider sliderPowerBar;
    GameObject powerBar;
    public static Controller Instance;
    private Ball scriptBall;
    private Player playerLastTouchedBall;
    private Player playerWithBall;
    private Player activeHumanPlayer;
    private Player passDestinationPlayer;
    private int teamWithBall;
    private int teamLastTouched;
    private int teamKickOff;
    private bool waitingForKickOff;
    private float waitingTimeKickOff;
    private float goalTextColorAlpha;
    public List<GameObject> EnemyList = new List<GameObject>();
    // Start is called before the first frame update
    public int count;
    int num;
    public Transform Player;
    public Transform Ball;
    public GameObject Goal_;
    public GameObject Runner;
    public GameObject Shooter;
    public GameObject Goalie;
    public GameObject Defender;
    public GameObject Scorer;
    public Transform BulletParent;
    float time;
    GameObject runner;
    GameObject shooter;
    GameObject goalie;
    GameObject defender;
    ScorerAI scorer;
    [SerializeField]
    public Camera cam;
    public void Awake()
    {
        count = 0;
        num = 4;
        time = Time.time;
        EnemyHealthScript rhealth = Runner.transform.GetChild(1).GetChild(0).GetComponent<EnemyHealthScript>();
        rhealth.cam = cam;
        RunnerAI run = Runner.GetComponent<RunnerAI>();
        run.Player = Player;
        run.Enemy = Player;
        runner = Runner;

        EnemyHealthScript shealth = Shooter.transform.GetChild(1).GetChild(0).GetComponent<EnemyHealthScript>();
        shealth.cam = cam;
        ShooterAi shoot = Shooter.GetComponent<ShooterAi>();
        shoot.player = Player;
        shoot.bulletparent = BulletParent;
        shooter = Shooter;

        EnemyHealthScript ghealth = Goalie.transform.GetChild(1).GetChild(0).GetComponent<EnemyHealthScript>();
        ghealth.cam = cam;
        GoalieController goal = Goalie.GetComponent<GoalieController>();
        goal.ball = Ball;
        goalie = Goalie;

        EnemyHealthScript dhealth = Defender.transform.GetChild(1).GetChild(0).GetComponent<EnemyHealthScript>();
        dhealth.cam = cam;
        DefenderAI defend = Defender.GetComponent<DefenderAI>();
        defend.goalie = Goal_;
        defend.ball = Ball;
        defender = Defender;

        scorer = Scorer.GetComponent<ScorerAI>();
        /*EnemyHealthScript schealth = Scorer.transform.GetChild(2).GetChild(0).GetComponent<EnemyHealthScript>();
        schealth.cam = cam;
        ScorerAI score = Scorer.GetComponent<ScorerAI>();
        score.ball = Ball;
        score.goal = Goal_;
        scorer = Scorer;*/


        if (gameManger != null && gameManger != this)
        {
            Destroy(this);
        }
        else
        {
            gameManger = this;
            Instance = this;
            sliderPowerBar = GameObject.Find("Canvas/Slider").GetComponent<Slider>();
            powerBar = GameObject.Find("Canvas/Slider");
            powerBar.SetActive(false);
        }

        EnemyList.Add(Instantiate(runner, new Vector3(0, -9.759598f, 22.27f), Quaternion.identity));
        EnemyList.Add(Instantiate(shooter, new Vector3(3, -9.759598f, 22.27f), Quaternion.identity));
        EnemyList.Add(Instantiate(goalie, new Vector3(0.919f, -9.7f, 25.17f), new Quaternion(0, -180, 0, 1)));
        EnemyList.Add(Instantiate(defender, new Vector3(.41F, -9.7f, 23.409f), new Quaternion(0, -180, 0, 1)));



    }

    private void Update()
    {
        for (int i = 0; i < EnemyList.Count; i++)
        {
            cap health = EnemyList[i].GetComponent<cap>();
            if (health.helath <= 0)
            {
                Destroy(EnemyList[i]);
                EnemyList.RemoveAt(i);
                StartCoroutine(Spawn(health.type));
            }
        }

        if(scorer.helath <= 0)
        {
            if (scorer.ballAttachedToEnemy == true)
            {
                scorer.BallAttachedToEnemy.StickToPlayer = false;
                scorer.ballAttachedToEnemy = null;
                Scorer.SetActive(false);
            }
            else
            {
                Scorer.SetActive(false);
            }
           
            
            scorer.helath = 100;
            StartCoroutine(Spawn(8));
        }

    }
    public void SetPowerBar(float value)
    {
        powerBar.SetActive(true);
        sliderPowerBar.value = value;
    }

    public void RemovePowerBar()
    {
        powerBar.SetActive(false);
        sliderPowerBar.value = 0;
    }

    IEnumerator Spawn(int y)
    {
        yield return new WaitForSeconds(15);
        if (y == 0)
        {
            EnemyList.Add(Instantiate(runner, new Vector3(0, -9.759598f, 22.27f), Quaternion.identity));
        }
        else if (y == 1)
        {
            EnemyList.Add(Instantiate(shooter, new Vector3(3, -9.759598f, 22.27f), Quaternion.identity));
        }
        else if (y == 2)
        {
            EnemyList.Add(Instantiate(goalie, new Vector3(0.919f, -9.7f, 25.17f), new Quaternion(0, -180, 0, 1)));
        }
        else if (y == 3)
        {
            EnemyList.Add(Instantiate(defender, new Vector3(.41F, -9.7f, 22.409f), new Quaternion(0, -180, 0, 1)));
        }
        else
        {
            Scorer.SetActive(true);
            EnemyHealthScript gh = Scorer.transform.GetChild(0).GetChild(0).GetComponent<EnemyHealthScript>();
            gh.UpdateHealthBar(scorer.helath, scorer.maxHealth);
        }
    }

}
