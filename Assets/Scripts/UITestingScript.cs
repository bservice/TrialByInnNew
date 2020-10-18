using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITestingScript : MonoBehaviour
{
    private Onboarding onboard;
    private bool test;

    // Start is called before the first frame update
    void Start()
    {
        test = false;
        onboard = FindObjectOfType<Onboarding>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            FindObjectOfType<GameUIDisplay>().Score += 5;
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            onboard.SetBubbleActive(1, true);
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            onboard.SetBubbleActive(1, false);
        }
    }
}
