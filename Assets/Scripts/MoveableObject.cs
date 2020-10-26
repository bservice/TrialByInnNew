using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.WSA;

/// <summary>
/// An object that the player can move.
/// Contents:
/// Start
/// Update
/// OnMouseDown: Selects the object when clicked.
/// MoveObject: Physically moves the object along the grid.
/// FreeMoveCheck: Checks if a given tile is okay to move into.
/// Collision Detection: Checks if colliding with another Object (based on tag)
/// </summary>  
public class MoveableObject : MonoBehaviour
{
    #region Fields
    //GameObjects this is dependent on
    public MoveableManager manager;
    public Grid grid;
    //Whether or not this object can be selected and moved
    public bool selectable;
    // The position of the top left corner of the square.
    public Vector2 position;
    // The size of the object in pixels.
    private Vector2 size;
    // Whether or not the object is being lifted.
    public bool isLifted;
    //Whether or not the object is colliding with another object
    public bool isColliding;
    // For animations
    public Animator animator;

    //Audio variables
    private AudioSource soundEffect;
    public AudioClip moveTable;

    public int xPosition; //Holds what x position in the grid the object is on
    public int yPosition; //Holds what y position in the grid the object is on
    /*public int width; //How many tiles wide the object is
    public int height; //How many tiles tall the object is*/
    public List<MoveableObject> associatedObjects; //List full of the friends of the object that move with it
    Vector2 cursorPosition;
    //public Vector2 position;
    #endregion
    #region Properties
    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }
    public Vector2 Size
    {
        get { return size; }
        set { size = value; }
    }
    public bool IsLifted
    {
        get { return isLifted; }
        set { isLifted = value; }
    }
    // Getting the points on the tile
    public Vector2 getTopLeft
    {
        get { return position; }
    }
    public Vector2 getTopRight
    {
        get { return new Vector2(position.x + size.x, position.y); }
    }
    public Vector2 getBottomRight
    {
        get { return new Vector2(position.x + size.x, position.y + size.y); }
    }
    public Vector2 getBottomLeft
    {
        get { return new Vector2(position.x, position.y + size.y); }
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //Get managers/other dependencies
        manager = FindObjectOfType<MoveableManager>(); //Search for manager in the scene
        grid = manager.grid; //Grab grid from manager
        isColliding = false;
        soundEffect = GetComponent<AudioSource>();

        //Mark current tiles taken up as empty
        grid.ArrayGrid[xPosition, 9 - yPosition].GetComponent<Square>().isEmpty = true;
        grid.ArrayGrid[xPosition - 1, 9 - yPosition].GetComponent<Square>().isEmpty = true;
        grid.ArrayGrid[xPosition + 1, 9 - yPosition].GetComponent<Square>().isEmpty = true;
    }

    // Update is called once per frame
    void Update()
    //Checks for objects being selected
    {

        //Puts the object at the world position of their current tile (mostly used to get it in the right place at the start)
        transform.position = grid.ArrayGrid[xPosition, yPosition].GetComponent<Square>().Position;
        //Grab vector2 for cursor to use in AABB math
        cursorPosition = Input.mousePosition;
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

        //Selection for objects
        if(Input.GetMouseButtonDown(0) && selectable)
        {
            //AABB collision test for cursor

            if (cursorPosition.x < this.GetComponent<BoxCollider2D>().bounds.max.x && cursorPosition.x > this.GetComponent<BoxCollider2D>().bounds.min.x)
            {
                //Potential collision!
                //Check the next condition in a nested if statement, just to not have a ton of &'s and to be more efficient
                if (cursorPosition.y > this.GetComponent<BoxCollider2D>().bounds.min.y && cursorPosition.y < this.GetComponent<BoxCollider2D>().bounds.max.y)
                {
                    //Collision!
                    // If the object is in the air...
                    if (isLifted)
                    {
                        //If intersecting another object, don't allow player to place object. Otherwise put object down 
                        if (!isColliding)
                        {
                            // Put it down.
                            isLifted = false;
                            animator.SetBool("AnimTable", false);
                            //Set color of all associated objects to default color
                            this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
                            for (int i = 0; i < associatedObjects.Count; i++)
                            {
                                associatedObjects[i].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.white);
                            }
                            //Unselect the Object. 
                            manager.selectedObject = null;
                        }
                    }
                    // If the object is on the ground...
                    else
                    {
                        //If you don't have another object selected
                        if (manager.selectedObject == null)
                        {
                            // Lift it up.
                            isLifted = true;
                            animator.SetBool("AnimTable", true);
                            //Change color of all associated objects to cyan
                            this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);
                            for (int i = 0; i < associatedObjects.Count; i++)
                            {
                                associatedObjects[i].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);
                            }
                            //Select the object.
                            manager.selectedObject = this;
                        }
                    }
                }

            }

        }
    }

    public void MoveObject(int x, int y)
    //Effect: Moves the object to a new position based on xy grid position
    //Called in: MoveableManager
    {
        //Mark current tiles taken up as empty
        grid.ArrayGrid[xPosition, 9 - yPosition].GetComponent<Square>().isEmpty = true;
        grid.ArrayGrid[xPosition - 1, 9 - yPosition].GetComponent<Square>().isEmpty = true;
        grid.ArrayGrid[xPosition + 1, 9 - yPosition].GetComponent<Square>().isEmpty = true;
        //Adds the amount we are moving by to the position so we know what part of the grid to move to.
        int xToMoveTo = xPosition + x;
        int yToMoveTo = yPosition + y;
        //Sets transform = the world position of that grid tile
        transform.position = grid.ArrayGrid[xToMoveTo, yToMoveTo].GetComponent<Square>().Position;
        //Set the grid position of the object to its new position
        xPosition = xToMoveTo;
        yPosition = yToMoveTo;
        //Mark the squares that are taken up as NOT empty
        grid.ArrayGrid[xPosition, 9 - yPosition].GetComponent<Square>().isEmpty = false;
        grid.ArrayGrid[xPosition - 1, 9 - yPosition].GetComponent<Square>().isEmpty = false;
        grid.ArrayGrid[xPosition + 1, 9 - yPosition].GetComponent<Square>().isEmpty = false;
        //Play table sound
        soundEffect.PlayOneShot(moveTable);
    }

    public bool FreeMoveCheck(int x, int y)
    //Called in: MoveableManager
    //Effect: Checks if the tile to move to is free. Returns true if it is free, returns false if it is not.
    //Arguments: x, y are the amount to move by, not exact coordinates in the grid (those are calculated in this method)
    {
        //Adds the amount we are moving by to the position so we know what part of the grid to move to.
        int xToMove = xPosition + x;
        int yToMove = yPosition + y;
        //Return true if the tile is empty, and if it's not a wall.
        if (((xToMove < grid.gridSize.x) && (xToMove > -1) && (yToMove < grid.gridSize.y) && (yToMove > -1)) /*&& grid.ArrayGrid[xToMove, yToMove].GetComponent<Square>().isEmpty*/)
        {
            return true;
        }
        //Return false if the tile is occupied
        else
        {
            //Double check that it's not one of the attached objects in that tile--if it is, that object will also move so it's technically free.
            if (associatedObjects != null)
            {
                for (int i = 0; i < associatedObjects.Count; i++)
                {
                    if (associatedObjects[i].xPosition == xToMove && associatedObjects[i].yPosition == y)
                    {
                        return true;
                    }
                }
            }
            //If double check doesn't pass, set bool to false so the whole set of objects can't move
            return false;
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    //Effect: Sets colliding bool to true which is used in OnMouseDown()
    //Called whenever there is a collision
    {
        if(other.gameObject.tag=="Object" || other.gameObject.tag == "Character" || other.gameObject.tag == "Wall") //So it doesn't happen with other misc colliders such as the grid tile's
        {
            isColliding = true;
            //Set color to red so player knows they can't set the object down there.
            if (this == manager.selectedObject || manager.selectedObject.associatedObjects.Contains(this))
            {
                Debug.Log("test");
                manager.selectedObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
                if(manager.selectedObject.associatedObjects!=null)
                {
                    for (int i = 0; i < manager.selectedObject.associatedObjects.Count; i++)
                    {
                        manager.selectedObject.associatedObjects[i].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
                    }
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    //Effect: Sets colliding bool to true which is used in OnMouseDown()
    //Called whenever there a collision ends
    {
        if (other.gameObject.tag == "Object" || other.gameObject.tag == "Character" || other.gameObject.tag == "Wall")
        {
            isColliding = false;
            //Sets color back to default
            if(this==manager.selectedObject || manager.selectedObject.associatedObjects.Contains(this))
            {
                manager.selectedObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);
                for (int i = 0; i < manager.selectedObject.associatedObjects.Count; i++)
                {
                    manager.selectedObject.associatedObjects[i].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);
                }
            }
        }
    }
}