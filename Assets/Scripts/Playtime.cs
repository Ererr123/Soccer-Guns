using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Playtime : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadSceneAsync("Level1");
    }
        
}
