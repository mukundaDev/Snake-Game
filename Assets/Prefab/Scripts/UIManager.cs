using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
   [SerializeField] GameObject gameOverScreen;


  
    public void EnableGameOverMenu()
    {
        gameOverScreen.SetActive(true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(1);
    }
    public void QuitButton()
    {
        SceneManager.LoadScene(0);
    }

}
