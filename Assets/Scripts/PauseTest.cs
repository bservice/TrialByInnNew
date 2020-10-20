using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseTest : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject playButton;
    public GameObject exitButton;

    private bool paused;

    //Property to access the paused bool
    public bool Paused
    {
        get { return paused; }
    }

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        //pauseMenu = Instantiate(pauseMenu, new Vector3(100.0f, 100.0f), Quaternion.identity);
        //playButton = Instantiate(playButton, new Vector3(100.0f, 100.0f), Quaternion.identity);
        //exitButton = Instantiate(exitButton, new Vector3(100.0f, 100.0f), Quaternion.identity);
        pauseMenu.transform.position = new Vector3(100.0f, 100.0f);
        playButton.transform.position = new Vector3(100.0f, 100.0f);
        exitButton.transform.position = new Vector3(100.0f, 100.0f);
        //exitButton.gameObject.GetComponent<SpriteRenderer>().color = new Color(210f / 255f, 198f / 255f, 140f / 255f);
    }

    // Update is called once per frame
    void Update()
    {
        if (paused)
        {
            if(playButton.GetComponent<PausePlay>().Clicked)
            {
                //playButton.gameObject.GetComponent<SpriteRenderer>().color = new Color(210f / 255f, 198f / 255f, 140f / 255f);
                pauseMenu.transform.position = new Vector3(100.0f, 100.0f);
                playButton.transform.position = new Vector3(100.0f, 100.0f);
                exitButton.transform.position = new Vector3(100.0f, 100.0f);
                paused = false;
                playButton.GetComponent<PausePlay>().Clicked = false;
            }
        }
        else
        {
            //playButton.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            if (Input.GetKeyDown(KeyCode.M))
            {
                //playButton.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                paused = true;
                pauseMenu.transform.position = new Vector3(0.024f, -0.094f);
                playButton.transform.position = new Vector3(-0.269f, -0.208f);
                exitButton.transform.position = new Vector3(0.437f, -0.208f);
            }
        }
    }
}
