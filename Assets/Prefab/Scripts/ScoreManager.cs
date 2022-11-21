using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
 public static int scoreValue = 0;

    Text score;

    private void Start()
    {
        score = GetComponent<Text>();
    }
    private void Update()
    {
        score.text = "TurtleGreen: " + scoreValue;
    }
}
