using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class SwarmAI : AI {

	public SwarmAI()
	{
	}
	List<Command> AI.loop(int resources, List<Unit> myUnits, List<Unit> enemyUnits)
	{
		List<Command> commands = new List<Command>();

		//spawn marines whenever possible
		Command spawnc = new Command();
		spawnc.addSpawn (3);
		commands.Add (spawnc);

		float dir = -1.5708f;
		if(myUnits[0].getY () - enemyUnits[0].getY () < 0)
		{
			dir = 1.5708f;
		}

		//handle all units
		for (int i=1; i<myUnits.Count; i++) {
			//gets coordinates and attackRange of unit
			float attackRange = myUnits[i].getAttackRange();
			float tx = myUnits[i].getX ();
			float ty = myUnits[i].getY ();
			bool attacked = false;
			for(int j=0; j<enemyUnits.Count;j++)
			{
				//check if enemy is within attack Rnage
				if(dist(tx,ty, enemyUnits[j].getX (), enemyUnits[j].getY ()) < attackRange)
				{
					if(enemyUnits[j].getIsGround())
					{
						if(!myUnits[i].getCanAttackGround())
						{
							continue;
						}
					}
					else
					{
						if(!myUnits[i].getCanAttackAir())
						{
							continue;
						}
					}
					//Sends attack command
					Command tAttack = new Command();
					tAttack.addAttack(myUnits[i].getID(), enemyUnits[j].getID());
					commands.Add (tAttack);
					attacked = true;
					break;
				}
			}

			if(!attacked)
			{
				//did not find someone to attack

				//move forward
				Command moveC = new Command();
				moveC.addMove(myUnits[i].getID(), dir);
				commands.Add (moveC);
			}

		}

		return commands;
	}

	private float dist(float x1, float y1, float x2, float y2)
	{
		return ((float)Math.Sqrt(((double) (  (x2-x1)*(x2-x1) + (y2-y1)*(y2-y1)  )   )));
	}
}
/*for (int i=1; i<myUnits.Count; i++) {
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
		}*/