using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDisplay : MonoBehaviour
{
    private GameUIDisplay scoreboard;

    private int score;
    private int patronsLeft;
    private int patronsSat;

    // Start is called before the first frame update
    void Start()
    {
        scoreboard = FindObjectOfType<GameUIDisplay>();
        score = scoreboard.Score;
        patronsLeft = scoreboard.PatronsLeft;
        patronsSat = scoreboard.PatronsSat;
        Destroy(scoreboard);
        Destroy(scoreboard.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(100.0f, 39.0f, 22, 19), score.ToString());
        GUI.Label(new Rect(100.0f, 59.0f, 22, 19), patronsLeft.ToString());
        GUI.Label(new Rect(100.0f, 79.0f, 22, 19), patronsSat.ToString());
    }
}
