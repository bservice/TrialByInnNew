using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTest : MonoBehaviour
{
    public GameObject pauseMenu;
    public PausePlay playButton;
    public GameObject exitButton;

    private bool paused;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        pauseMenu = Instantiate(pauseMenu, new Vector3(100.0f, 100.0f), Quaternion.identity);
        playButton = Instantiate(playButton, new Vector3(100.0f, 100.0f), Quaternion.identity);
        exitButton = Instantiate(exitButton, new Vector3(100.0f, 100.0f), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            if(playButton.Clicked)
            {
                pauseMenu.transform.position = new Vector3(100.0f, 100.0f);
                playButton.transform.position = new Vector3(100.0f, 100.0f);
                exitButton.transform.position = new Vector3(100.0f, 100.0f);
                paused = false;
                playButton.Clicked = false;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                paused = true;
                pauseMenu.transform.position = new Vector3(0.0f, 0.0f);
                playButton.transform.position = new Vector3(-1.8f, -1.48f);
                exitButton.transform.position = new Vector3(2.43f, -1.48f);
            }
        }
    }
}
