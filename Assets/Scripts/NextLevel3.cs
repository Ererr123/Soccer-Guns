using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel3 : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadSceneAsync("Level4");
    }
        
}
