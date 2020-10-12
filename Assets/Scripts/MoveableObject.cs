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
    private bool isLifted;
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
        }
        // If the object is on the ground...
        else
        {
            // Lift it up.
            isLifted = true;
            this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.cyan);
        }
    }
}
