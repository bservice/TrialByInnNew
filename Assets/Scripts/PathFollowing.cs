using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{
    public bool start = false;

    public int startX;
    public int startY;
    public int endX;
    public int endY;


    private int columns;
    private int rows;
    private int curX;
    private int curY;

    private int[,] floorPlan;
    private List<int> unvisitedX;
    private List<int> unvisitedY;
    private List<int> visitedX;
    private List<int> visitedY;
    private List<int> pathX;
    private List<int> pathY;


    // Start is called before the first frame update
    void Start()
    {
        // Replace with link to grid
        floorPlan = new int[15, 10];

        visitedX = new List<int>();
        visitedY = new List<int>();
        unvisitedX = new List<int>();
        unvisitedY = new List<int>();
        pathX = new List<int>();
        pathY = new List<int>();

        // Replace with player clicks
        startX = 0;
        startY = 0;
        endX = 14;
        endY = 9;

        curX = startX;
        curY = startY;

        visitedX.Add(curX);
        visitedY.Add(curY);
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            for (int x = curX - 1; x < curX + 1; x++)
            {
                for (int y = curY - 1; y < curY + 1; y++)
                {
                    // Skip current
                    if (x == curX && y == curY)
                        return;

                    // Skip diagonals
                    if (x == curX - 1 && y == curY - 1 ||
                        x == curX - 1 && y == curY + 1 ||
                        x == curX + 1 && y == curY - 1 ||
                        x == curX + 1 && y == curY + 1)
                    {
                        return;
                    }

                    // Bounds
                    if (x < 0 || x > columns && y < 0 || y > rows)
                        return;

                    // Visited
                    for (int i = 0; i < visitedX.Count - 1; i++)
                    {
                        if (curX == visitedX[i] && curY == visitedY[i])
                            return;
                    }

                    // Obstacle
                    if (floorPlan[curX, curY] != 0)
                        return;

                    // End
                    if (curX == endX && curY == endY)
                    {
                        //Add the finish to the visited list
                        visitedX.Add(curX);
                        visitedY.Add(curY);

                    }

                    // Add


                }
            }
        }
    }
}