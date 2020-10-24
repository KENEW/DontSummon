using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoSingleton<Energy>
{
	private int maxEnergy;
	private int curEnergy
	{
		get
		{
			return curEnergy;
		}
		set
		{
			curEnergy = value;
		}
	}

	private void Start()
	{
		maxEnergy = 100;
		curEnergy = 10;
	}
}
