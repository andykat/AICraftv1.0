using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class SwarmAI : AI {
	private int resources;
	private List<Unit> myUnits;
	private List<Unit> enemyUnits;
	private int currentSpawn = 3;

	public SwarmAI()
	{
	}
	List<Command> AI.loop(int tResources, List<Unit> tMyUnits, List<Unit> tEnemyUnits)
	{
		resources = tResources;
		myUnits = tMyUnits;
		enemyUnits = tEnemyUnits;
		List<Command> commands = new List<Command>();

		//spawn random units whenever possible
		Command spawnc = new Command();
		if (resources < 6 || resources > 79) {
			currentSpawn = UnityEngine.Random.Range (2, 5);
		}
		spawnc.addSpawn (currentSpawn);
		commands.Add (spawnc);


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
				moveC.addMove(myUnits[i].getID(), getDirection(myUnits[i].getX (), myUnits[i].getY (), enemyUnits[0].getX(), enemyUnits[0].getY()));
				commands.Add (moveC);
			}

		}

		return commands;
	}

	private float getDirection(float x1, float y1, float x2, float y2)
	{
		return ((float) Math.Atan2 (y2 - y1, x2 - x1 ));
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