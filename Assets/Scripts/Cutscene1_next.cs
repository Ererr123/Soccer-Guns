using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene1_next : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadSceneAsync("Cutscene2");
    }
        
}
