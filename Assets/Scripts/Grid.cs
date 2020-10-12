using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds all of the spaces on the grid.
/// </summary>
public class Grid : MonoBehaviour
{
    #region Fields
    // The number of rows and columns in the grid.
    public Vector2 gridSize;
    // Marks the square on the screen.
    public GameObject spot;
    // The size of the boxes.
    public float size;
    // The matrix of squares.
    private GameObject[,] squares;
    #endregion

    #region Properties
    public int xAxisLength
    {
        get { return (int)gridSize.x; }
        set { gridSize.x = (float)value; }
    }
    public int yAxisLength
    {
        get { return (int)gridSize.y; }
        set { gridSize.y = (float)value; }
    }
    public int getGridSize
    {
        get { return (int)(gridSize.x * gridSize.y); }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Initializing the squares.
        squares = new GameObject[xAxisLength, yAxisLength];
        // The positions of the squares.
        float xPos = -((xAxisLength * size) / 2);
        float yPos = -(yAxisLength * size / 2);
        for (int y = 0; y < yAxisLength; y++)
        {
            for(int x = 0; x < xAxisLength; x++)
            {
                squares[x, y] = GameObject.Instantiate(spot);
                squares[x, y].GetComponent<Square>().Position = new Vector2(xPos, yPos);
                squares[x, y].GetComponent<Square>().Size = size;
                xPos += size;
            }
            yPos += size;
            xPos = -((xAxisLength * size) / 2);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
