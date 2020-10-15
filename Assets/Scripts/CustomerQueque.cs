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
	public Customer[] innerArray;
	public Customer mercenary;
	public Customer noble;
	public Customer villager;
	public Customer wizard;

	//~~~Private Fields~~~
	private int index;
	private int oldLevel;
	private Vector3 pos;

	// Start is called before the first frame update
	void Start()
	{
		level = 1;
		oldLevel = level;
		size = 3 + (2 * level);
		index = -1;
		innerArray = new Customer[size];

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
	void Push(Customer npc)
	{
		index++;

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
			float height = -3 + index;
			Customer merc = Instantiate(mercenary, new Vector3(-9.5f, height, 0.0f), Quaternion.identity);
			
			Push(merc);
		}

		//25% chance to get a Noble
		else if (25 <= number && number < 50)
		{
			float height = -3 + index;
			Customer nob = Instantiate(noble, new Vector3(-9.5f, height, 0.0f), Quaternion.identity);
			Push(nob);
		}

		//25% chance to get a Villager
		else if (50 <= number && number < 75)
		{
			float height = -3 + index;
			Customer vil = Instantiate(villager, new Vector3(-9.5f, height, 0.0f), Quaternion.identity);
			Push(vil);
		}

		//25% chance to get a Wizard
		else if (75 <= number)
		{
			float height = -3 + index;
			Customer wiz = Instantiate(wizard, new Vector3(-9.5f, height, 0.0f), Quaternion.identity);
			Push(wiz);
		}
	}

	//To move the queque spirtes down the list outside
	public void ShiftQueque()
    {
		for (int i = 0; i <= index; i++)
		{
			pos = innerArray[i].transform.position;
			pos.y -= 1.0f;
			innerArray[i].transform.position = pos;
		}
	}
}
