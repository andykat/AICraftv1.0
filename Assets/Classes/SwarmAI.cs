using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SwarmAI : AI {

	public SwarmAI()
	{
	}
	float[][] AI.loop(int resources, List<Unit> myUnits, List<Unit> enemyUnits)
	{
		float[][] commands = new float[10][];
		for (int i=0; i<10; i++) {
			commands [i] = new float[5];
		}
		return commands;
	}
}
