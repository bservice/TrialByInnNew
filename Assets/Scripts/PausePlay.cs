using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePlay : MonoBehaviour
{
    private bool clicked;
    private bool clicked2;

    Vector2 cursorPosition;

    private AudioSource soundEffect;
    public AudioClip button;

    private float timeLeft;

    public bool Clicked
    {
        get { return clicked; }
        set
        {
            clicked = value;
        }
    }

    public bool Clicked2
    {
        get { return clicked2; }
        set
        {
            clicked2 = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
        clicked2 = false;
        soundEffect = GetComponent<AudioSource>();
        timeLeft = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(clicked2)
        {
            clicked = true;
            clicked2 = false;            
        }

        //Grab vector2 for cursor to use in AABB math
        cursorPosition = Input.mousePosition;
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

        //Selection for objects
        if (Input.GetMouseButtonDown(0))
        {
            //AABB collision test for cursor
            if (cursorPosition.x < this.GetComponent<BoxCollider2D>().bounds.max.x && cursorPosition.x > this.GetComponent<BoxCollider2D>().bounds.min.x)
            {
                //Potential collision!
                //Check the next condition in a nested if statement, just to not have a ton of &'s and to be more efficient
                if (cursorPosition.y > this.GetComponent<BoxCollider2D>().bounds.min.y && cursorPosition.y < this.GetComponent<BoxCollider2D>().bounds.max.y)
                {
                    //Collision!
                    clicked2 = true;
                }
            }
        }
    }
}
