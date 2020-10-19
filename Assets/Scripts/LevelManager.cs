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

    //~~~Private Fields~~~
    private Customer customerRef;
    private bool inProgress;
    private bool shift;
    private Vector3 pos;


    // Start is called before the first frame update
    void Start()
    {
        inProgress = false;
        shift = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.Paused)
        {
            if (Input.GetKeyDown(KeyCode.Space) && inProgress == false)
            {
                customerRef = quequeRef.Pop();
                pos = customerRef.transform.position;

                customerRef.ActivePlaying = true;
                shift = true;
                inProgress = true;
                Debug.Log(inProgress);
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
            }


        }

    }
}
