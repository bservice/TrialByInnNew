using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class CustomerQueque : MonoBehaviour
{
	//~~~Public Fields~~~
	public int size;
	public int level;
	public GameObject[] innerArray;
	public GameObject mercenary;
	public GameObject noble;
	public GameObject villager;
	public GameObject wizard;

	//~~~Private Fields~~~
	private int index;
	private int oldLevel;

	// Start is called before the first frame update
	void Start()
	{
		level = 1;
		oldLevel = level;
		size = 3 + (2 * level);
		index = -1;
		innerArray = new GameObject[size];

		//For each data spot in the queque fill with a random NPC
		for (int i = 0; i < size; i++)
		{
			GetRandomNPC();
		}
	}

	// Update is called once per frame
	void Update()
	{
		//Re-make the queque based on the level
		if(oldLevel < level && IsEmpty())
        {
			size = 3 + (2 * level);
			//For each data spot in the queque fill with a random NPC
			for (int i = 0; i < size; i++)
			{
				GetRandomNPC();
			}
			oldLevel = level;
		}
	}

	//To add data to the back of the queque
	void Push(GameObject npc)
	{
		index++;

		//To see if the array is large enough to hold the data
		if (size <= index)
		{
			size *= 2;

			//Making a new array that is twice the size of the old one
			GameObject[] temp = new GameObject[size];

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
	GameObject Pop()
	{
		if (IsEmpty())
		{
			return null;
		}
		else
		{
			//The data we are popping
			GameObject temp = innerArray[0];

			//Making the new array without the popped data
			GameObject[] tempArray = new GameObject[size];
			for (int i = 0; i <= index; i++)
			{
				tempArray[i] = innerArray[i + 1];
			}

			for (int i = 0; i <= index; i++)
			{
				innerArray[i] = tempArray[i];
			}

			index--;
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
			GameObject merc = Instantiate(mercenary, new Vector3(-1000.0f, 0.0f, 0.0f), Quaternion.identity);
			Push(merc);
		}

		//25% chance to get a Noble
		else if (25 <= number && number < 50)
		{
			GameObject nob = Instantiate(mercenary, new Vector3(-1000.0f, 0.0f, 0.0f), Quaternion.identity);
			Push(nob);
		}

		//25% chance to get a Villager
		else if (50 <= number && number < 75)
		{
			GameObject vil = Instantiate(mercenary, new Vector3(-1000.0f, 0.0f, 0.0f), Quaternion.identity);
			Push(vil);
		}

		//25% chance to get a Wizard
		else if (75 <= number)
		{
			GameObject wiz = Instantiate(mercenary, new Vector3(-1000.0f, 0.0f, 0.0f), Quaternion.identity);
			Push(wiz);
		}
	}
}
