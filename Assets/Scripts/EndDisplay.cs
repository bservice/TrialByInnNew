using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDisplay : MonoBehaviour
{
    public GameObject displayBoard;

    private GameUIDisplay scoreboard;

    private GUIStyle style;

    private int score;
    private int patronsLeft;
    private int patronsSat;

    private float xPos;
    private float yPos;

    // Start is called before the first frame update
    void Start()
    {
        scoreboard = FindObjectOfType<GameUIDisplay>();
        score = scoreboard.Score;
        patronsLeft = scoreboard.PatronsLeft;
        patronsSat = scoreboard.PatronsSat;
        Destroy(scoreboard);
        Destroy(scoreboard.gameObject);
        xPos = Camera.main.WorldToScreenPoint(displayBoard.gameObject.transform.position).x;
        yPos = Screen.height - Camera.main.WorldToScreenPoint(displayBoard.gameObject.transform.position).y;
        style = new GUIStyle();
        style.fontSize = 28;
        style.normal.textColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        xPos = Camera.main.WorldToScreenPoint(displayBoard.gameObject.transform.position).x;
        yPos = Screen.height - Camera.main.WorldToScreenPoint(displayBoard.gameObject.transform.position).y;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(xPos + 115.0f, yPos + 95.0f, 22, 19), score.ToString(), style);
    }
}
