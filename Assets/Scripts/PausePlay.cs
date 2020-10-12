using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePlay : MonoBehaviour
{
    private bool clicked;

    public bool Clicked
    {
        get { return clicked; }
        set
        {
            clicked = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        clicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
       
       clicked = true;
        
    }
}
