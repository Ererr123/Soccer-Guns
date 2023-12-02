using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel4 : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadSceneAsync("Level5");
    }
        
}
