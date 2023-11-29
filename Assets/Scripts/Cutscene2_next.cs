using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutScene2_next : MonoBehaviour
{
    public void NextScene()
    {
        SceneManager.LoadSceneAsync("Cutscene3");
    }
        
}
