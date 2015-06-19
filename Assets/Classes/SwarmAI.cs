using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SwarmAI : AI {

	public SwarmAI()
	{
	}
	List<Command> AI.loop(int resources, List<Unit> myUnits, List<Unit> enemyUnits)
	{
		List<Command> commands = new List<Command>();
		Command c = new Command();
		c.addSpawn (2);
		commands.Add (c);
		return commands;
	}
}
