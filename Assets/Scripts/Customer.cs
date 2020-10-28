using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;

public class Customer : MonoBehaviour
{
    //~~~Public Fields~~~
    public float speed = 0.003f;    
    public AudioClip step;

    //~~~Private Fields~~~
    private bool activePlaying = false;
    private Vector3 pos;
    private PauseTest pauseMenu;
    private Onboarding onboard;
    private AudioSource soundEffect;
    private Grid grid;
    private LevelManager levelManager;
    private GameObject moveableManager;



    // Carson Fields
    private float tileDistance = .16f;
    public int curX = 0;
    public int curY = 9;
    private bool placed = false;
    private bool[] dir = { false, false, false, false }; // 0 = Up, 1 = Left, 2 = Down, 3 = Right

    public Animator animator;
    private GameUIDisplay scoreboard;

    //Seating Fields
    private int numberOfTables; //Stores how many tables are in the level
    bool inSeat;
    bool isSeated;
    GameObject seat;

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
        onboard = FindObjectOfType<Onboarding>();
        soundEffect = GetComponent<AudioSource>();
        scoreboard = FindObjectOfType<GameUIDisplay>();
        grid = FindObjectOfType<Grid>();
        levelManager = FindObjectOfType<LevelManager>();
        moveableManager = GameObject.Find("MoveableManager");
        // Carson Code
        // Call move every .3s
        InvokeRepeating("move", 0.0f, 0.15f); //This is in start but will run every .3s


        //Seating initialization
        numberOfTables = moveableManager.GetComponent<MoveableManager>().tables.Count; //Set the number of tables in the level
        inSeat = false;
        seat = null;
    }

    // Update is called once per frame
    void Update()
    {
        //Set this x and y to its moveable object script x and y
        if(!isSeated)
        {
            this.GetComponent<MoveableObject>().xPosition = curX;
            this.GetComponent<MoveableObject>().yPosition = 9-curY;
        }
    


        //Display the move instructions
        onboard.SetBubbleActive(2, onboard.MoveInstruct);

        if (!pauseMenu.Paused)
        {
            pos = this.transform.position;
            //If the current customer activate the movement
            if (activePlaying)
            {
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

        if (inSeat)
        {
            if (!isSeated)
            {
                if (Input.GetKeyDown(KeyCode.P))
                {
                    //Set the xPos and yPos in moveable object so their positions are lined up
                    this.GetComponent<MoveableObject>().xPosition = curX;
                    this.GetComponent<MoveableObject>().yPosition = 9 - curY;
               
                    isSeated = true;
                    //Grab the number of the collider
                    string snumToAssociate = seat.gameObject.name.Remove(0, 13); //Trim the rest of the name so you just have the number (number assigned in TableScript)
                    int numToAssociate = int.Parse(snumToAssociate);
                    for (int i = 0; i < numberOfTables; i++)
                    //For each table in the list, check their number. Become an associated object when the numbers match.
                    {
                        //Pull the number of the table 
                        int tableNum = moveableManager.GetComponent<MoveableManager>().tables[i].GetComponent<TableScript>().tableNumber;
                        //Check if the number is the same as the associated 
                        if (numToAssociate == tableNum)
                        {
                            //If they are, add this customer to the table's moveable object list.
                            moveableManager.GetComponent<MoveableManager>().tables[i].GetComponent<MoveableObject>().associatedObjects.Add(this.GetComponent<MoveableObject>());

                            //Disable selection + movement for this customer
                            //This seems to happen already!
                        }
                    }

                    //call the code in Level Manager that makes them sit down
                    levelManager.CustomerCollision();
                }
            }
        }

    }
    // Carson Code
    // Move WASD in grid
    void move()
    {
        if(!isSeated)
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

                    // -10 per move
                    scoreboard.Score -= 10;

                    //Play sound every move
                    soundEffect.PlayOneShot(step);


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

                    // -10 per move
                    scoreboard.Score -= 10;

                    //Play sound every move
                    soundEffect.PlayOneShot(step);
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

                    // -10 per move
                    scoreboard.Score -= 10;

                    //Play sound every move
                    soundEffect.PlayOneShot(step);
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

                    // -10 per move
                    scoreboard.Score -= 10;

                    //Play sound every move
                    soundEffect.PlayOneShot(step);
                }
            }
            else
            {
                // Idle Anim
                animator.SetBool("Walk", false);
            }

            //Update moveable object coords
            this.GetComponent<MoveableObject>().xPosition = curX;
            this.GetComponent<MoveableObject>().yPosition = 9 - curY;
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
        {
            dir[0] = true;
            //Turn off the move instructions once the player hits W or D
            //Then set the table bool to true to be able to be displayed back in the level manager class
            onboard.MoveInstruct = false;
            onboard.TableInstruct = true;
        }
        // Left
        if (Input.GetKeyDown(KeyCode.A))
            dir[1] = true;
        // Down
        if (Input.GetKeyDown(KeyCode.S))
            dir[2] = true;
        // Right
        if (Input.GetKeyDown(KeyCode.D))
        {
            dir[3] = true;
            onboard.MoveInstruct = false;
            onboard.TableInstruct = true;
        }
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

    private void OnCollisionStay2D(Collision2D collision)
    //Customer collision
    {

        if (collision.gameObject.tag == "Seat")
        {
            //Set true used in Update()
            seat = collision.gameObject;
            inSeat = true;
            if(!isSeated)
            {
              this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);

            }
        }
            
            //Make it so that they can't collide with other customers/tables
            //Insert code here
        }

    //When customers leave a collision
    private void OnCollisionExit2D(Collision2D collision)
    {
       this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
        inSeat = false;
        collision = null;
    }
}
