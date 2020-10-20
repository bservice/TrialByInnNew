using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An object that the player can move.
/// Could parent other classes (like a bard class or something).
/// </summary>
public class MoveableObject : MonoBehaviour
{
    #region Fields
    //GameObjects this is dependent on
    public MoveableManager manager;
    public Grid grid;
    // The position of the top left corner of the square.
    public Vector2 position;
    // The size of the object in pixels.
    private Vector2 size;
    // Whether or not the object is being lifted.
    public bool isLifted;
    //Whether or not the object is colliding with another object
    public bool isColliding;

    public int xPosition; //Holds what x position in the grid the object is on
    public int yPosition; //Holds what y position in the grid the object is on
    /*public int width; //How many tiles wide the object is
    public int height; //How many tiles tall the object is*/
    public List<MoveableObject> associatedObjects; //List full of the friends of the object that move with it
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
    /*Methods:
     * Start
     * Update
     * OnMouseDown
     * MoveObject
     * CheckMoveCheck*/

    // Start is called before the first frame update
    void Start()
    {
        //Get managers/other dependencies
        manager = FindObjectOfType<MoveableManager>(); //Search for manager in the scene
        grid = manager.grid; //Grab grid from manager
        //Moves object to its starting location


        isColliding = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Puts the object at the world position of their current tile (mostly used to get it in the right place at the start)
        transform.position = grid.ArrayGrid[xPosition, yPosition].GetComponent<Square>().Position;
    }

    // What happens when the mouse button is clicked.
    private void OnMouseDown()
    {
        // If the object is in the air...
        if (isLifted)
        {
            //If intersecting another object, don't allow player to place object.
            if(isColliding)
            {
                return;
            }
            //Otherwise put object down 
            else
            {
                // Put it down.
                isLifted = false;
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
            if(manager.selectedObject==null)
            {
                // Lift it up.
                isLifted = true;
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

    public void MoveObject(int x, int y)
    //Effect: Moves the object to a new position based on xy grid position
    //Called in: MoveableManager
    {
        //Mark current tiles taken up as empty
        grid.ArrayGrid[xPosition, yPosition].GetComponent<Square>().isEmpty = true;
        //How to do this based on height and width...

        //Adds the amount we are moving by to the position so we know what part of the grid to move to.
        int xToMoveTo = xPosition + x;
        int yToMoveTo = yPosition + y;
        //Sets transform = the world position of that grid tile
        transform.position = grid.ArrayGrid[xToMoveTo, yToMoveTo].GetComponent<Square>().Position;
        //Set the grid position of the object to its new position
        xPosition = xToMoveTo;
        yPosition = yToMoveTo;
        //Mark the squares that are taken up as NOT empty
        grid.ArrayGrid[xPosition, yPosition].GetComponent<Square>().isEmpty = false;
            //How to do this based on height and width...
            //for(int i = -width/2; i<width/2; i++)
    }

    public bool FreeMoveCheck(int x, int y)
    //Effect: Checks if the tile moving into is free (***Also going to need to make this check work so that it is okay with it being occupied by another object in its group)
    //Called in: MoveableManager
    {
        //Adds the amount we are moving by to the position so we know what part of the grid to move to.
        int xToMove = xPosition + x;
        int yToMove = yPosition + y;
        //Return true if the tile is empty, and if it's not a wall.
        if (((xToMove < grid.gridSize.x) && (xToMove > -1) && (yToMove < grid.gridSize.y) && (yToMove > -1)) && grid.ArrayGrid[xToMove, yToMove].GetComponent<Square>().isEmpty)
        {
            return true;
        }
        //Return false if the tile is occupied
        else
        {
            //Double check that it's not a part of its height/width

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
        if(other.gameObject.tag=="Object") //So it doesn't happen with other misc colliders such as the grid tile's
        {
            isColliding = true;
            //Set color to red so player knows they can't set the object down there.
            if(this==manager.selectedObject)
            {
                this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
                for (int i = 0; i < associatedObjects.Count; i++)
                {
                    associatedObjects[i].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
                }
            }
            //If this is an object attached to the selected object, don't let them put the selected object down.
            else if(manager.selectedObject.associatedObjects.Contains(this))
            {
                manager.selectedObject.isColliding = true;
                manager.selectedObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
                for (int i = 0; i < manager.selectedObject.associatedObjects.Count; i++)
                {
                    manager.selectedObject.associatedObjects[i].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
                }
            }
        }
    }
    //Effect: Sets colliding bool to true which is used in OnMouseDown()
    //Called whenever there a collision ends
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Object")
        {
            isColliding = false;
            //Sets color back to default
            if(this==manager.selectedObject)
            {
               this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);
                for (int i = 0; i < associatedObjects.Count; i++)
                {
                    associatedObjects[i].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);
                }
            }
            else if (manager.selectedObject.associatedObjects.Contains(this))
            {
                manager.selectedObject.isColliding = false;
                manager.selectedObject.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan    );
                for (int i = 0; i < manager.selectedObject.associatedObjects.Count; i++)
                {
                    manager.selectedObject.associatedObjects[i].GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);
                }
            }
        }
    }
}