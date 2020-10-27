using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour
{
    #region Fields
    public bool isHorizontal; //True if horizontal, false if vertical.
    public int tableNumber; //What number the table is for association purposes (Maybe unnecessary?)
    public GameObject seatCollider;
    public GameObject sCollider;
    public GameObject table;
    public Animator animator;
    private GameObject manager;
    

    public int xPosition;
    public int yPosition;

    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        table = gameObject;
        //grid = GameObject.Find("Grid");
        manager = GameObject.Find("MoveableManager");
        //Set tableNumber to index in list
        tableNumber = manager.GetComponent<MoveableManager>().tables.IndexOf(this.gameObject);

        //Instantiate a box collider around the table to act as seats (This collider is a separate game object and separate from the table's own collider)
        sCollider = Instantiate(seatCollider, this.gameObject.transform.position, Quaternion.identity); //Creates an empty moveable object that just has a collider
        xPosition = table.GetComponent<MoveableObject>().xPosition;
        yPosition = table.GetComponent<MoveableObject>().yPosition;
        //Set name of collider to match the table
        sCollider.gameObject.name = "TableCollider" + tableNumber;

        //this.gameObject.GetComponent<MoveableObject>().associatedObjects.Add(sCollider);
        //sCollider.xPosition = this.gameObject.GetComponent<MoveableObject>().xPosition;
        //sCollider.yPosition = this.gameObject.GetComponent<MoveableObject>().yPosition;

    }

    // Update is called once per frame
    void Update()
    {
        //If selected, make the table have an animation
        if (this.gameObject.GetComponent<MoveableObject>().IsLifted)
        {
            animator.SetBool("AnimTable", true);
        }
        else
        {
            animator.SetBool("AnimTable", false);
        }

        //Make the table's seat collider travel with the table
        xPosition = table.GetComponent<MoveableObject>().xPosition;
        yPosition = table.GetComponent<MoveableObject>().yPosition;
        sCollider.transform.position = table.GetComponent<MoveableObject>().grid.GetComponent<Grid>().ArrayGrid[xPosition, yPosition].GetComponent<Square>().Position;

    }
    #endregion
}
