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
        if (selectedObject==null)
        {
           //Do nothing
        }
        //If an object is selected
        else
        { 
            //Based on input, move in a particular direction
            if(Input.GetKeyDown(KeyCode.W))
            {
                MoveAttempt('w');
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                MoveAttempt('s');
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                MoveAttempt('a');
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MoveAttempt('d');
            }

        }
    }

    void MoveAttempt(char input)
    {
        int x = 0;
        int y = 0; 
        //Sets x and y to move by based on direction
        if (input == 'w') { x = 0; y = 1; }
        else if (input == 's') { x = 0; y = -1; }
        else if (input == 'a') { x = -1; y = 0; }
        else if (input == 'd') { x = 1; y = 0; }
        bool canMove = true; //Becomes false if any of the objects that will be moved can't move to that space
        //Check free tile for the selected object
        if(!selectedObject.FreeMoveCheck(x, y))
        {
            canMove = false;
        }
        //Check free tile for all its buddies
        if (selectedObject.associatedObjects.Count != 0)
        {
            for (int i = 0; i < selectedObject.associatedObjects.Count; i++)
            {
                //If you can't move the object into that spot
                if (!selectedObject.associatedObjects[i].FreeMoveCheck(x, y))
                {
                    canMove = false;
                    //Call a method that turns object(s) red briefly 
                }
            }
        }
        //If all objects can move to their new tiles, move them
        if (canMove)
        {
            selectedObject.MoveObject(x, y);
            //If there are associated objects, move them
            if (selectedObject.associatedObjects.Count != 0)
            {
                for (int i = 0; i < selectedObject.associatedObjects.Count; i++)
                {
                    selectedObject.associatedObjects[i].MoveObject(x, y);
                }
            }
        }
    }

}
