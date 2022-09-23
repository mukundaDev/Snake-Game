using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Awake()
    {
       // transform.Find("RetryButton").GetComponent<Button_UI>().ClickFunc = ()>{
            SceneManager.LoadScene(0);
    }

}
