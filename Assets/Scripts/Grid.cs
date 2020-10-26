using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

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
    //Queque ref
    public CustomerQueque queRef;
    //Table Prefab
    public GameObject table;
    // The matrix of squares.
    private GameObject[,] squares;
    //The Targets for the player
    private Vector2[] targets;
    private GameObject[] tables;

    private int prev;
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
    public GameObject[,] ArrayGrid 
    { 
        get { return squares; } 
    }
    public Vector2[] Targets
    {
        get { return targets; }
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

        targets = new Vector2[5];

        tables = new GameObject[5];
        populateTargets(5);
    }

    // Update is called once per frame
    void Update()
    {
        if(queRef.level != prev)
        {
            targets = new Vector2[queRef.size];
        }
        prev = queRef.level;
    }

    void populateTargets(int sizeofQue)
    {
        //for(int i = 0; i < sizeofQue; i++)
        //{
        //    Vector2 temp;
        //    temp.x = Random.Range(1, 14);
        //    temp.y = Random.Range(1, 9);
        //
        //    if (i != 0 && temp.x == targets[i-1].x)
        //    {
        //        temp.x = Random.Range(1, 14);
        //    }
        //
        //    if (i != 0 && temp.y == targets[i - 1].y)
        //    {
        //        temp.y = Random.Range(1, 9);
        //    }
        //
        //    targets[i] = new Vector2(squares[(int)temp.x, (int)temp.y].GetComponent<Square>().position.x, squares[(int)temp.x, (int)temp.y].GetComponent<Square>().position.y);
        //    squares[(int)temp.x, (int)temp.y].GetComponent<Square>().tar = true;
        //}

        for (int i = 0; i < 5; i++)
        {
            Vector3 temp;
            bool correct = false;
            temp.x = Random.Range(1, 14);
            temp.y = Random.Range(1, 9);
            temp.z = 0;

            for (int j = 0; j < 5; j++)
            {
                //If j == i break out of loop.  This happens when the j is trying to access a non instatated table
                if (i == j)
                {
                    break;
                }

                int oldX = tables[j].GetComponent<MoveableObject>().xPosition;
                int oldY = tables[j].GetComponent<MoveableObject>().yPosition;

                while (!correct)
                {
                    //Gets the new x and y pos of the tables
                    if (
                        i != 0 &&
                        oldX <= temp.x && temp.x <= (oldX + 4) &&
                        oldY <= temp.y && temp.y <= (oldY + 2)
                        )
                    {
                        temp.x = Random.Range(1, 14);
                        temp.y = Random.Range(1, 9);
                    }
                    else
                    {
                        correct = true;
                    }
                }
                correct = false;
            }

            tables[i] = Instantiate(table);
            tables[i].GetComponent<MoveableObject>().xPosition = (int)temp.x;
            tables[i].GetComponent<MoveableObject>().yPosition = (int)temp.y;
        }
    }
}
