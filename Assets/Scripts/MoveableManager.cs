using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages all moveable objects.
/// </summary>
public class MoveableManager : MonoBehaviour
{
    #region
    // Whether an object is selected.
    private bool objectSelected;
    // A list of all objects on the grid.
    private GameObject[] objects;
    // Marks where a moveable object is.
    public GameObject moveable;
    // A list of the position of every desired object.
    public Vector2[] objectPositions;
    // A list of the dimensions of every desired object.
    public Vector2[] objectDimensions;
    #endregion
    //Fields
    public int tables; //Holds the number of tables 
    public Grid grid; //Insert the scene grid
    public MoveableObject selectedObject; //Will be the object currently selected, changed whenever a new object is selected
    private char controlsInput; //W, A, S, or D depending on player controls input

    // Start is called before the first frame update
    void Start()
    {
        /*
        objectSelected = false;
        for (int i = 0; i < objectDimensions.Length; i++)
        {
            objects[i] = GameObject.Instantiate(moveable);
            objects[i].GetComponent<MoveableObject>().Position = objectPositions[i];
            objects[i].GetComponent<MoveableObject>().Size = objectDimensions[i];
        }
        */

        //Assign numbers for each moveable object
        //grid.ArrayGrid[1, 2].GetComponent<Square>().Position = new Vector2(0,0);

    }

    // Update is called once per frame
    void Update()
    {
        if(selectedObject==null)
        {
           //Do nothing
        }
        //If an object is selected
        else
        { 
            //Based on input, move in a particular direction
            if(controlsInput=='w')
            {
                selectedObject.MoveObject(0, -1);
            }
            else if (controlsInput == 's')
            {
                selectedObject.MoveObject(0, -1);
            }
            else if (controlsInput == 'a')
            {
                selectedObject.MoveObject(-1, 0);
            }
            else if (controlsInput == 'd')
            {
                selectedObject.MoveObject(1, 0);
            }
            
        }
    }
}
