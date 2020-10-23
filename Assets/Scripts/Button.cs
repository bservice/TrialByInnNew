using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{

    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(210f /255f, 198f / 255f, 140f / 255f);
        SceneManager.LoadScene(sceneName);
    }
}
