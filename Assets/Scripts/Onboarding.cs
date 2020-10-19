using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onboarding : MonoBehaviour
{
    //bools to control when each speech bubble gets created
    private bool welcome;
    private bool clickInstruct;
    private bool moveInstruct;
    private bool tableInstruct;
    private bool bonus;
    private bool correctSeat;
    private bool wrongSeat;

    //GameObject to hold the host
    public GameObject host;

    //Vectors to track the host's position and previous position
    private Vector3 prevHostPos;
    private Vector3 hostPos;

    //List to hold the speech bubble objects
    public List<GameObject> bubbles;
    private int count;

    //Position floats for the pubbles
    private float xPos;
    private float yPos;

    public bool Welcome { 
        get { return welcome; }
        set
        {
            welcome = value;
        }
    }
    public bool ClickInstruct
    {
        get { return clickInstruct; }
        set
        {
            clickInstruct = value;
        }
    }
    public bool MoveInstruct
    {
        get { return moveInstruct; }
        set
        {
            moveInstruct = value;
        }
    }
    public bool TableInstruct
    {
        get { return tableInstruct; }
        set
        {
            tableInstruct = value;
        }
    }
    public bool Bonus
    {
        get { return bonus; }
        set
        {
            bonus = value;
        }
    }
    public bool CorrectSeat
    {
        get { return correctSeat; }
        set
        {
            correctSeat = value;
        }
    }
    public bool WrongSeat
    {
        get { return wrongSeat; }
        set
        {
            wrongSeat = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initializing everything to the proper values
        welcome = false;
        clickInstruct = false;
        moveInstruct = false;
        tableInstruct = false;
        bonus = false;
        correctSeat = false;
        wrongSeat = false;
        SetBubbleActive(0, welcome);
        SetBubbleActive(1, clickInstruct);
        SetBubbleActive(2, moveInstruct);
        SetBubbleActive(3, tableInstruct);
        SetBubbleActive(4, bonus);
        SetBubbleActive(5, correctSeat);
        SetBubbleActive(6, wrongSeat);
        xPos = host.transform.position.x;
        yPos = host.transform.position.y;
        prevHostPos = host.transform.position;
        SetPosition();
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Check the host's position every frame and if it is not equal to its previous position, update the bubbles' position
        //Saves on for loop calculations
        hostPos = host.transform.position;
        if (hostPos != prevHostPos)
        {
            SetPosition();
            prevHostPos = host.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            welcome = !welcome;
        }
    }

    //Updates the position of the bubble so it is near the host
    public void SetPosition()
    {
        xPos = host.transform.position.x + 2.8f;
        yPos = host.transform.position.y + 2.24f;

        for(int i = 0; i < bubbles.Count; i++)
        {
            bubbles[i].transform.position = new Vector3(xPos, yPos);
        }
    }

    //Set the bubble active
    //*****CALL THIS METHOD IN OTHER CLASSES TO SET THE BUBBLES*****
    public void SetBubbleActive(int num, bool bubbleBool)
    {
        bubbles[num].SetActive(bubbleBool);
    }
}
