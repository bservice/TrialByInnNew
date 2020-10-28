using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIDisplay : MonoBehaviour
{

    private int patronsLeft;
    private int patronsSat;
    private int score;

    private string scene;

    private float xPos;
    private float yPos;

    private GUIStyle style;

    public int PatronsLeft
    {
        get { return patronsLeft; }
        set
        {
            patronsLeft = value;
        }
    }
    public int PatronsSat
    {
        get { return patronsSat; }
        set
        {
            patronsSat = value;
        }
    }

    public int Score
    {
        get { return score; }
        set
        {
            score = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;        
        DontDestroyOnLoad(this);
        patronsSat = 0;
        score = 1200;
        xPos = Camera.main.WorldToScreenPoint(gameObject.transform.position).x;
        yPos = Screen.height - Camera.main.WorldToScreenPoint(gameObject.transform.position).y;
        style = new GUIStyle();
        style.fontSize = 25;
        style.normal.textColor = Color.white;
    }

    // Update is called once per frame
    void Update()
    {
        scene = SceneManager.GetActiveScene().name;

        if(scene != "CarsonTests" && scene != "LucasTests" && scene != "Grid")
        {
            Destroy(this);
            Destroy(this.gameObject);
        }

        xPos = Camera.main.WorldToScreenPoint(gameObject.transform.position).x;
        yPos = Screen.height - Camera.main.WorldToScreenPoint(gameObject.transform.position).y;
    }

    private void OnGUI()
    {
        //Patrons left
        GUI.Label(new Rect(xPos + 100.0f, yPos - 63.0f, 22, 19), patronsLeft.ToString(), style);

        //Patrons sat
        GUI.Label(new Rect(xPos + 100.0f, yPos - 9.0f, 22, 19), patronsSat.ToString(), style);

        //Score
        GUI.Label(new Rect(xPos + 90.0f, yPos + 42.0f, 22, 19), score.ToString(), style);
    }
}
