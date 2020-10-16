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
        if(selectedObject==null)
        {
           //Do nothing
        }
        //If the selected object is a table...
        else if (selectedObject.tag == "table")
        {
            selectedObject.MoveObject(1, 0);
        }
        //If any other piece of furniture (e.g barrel)
        else
        {

        }
    }
}
