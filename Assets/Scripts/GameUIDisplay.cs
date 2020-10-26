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
        patronsLeft = 10;
        patronsSat = 0;
        score = 500;
        xPos = Camera.main.WorldToScreenPoint(gameObject.transform.position).x;
        yPos = Screen.height - Camera.main.WorldToScreenPoint(gameObject.transform.position).y;
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
        GUI.Label(new Rect(xPos + 55.0f, yPos - 39.0f, 22, 19), patronsLeft.ToString());

        //Patrons sat
        GUI.Label(new Rect(xPos + 55.0f, yPos - 9.0f, 22, 19), patronsSat.ToString());

        //Score
        GUI.Label(new Rect(xPos + 55.0f, yPos + 19.0f, 22, 19), score.ToString());
    }
}
