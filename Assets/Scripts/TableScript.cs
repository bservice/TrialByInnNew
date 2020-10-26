using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableScript : MonoBehaviour
{
    #region Fields
    //List to hold the tables
    List<MoveableObject> tables;
    //Number of tables

    public Animator animator;
    private MoveableManager manager;

    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<MoveableManager>(); //Search for manager in the scene
       

        //Give all tables the "table" tag

        //Assign each a number so we can know it apart from other Tables
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<MoveableObject>().IsLifted)
        {
            animator.SetBool("AnimTable", true);
        }
        else
        {
            animator.SetBool("AnimTable", false);
        }
        
    }
    #endregion
}
