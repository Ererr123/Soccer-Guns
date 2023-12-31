using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private Player scriptPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Ball"))
        {
            if (name.Equals("PlayerGoalDetector"))
            {
                scriptPlayer.IncreaseMyScore();
            }
            else
            {
                scriptPlayer.IncreaseOtherScore();
            }
        }
    }
}
