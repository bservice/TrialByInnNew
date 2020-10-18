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
    public MoveableManager manager;
    public Grid grid;
    // The position of the top left corner of the square.
    public Vector2 position;
    // The size of the object in pixels.
    private Vector2 size;
    // Whether or not the object is being lifted.
    public bool isLifted;
    public int xPosition; //Holds what x position in the grid the object is on
    public int yPosition; //Holds what y position in the grid the object is on
    public int width; //How many tiles wide the object is
    public int height; //How many tiles tall the object is
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

    // Start is called before the first frame update
    void Start()
    {
        // Setting the object to blue by default.
        this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.blue);
        manager = FindObjectOfType<MoveableManager>(); //Search for manager in the scene
        //grid =  grab from gamemanager;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // What happens when the mouse button is clicked.
    private void OnMouseDown()
    {
        // If the object is in the air...
        if (isLifted)
        {
            // Put it down.
            isLifted = false;
            this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.blue);
            //Set the manager's selectedObject to empty
            manager.selectedObject = null;
        }
        // If the object is on the ground...
        else
        {
            // Lift it up.
            isLifted = true;
            this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);
            //Set the manager's selectedObject to this object
            manager.selectedObject = this;
        }
    }

    public void MoveObject(int x, int y)
    //Effect: Moves the object to a new position based on xy grid position
    //Called in: MoveableManager
    {
        //Adds the amount we are moving by to the position so we know what part of the grid to move to.
        int xToMoveTo = xPosition + x;
        int yToMoveTo = yPosition + y;
        //Sets transform = the world position of that grid tile
        transform.position = grid.ArrayGrid[xToMoveTo, yToMoveTo].GetComponent<Square>().Position;
        //Set the grid position of the object to its new position
        xPosition = xToMoveTo;
        yPosition = yToMoveTo;
    }

    public bool FreeMoveCheck(int x, int y)
    //Effect: Checks if the tile moving into is free (***Also going to need to make this check work so that it is okay with it being occupied by another object in its group)
    //Called in: MoveableManager
    {
        //Adds the amount we are moving by to the position so we know what part of the grid to move to.
        int xToMove = xPosition + x;
        int yToMove = yPosition + y;
        //Return true if the tile is empty, and if it's not a wall.
        if ( grid.ArrayGrid[xToMove, yToMove].GetComponent<Square>().isEmpty && ((xToMove<grid.gridSize.x) && (xToMove>-1) && (yToMove<grid.gridSize.y) && (yToMove>-1)) ) //Add the wall check part
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
}
