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
    // The position of the top left corner of the square.
    public Vector2 position;
    // The size of the object in pixels.
    private Vector2 size;
    // Whether or not the object is being lifted.
    public bool isLifted;
    #endregion
    public MoveableManager manager;
    public Grid grid;
    public int xPosition; //Holds what x position in the grid the object is on
    public int yPosition; //Holds what y position in the grid the object is on
    
    //public Vector2 position;

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
        //If the object is selected, you can move it around with WASD.
        if(isLifted)
        {

        }
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

        //Check for obstacles/occupied tile? Also walls
        if(ObstacleCheck())
        {
            //If there aren't any check if there are for attached objects, then move them

            //Sets transform = the world position of that grid tile
            transform.position = grid.ArrayGrid[xToMoveTo, yToMoveTo].GetComponent<Square>().Position;
            //Set the grid position of the object to its new position
            xPosition = xToMoveTo;
            yPosition = yToMoveTo;
        }
    }
        

    public bool ObstacleCheck()
    {



    }
}
