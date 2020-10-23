using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    private bool clicked;

    public string sceneName;

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

    public void OnMouseDown()
    {        
        gameObject.GetComponent<SpriteRenderer>().color = new Color(210f / 255f, 198f / 255f, 140f / 255f);
        clicked = true;
        SceneManager.LoadScene(sceneName);
    }
}
