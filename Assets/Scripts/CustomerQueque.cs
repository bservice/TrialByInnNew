﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomerQueque : MonoBehaviour
{
	//~~~Public Fields~~~
	public int size;
	public int level;
	public Customer[] innerArray;
	public Customer mercenary;
	public Customer noble;
	public Customer villager;
	public Customer wizard;
	public PauseTest pauseMenu;
	public GameUIDisplay scoreboard;
	public GameObject level1Screen;
	public GameObject level2Screen;
	public GameObject level3Screen;
	public GameObject tutorialScreen;

	//~~~Private Fields~~~
	private int index;
	private int oldLevel;
	private Vector3 pos;
	private float timeLeft;
	private bool moveScreen;
	private int prevSat;
	private float xPos;
	private float yPos;
	private GUIStyle style;

	// Start is called before the first frame update
	void Start()
	{
		level = 1;
		oldLevel = level;
		size = 4 + (2 * level);
		index = -1;
		innerArray = new Customer[size];
		scoreboard.PatronsLeft = size;
		Debug.Log(index);
		timeLeft = 3.0f;
		moveScreen = false;
		prevSat = 0;
		xPos = -1000.0f;
		yPos = -1000.0f;
		style = new GUIStyle();
		style.fontSize = 35;
		style.normal.textColor = Color.white;
		//Debug.Log(index);

		//For each data spot in the queque fill with a random NPC
		for (int i = 0; i < size; i++)
		{
			GetRandomNPC();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (!pauseMenu.Paused)
		{
			if (IsEmpty() && scoreboard.PatronsSat - prevSat == size)
			{
				ResetBoard();
			}

			if (moveScreen)
			{
				timeLeft -= Time.deltaTime;

				if (timeLeft <= 0.0f)
				{
					level1Screen.transform.position = new Vector3(10.0f, 10.0f, 0.0f);
					level2Screen.transform.position = new Vector3(10.0f, 10.0f, 0.0f);
					level3Screen.transform.position = new Vector3(10.0f, 10.0f, 0.0f);
					tutorialScreen.transform.position = new Vector3(10.0f, 10.0f, 0.0f);
					xPos = -1000.0f;
					yPos = -1000.0f;
					timeLeft = 3.0f;
					moveScreen = false;
				}
			}
		}
	}

	//To add data to the back of the queque
	void Push(Customer npc)
	{
		index++;
		//Debug.Log(index);
		//To see if the array is large enough to hold the data
		if (size <= index)
		{
			size *= 2;

			//Making a new array that is twice the size of the old one
			Customer[] temp = new Customer[size];

			//Filling in the values of the new array
			for (int i = 0; i < index; i++)
			{
				temp[i] = this.innerArray[i];
			}

			for (int i = 0; i <= index; i++)
			{
				this.innerArray[i] = temp[i];
			}
		}

		//Adding the data to the back of the queque
		this.innerArray[index] = npc;
	}

	//To remove data from the front of the queque
	public Customer Pop()
	{
		if (IsEmpty())
		{
			return null;
		}
		else
		{
			//The data we are popping
			Customer temp = innerArray[0];

			//Making the new array without the popped data
			Customer[] tempArray = new Customer[size];
			for (int i = 0; i <= index; i++)
			{
				if (i + 1 > index)
				{
					tempArray[i] = null;

				}
                else
                {
					tempArray[i] = innerArray[i + 1];
				}
			}

			for (int i = 0; i <= index; i++)
			{
				innerArray[i] = tempArray[i];
			}

			index--;
			//Debug.Log(index);
			return temp;
		}
	}

	//To see if the queque is empty
	bool IsEmpty()
	{
		if (index == -1)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	//To get a random NPC prefab and put it into the queque
	void GetRandomNPC()
	{
		float number = UnityEngine.Random.Range(0.0f, 100.0f);

		//25% chance to get a Mercenary
		if (number < 25)
		{
			/*Lucas Code
			float height = -3 + index;
			Customer merc = Instantiate(mercenary, new Vector3(-9.5f, height, 0.0f), Quaternion.identity);
			*/
			// Carson Code
			float width = -1.2f - ((index+1) * .16f);
			Customer merc = Instantiate(mercenary, new Vector3(width, -0.8f, 0.0f), Quaternion.identity);

			Push(merc);
		}

		//25% chance to get a Noble
		else if (25 <= number && number < 50)
		{
			/*Lucas Code
			float height = -3 + index;
			Customer nob = Instantiate(noble, new Vector3(-9.5f, height, 0.0f), Quaternion.identity);
			*/
			// Carson Code
			float width = -1.2f - ((index + 1) * .16f);
			Customer nob = Instantiate(noble, new Vector3(width, -0.8f, 0.0f), Quaternion.identity);

			Push(nob);

		}

		//25% chance to get a Villager
		else if (50 <= number && number < 75)
		{
			/*Lucas Code
			float height = -3 + index;
			Customer vil = Instantiate(villager, new Vector3(-9.5f, height, 0.0f), Quaternion.identity);
			*/
			// Carson Code
			float width = -1.2f - ((index + 1) * .16f);
			Customer vil = Instantiate(villager, new Vector3(width, -0.8f, 0.0f), Quaternion.identity);

			Push(vil);
		}

		//25% chance to get a Wizard
		else if (75 <= number)
		{
			/*Lucas Code
			float height = -3 + index;
			Customer wiz = Instantiate(wizard, new Vector3(-9.5f, height, 0.0f), Quaternion.identity);
			*/
			// Carson Code
			float width = -1.2f - ((index + 1) * .16f);
			Customer wiz = Instantiate(wizard, new Vector3(width, -0.8f, 0.0f), Quaternion.identity);

			Push(wiz);
		}
	}

	//To move the queque spirtes down the list outside
	public void ShiftQueque()
    {
		for (int i = 0; i <= index; i++)
		{
			pos = innerArray[i].transform.position;
			/*Lucas Code
			pos.y -= 1.0f;
			*/
			pos.x += 0.16f;
			innerArray[i].transform.position = pos;
		}
	}

	//Reset the board with the new level
	public void ResetBoard()
	{
		oldLevel = level;
		
		size = 4 + (2 * level);
		innerArray = new Customer[size];
		scoreboard.PatronsLeft = size;
		scoreboard.Score += 100;
		Debug.Log(index);
		moveScreen = true;
		prevSat = scoreboard.PatronsSat;

		if (level == 1)
		{
			tutorialScreen.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
			xPos = -1000.0f;
			yPos = -1000.0f;
		}

		if (level == 2)
		{
			level1Screen.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
			xPos = level1Screen.transform.position.x;
			yPos = level1Screen.transform.position.y;
		}

		else if (level == 3)
		{
			level2Screen.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
			xPos = level2Screen.transform.position.x;
			yPos = level2Screen.transform.position.y;
		}

		else if (level == 4)
		{
			level3Screen.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
			xPos = level3Screen.transform.position.x;
			yPos = level3Screen.transform.position.y;
		}

		else if (level == 5)
		{
			SceneManager.LoadScene("EndScene");
		}

		level++;

		//For each data spot in the queque fill with a random NPC
		for (int i = 0; i < size; i++)
		{
			GetRandomNPC();
		}
	}

	//Display score on level complete screen
	private void OnGUI()
	{
		//Score
		GUI.Label(new Rect(xPos + 910.0f, yPos + 522.0f, 22, 19), scoreboard.Score.ToString(), style);
	}
}
