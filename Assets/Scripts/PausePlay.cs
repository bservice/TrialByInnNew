using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePlay : MonoBehaviour
{
    private bool clicked;
    //private float timeLeft;

    public bool Clicked
    {
        get { return clicked; }
        set
        {
            clicked = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //timeLeft = 5.1f;
        //gameObject.GetComponent<SpriteRenderer>().color = new Color(210f / 255f, 198f / 255f, 140f / 255f);
        clicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (clicked)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(210f / 255f, 198f / 255f, 140f / 255f);
            timeLeft -= Time.deltaTime;
            Debug.Log(timeLeft);
            if (timeLeft < 0)
            {
                
                timeLeft = 1.1f;
                clicked = false;
            }
        }*/
    }

    void OnMouseDown()
    {        
        clicked = true;   
    }
}
