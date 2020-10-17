using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Customer : MonoBehaviour
{
    //~~~Public Fields~~~
    public float speed = 0.003f;
    public Grid grid;

    //~~~Private Fields~~~
    private bool activePlaying = false;
    private Vector3 pos;

    // Carson Fields
    private int curX = 0;
    private int curY = 9;
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
    }

    // Update is called once per frame
    void Update()
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
            move();
            
        }
    }
    // Carson Code
    // Move WASD in grid
    void move()
    {
        float tileDistance = .16f;
        // UP
        if (Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("Sit", false);
            animator.SetBool("Walk", true);

            // Check if move = possible
            if (moveCheck(pos.y + tileDistance, 1))
            {
                pos.y += tileDistance;
                this.transform.position = pos;

                // Unfill, Move, Fill
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = true;
                curY--;
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = false;
            }
        }
        //LEFT
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetBool("Sit", false);
            animator.SetBool("Walk", true);

            // Check if move = possible
            if (moveCheck(pos.x - tileDistance, 2))
            {
                pos.x -= tileDistance;
                this.transform.position = pos;

                // Unfill, Move, Fill
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = true;
                curX--;
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = false;
            }
        }
        // DOWN
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("Sit", false);
            animator.SetBool("Walk", true);

            // Check if move = possible
            if (moveCheck(pos.y - tileDistance, 3))
            {
                pos.y -= tileDistance;
                this.transform.position = pos;

                // Unfill, Move, Fill
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = true;
                curY++;
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = false;
            }
        }
        // RIGHT
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("Sit", false);
            animator.SetBool("Walk", true);

            // Check if move = possible
            if (moveCheck(pos.x + tileDistance, 4))
            {
                pos.x += tileDistance;
                this.transform.position = pos;

                // Unfill, Move, Fill
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = true;
                curX++;
                grid.ArrayGrid[curX, curY].GetComponent<Square>().isEmpty = false;
            }
        }

        // Sit
        if (Input.GetKeyDown(KeyCode.O))
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Sit", true);
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
                if (destination <= 0.64f)
                {
                    // Occupied
                    if (grid.ArrayGrid[curX, curY - 1].GetComponent<Square>().isEmpty == true)
                        return true;
                }      
                break;

            // LEFT
            case 2:
                // Boundary
                if (destination >= -1.2f)
                {
                    // Occupied
                    if (grid.ArrayGrid[curX - 1, curY].GetComponent<Square>().isEmpty == true)
                        return true;
                }
                break;

            // DOWN
            case 3:
                // Boundary
                if (destination >= -0.8f)
                {
                    // Occupied
                    if (grid.ArrayGrid[curX, curY + 1].GetComponent<Square>().isEmpty == true)
                        return true;
                }
                break;

            // RIGHT
            case 4:
                // Boundary
                if (destination <= 1.04f)
                {
                    // Occupied
                    if (grid.ArrayGrid[curX + 1, curY].GetComponent<Square>().isEmpty == true)
                        return true;
                }
                break;
        }

        return false;
    }
}
