using System.Collections;
using System.Collections.Generic;
using Keto;
using UnityEngine;
using UnityEngine.UI;

public class Lose : MonoBehaviour
{
    public Text pointsText;
    public void Setup(int score) 
    {
        gameObject.SetActive(true);
        pointsText.text = "You Scored: " + score.ToString() + " points";
    }
    
    
}
