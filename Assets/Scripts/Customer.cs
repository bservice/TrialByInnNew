using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class Customer : MonoBehaviour
{
    //~~~Public Fields~~~
    public float speed = 0.003f;
    public Grid grid;

    //~~~Private Fields~~~
    private bool activePlaying = false;
    private Vector3 pos;
    private PauseTest pauseMenu;

    // Carson Fields
    private float tileDistance = .16f;
    private int curX = 0;
    private int curY = 9;
    private bool placed = false;
    private bool[] dir = { false, false, false, false }; // 0 = Up, 1 = Left, 2 = Down, 3 = Right

    public Animator animator;


    //~~~Properties~~~
    public bool ActivePlaying
    {
        get
        {
            return activePlaying;
        }
        set
        {
            activePlaying = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pos = this.transform.position;
        pauseMenu = FindObjectOfType<PauseTest>();

        // Casron Code
        // Call move every .3s
        InvokeRepeating("move", 0.0f, 0.3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.Paused)
        {
            pos = this.transform.position;
            //If the current customer activate the movement
            if (activePlaying)
            {
                /* Lucas Code
                if (Input.GetKey(KeyCode.W))
                {
                    pos.y += speed;
                    this.transform.position = pos;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    pos.x -= speed;
                    this.transform.position = pos;
                }
                if (Input.GetKey(KeyCode.S))
                {
                    pos.y -= speed;
                    this.transform.position = pos;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    pos.x += speed;
                    this.transform.position = pos;
                }
                */


                // Carson Code
                // Update what keys are pressed and released
                press();
                depress();
                placed = true;
            }
            // Carson Code
            // Sit once no longer active and have moved
            else if (placed)
            {
                animator.SetBool("Walk", false);
                animator.SetBool("Sit", true);
            }
        }
    }
    // Carson Code
    // Move WASD in grid
    void move()
    {
        // UP
        if (dir[0])
        {
            // Check if move = possible
            if (moveCheck(pos.y + tileDistance, 1))
            {
                // Walk Anim
                animator.SetBool("Walk", true);

                // Move
                pos.y += tileDistance;
                this.transform.position = pos;

                // Unfill, Move, Fill
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = true;
                curY--;
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = false;
            }
        }
        //LEFT
        else if (dir[1])
        {
            // Check if move = possible
            if (moveCheck(pos.x - tileDistance, 2))
            {
                // Walk Anim
                animator.SetBool("Walk", true);

                // Move
                pos.x -= tileDistance;
                this.transform.position = pos;

                // Unfill, Move, Fill
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = true;
                curX--;
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = false;
            }
        }
        // DOWN
        else if (dir[2])
        {
            // Check if move = possible
            if (moveCheck(pos.y - tileDistance, 3))
            {
                // Walk Anim
                animator.SetBool("Walk", true);

                // Move
                pos.y -= tileDistance;
                this.transform.position = pos;

                // Unfill, Move, Fill
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = true;
                curY++;
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = false;
            }
        }
        // RIGHT
        else if (dir[3])
        {
            // Check if move = possible
            if (moveCheck(pos.x + tileDistance, 4))
            {
                // Walk Anim
                animator.SetBool("Walk", true);

                // Move
                pos.x += tileDistance;
                this.transform.position = pos;

                // Unfill, Move, Fill
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = true;
                curX++;
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = false;
            }
        }
        else
        {
            // Idle Anim
            animator.SetBool("Walk", false);
        }
    }


    // Check if move = possible, destination = where to potentially move, direction: 1= Up, 2 = Left, 3 = Down, 4 = Right
    bool moveCheck(float destination, int direction)
    {
        switch(direction)
        {
            // UP
            case 1:
                // Boundary
                if (curY - 1 > -1)
                {
                    // Occupied
                    if (grid.ArrayGrid[curX, curY - 1].GetComponent<Square>().isEmpty == true)
                        return true;
                }      
                break;

            // LEFT
            case 2:
                // Boundary
                if (curX - 1 > -1)
                {
                    // Occupied
                    if (grid.ArrayGrid[curX - 1, curY].GetComponent<Square>().isEmpty == true)
                        return true;
                }
                break;

            // DOWN
            case 3:
                // Boundary
                if (curY + 1 < grid.yAxisLength)
                {
                    // Occupied
                    if (grid.ArrayGrid[curX, curY + 1].GetComponent<Square>().isEmpty == true)
                        return true;
                }
                break;

            // RIGHT
            case 4:
                // Boundary
                if (curX + 1 < grid.xAxisLength)
                {
                    // Occupied
                    if (grid.ArrayGrid[curX + 1, curY].GetComponent<Square>().isEmpty == true)
                        return true;
                }
                break;
        }

        return false;
    }

    // Returns true for button pressed
    void press()
    {
        // Up
        if (Input.GetKeyDown(KeyCode.W))
            dir[0] = true;
        // Left
        if (Input.GetKeyDown(KeyCode.A))
            dir[1] = true;
        // Down
        if (Input.GetKeyDown(KeyCode.S))
            dir[2] = true;
        // Right
        if (Input.GetKeyDown(KeyCode.D))
            dir[3] = true;
    }

    // Returns false for button released
    void depress()
    {
        // Up
        if (Input.GetKeyUp(KeyCode.W))
            dir[0] = false;
        // Left
        if (Input.GetKeyUp(KeyCode.A))
            dir[1] = false;
        // Down
        if (Input.GetKeyUp(KeyCode.S))
            dir[2] = false;
        // Right
        if (Input.GetKeyUp(KeyCode.D))
            dir[3] = false;
    }

}
