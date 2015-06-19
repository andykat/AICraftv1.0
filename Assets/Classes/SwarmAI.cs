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

		for (int i=1; i<myUnits.Count; i++) {
			Command mc = new Command();
			if(myUnits[0].getY () - enemyUnits[0].getY () < 0)
			{
				mc.addMove(myUnits[i].getID(), 1.0f + ((float)i)/4.0f);
			}
			else
			{
				mc.addMove(myUnits[i].getID(), -1.0f - ((float)i)/4.0f);
			}

			commands.Add(mc);
		}

		return commands;
	}
}
