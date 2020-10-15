using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIDisplay : MonoBehaviour
{

    private int patronsLeft;
    private int patronsSat;
    private int score;

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
        patronsLeft = 10;
        patronsSat = 0;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        //Patrons left
        GUI.Label(new Rect(120, 28, 22, 19), patronsLeft.ToString());

        //Patrons sat
        GUI.Label(new Rect(120, 58, 22, 19), patronsSat.ToString());

        //Score
        GUI.Label(new Rect(120, 92, 22, 19), score.ToString());
    }
}
