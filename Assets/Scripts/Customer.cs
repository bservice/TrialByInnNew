using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    //~~~Public Fields~~~
    public float speed = 0.003f;

    //~~~Private Fields~~~
    private bool activePlaying = false;
    private Vector3 pos;

    //~~~Properties~~~
    public bool ActivePlaying
    {
        get
        {
            return activePlaying;
        }
        set
        {
            activePlaying = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos = this.transform.position;
        //If the current customer activate the movement
        if (activePlaying)
        {
            if (Input.GetKey(KeyCode.W))
            {
                pos.y += speed;
                this.transform.position = pos;
            }
            if (Input.GetKey(KeyCode.A))
            {
                pos.x -= speed;
                this.transform.position = pos;
            }
            if (Input.GetKey(KeyCode.S))
            {
                pos.y -= speed;
                this.transform.position = pos;
            }
            if (Input.GetKey(KeyCode.D))
            {
                pos.x += speed;
                this.transform.position = pos;
            }
        }
    }
}
