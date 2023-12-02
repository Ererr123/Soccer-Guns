using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadSceneAsync("Level2");
    }
        
}
