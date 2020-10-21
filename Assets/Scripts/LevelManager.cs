using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //~~~Public Fields~~~
    public CustomerQueque quequeRef;
    public GameObject target;
    public PauseTest pauseMenu;
    public GameUIDisplay scoreboard;
    public Onboarding onboard;

    //~~~Private Fields~~~
    private Customer customerRef;
    private bool inProgress;
    private bool shift;
    private Vector3 pos;
    private bool welcome;
    private float timeLeft;
    private float timeLeft2;
    private bool click;
    private bool table;
    private bool table2;
    private bool bonus;
    private bool bonus2;


    // Start is called before the first frame update
    void Start()
    {
        inProgress = false;
        shift = false;
        welcome = true;
        timeLeft = 5.0f;
        timeLeft2 = 5.0f;
        click = false;
        table2 = true;
        bonus = false;
        bonus2 = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Setting the bubbles based on their booleans
        onboard.SetBubbleActive(0, welcome);
        onboard.SetBubbleActive(1, click);
        onboard.SetBubbleActive(3, table);
        onboard.SetBubbleActive(4, bonus);

        //Only run the game loop if not paused
        if (!pauseMenu.Paused)
        {
            //If statement that will allow the table bubble to only display once
            if (onboard.TableInstruct && table2)
            {
                table = true;
                table2 = false;
            }

            //Start a timer to turn off the bonus bubble if it is active
            if(bonus)
            {
                timeLeft2 -= Time.deltaTime;

                if (timeLeft2 <= 0.0f)
                {
                    bonus = false;
                    bonus2 = false;
                }
            }

            //Decrease timer which starts at 5
            timeLeft -= Time.deltaTime;

            //Turn off the welcome bubble and turn on click
            if (timeLeft <= 0.0f && welcome)
            {
                welcome = false;
                click = true;
            }

            //Don't allow player to select a patron while the welcome message is playing
            if (!welcome)
            {
                if (Input.GetKeyDown(KeyCode.Space) && inProgress == false)
                {
                    customerRef = quequeRef.Pop();
                    pos = customerRef.transform.position;

                    customerRef.ActivePlaying = true;
                    shift = true;
                    inProgress = true;
                    if (click)
                        onboard.MoveInstruct = true; //Set move instruct to true, will use this to turn on the move instructions from customer script
                    //Turning off click when the first patron is selected
                    click = false;

                    Debug.Log(inProgress);
                }
            }
            /* Lucas Code
            if (Vector3.Distance(pos, customerRef.transform.position) > 2.0f && shift)
            {
                quequeRef.ShiftQueque();
                shift = false;
            }

            if (Vector3.Distance(customerRef.transform.position, target.transform.position) <= 2.75f)
            {
                customerRef.ActivePlaying = false;
                inProgress = false;
            }
            */

            // Carson Code
            // Temp switches on Left Arrow Press
            if (Input.GetKeyDown(KeyCode.P))
            {
                quequeRef.ShiftQueque();
                scoreboard.Score += 10;
                scoreboard.PatronsSat++;
                scoreboard.PatronsLeft--;
                shift = false;
                customerRef.ActivePlaying = false;
                inProgress = false;
                if(bonus2)
                    bonus = true;
                table = false;                
            }


        }

    }
}
