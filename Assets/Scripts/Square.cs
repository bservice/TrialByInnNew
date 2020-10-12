using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    #region Fields
    // The position of the top left corner of the square.
    public Vector2 position;
    // The size of the square in pixels.
    private float size;
    // Whether or not the square has an object placed on it.
    private bool empty;
    #endregion

    #region Properties
    public Vector2 Position
    {
        get { return position; }
        set { position = value; }
    }
    public float Size
    {
        get { return size; }
        set { size = value; }
    }
    public bool isEmpty
    {
        get { return empty; }
        set { empty = value; }
    }
    // Getting the points on the square.
    public Vector2 getTopLeft
    {
        get { return position; }
    }
    public Vector2 getTopRight
    {
        get { return new Vector2(position.x + size, position.y); }
    }
    public Vector2 getBottomRight
    {
        get { return new Vector2(position.x + size, position.y + size); }
    }
    public Vector2 getBottomLeft
    {
        get { return new Vector2(position.x, position.y + size); }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        size = 1.0f;
        empty = true;
        // Setting the squares to green by default.
        this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = position;
    }

    // What happens when the mouse hovers over a square.
    private void OnMouseOver()
    {
        this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.red);
    }

    // What happens when the mouse exits a square.
    private void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().material.SetColor("_Color", Color.green);
    }
}
