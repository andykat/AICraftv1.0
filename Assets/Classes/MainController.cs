using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class MainController : MonoBehaviour {
	public GameObject[] unitObjectPreFabs;

	//50 fps
	private float framePeriod = 0.1f;
	private float timeCurrent = -100000.0f;

	private float PI = (float)Math.PI;

	private int startingResource = 40;
	private int resourceIncrease = 3;

	private float charRadius = 0.5f;

	//Board Data
	private float boardHeight = 8.0f;
	private float boardWidth = 5.0f;
	private float halfBoardHeight = 4.0f;
	private float halfBoardWidth = 2.5f;

	//All data for units
	private Unit[] unitData = new Unit[4]; 
	private List<Unit>[] units = new List<Unit>[2];
	private AI[] players = new AI[2];
	private int[] resources = new int[2];
	private int[] ids = new int[2];

	private List<GameObject>[] unitGOs = new List<GameObject>[2];
	void Start () {
		//Load AI
		loadAI ();
		initUnitData ();

		unitGOs [0] = new List<GameObject> ();
		unitGOs [1] = new List<GameObject> ();

		//initalize units
		units [0] = new List<Unit> ();
		units [1] = new List<Unit> ();

		//add nexus
		units [0].Add (createUnit(1, 0, 0.0f, -halfBoardHeight + charRadius, PI/2.0f, 1));
		units [1].Add (createUnit (1, 1, 0.0f, halfBoardHeight - charRadius, 1.5f*PI,1001));

		unitGOs [0].Add (Instantiate(unitObjectPreFabs[0]) as GameObject);
		unitGOs [1].Add (Instantiate(unitObjectPreFabs[1]) as GameObject);

		unitGOs [0] [0].transform.position = new Vector2 (units [0] [0].getX (), units [0] [0].getY ());
		unitGOs [1] [0].transform.position = new Vector2 (units [1] [0].getX (), units [1] [0].getY ());

		unitGOs [0] [0].transform.eulerAngles = new Vector3 (0.0f, 0.0f, units [0] [0].getRotato () * 180.0f / PI);
		unitGOs [1] [0].transform.eulerAngles = new Vector3 (0.0f, 0.0f, units [1] [0].getRotato () * 180.0f / PI);

		//set up resources
		resources [0] = startingResource;
		resources [1] = startingResource;

		//set up ids
		ids [0] = 2;
		ids [1] = 1002;

		//start the game
		timeCurrent = -0.5f;
	}
	
	//Loop
	void Update () {
		//update time
		timeCurrent += Time.deltaTime;

		//check if next frame can be run
		if (timeCurrent < framePeriod) {
			return;
		}

		timeCurrent -= framePeriod;

		//increase resources
		resources [0] += resourceIncrease;
		resources [1] += resourceIncrease;

		//run AI
		List<Command> p0Commands = players [0].loop (resources [0], units [0], units [1]);
		List<Command> p1Commands = players [1].loop (resources [1], units [1], units [0]);

		runCommands (0, p0Commands);
		runCommands (1, p1Commands);

	}

	//Runs commands received from AI Class
	private void runCommands(int player, List<Command> lc)
	{
		//only runs first 100 commands
		int tN = minimum (100, lc.Count);
		for (int i=0; i<tN; i++) {
			if(lc[i].getType() == 1)
			{
				//spawn
				spawn(player, lc[i].getInt(0));
			}
			else if(lc[i].getType() == 2)
			{
				//move
				move(player, lc[i].getSelfID(), lc[i].getFloat(0));
			}
			else if(lc[i].getType() == 3)
			{
				//attack
				//attack(player, lc[i].getSelfID(), lc[i].getEnemyID());
			}
		}
	}

	private void move(int player, int id, float rotato)
	{
		//search for index of ID
		int cIndex = -1;
		for (int i=0; i<units[player].Count; i++) {
			if(units[player][i].getID() == id)
			{
				cIndex = i;
				break;
			}
		}
		if (cIndex == -1) {
			//id not found
			return;
		}

		//tried to move unit on the other team!
		if (player != units [player] [cIndex].getTeam ()) {
			return;
		}

		units [player] [cIndex].setRotato (rotato);
		unitGOs [player] [cIndex].transform.eulerAngles = new Vector3 (0.0f, 0.0f, rotato * 180.0f / PI);
		float newX = units [player] [cIndex].getX () + units [player] [cIndex].getMoveSpeed () * ((float)Math.Cos (rotato));
		float newY = units [player] [cIndex].getY () + units [player] [cIndex].getMoveSpeed () * ((float)Math.Sin (rotato));

		if (newX > halfBoardWidth || newX < -halfBoardWidth || newY > halfBoardHeight || newY < -halfBoardHeight) {
			//out of bounds
			return;
		}

		units [player] [cIndex].setX (newX);
		units [player] [cIndex].setY (newY);

		unitGOs [player] [cIndex].transform.position = new Vector2 (newX, newY);
	}

	private void spawn(int player, int type)
	{
		//calculate spawn location
		float spawnX = units [player] [0].getX();
		float spawnY = units [player] [0].getY() + ((float)Math.Sin (units [player] [0].getRotato())) * 0.5f;
		Unit newUnit;
		//add unit to list
		if (type < 2 || type > 3) {
			//wrong type
			return;
		}
		else
		{
			//not enough resources
			if(resources[player] < unitData[type].getCost())
			{
				return;
			}
			newUnit = createUnit (type, player, spawnX, spawnY, units [player] [0].getRotato (), ids [player]);
		}

		int cIndex = units [player].Count;
		if (cIndex != unitGOs [player].Count) {
			// wtf different number of objects
			return;
		}
		units [player].Add (newUnit);

		//update resources, increment unit id
		resources [player] -= unitData [type].getCost ();
		ids [player] ++;



		//add unit gameObject
		unitGOs [player].Add (Instantiate (unitObjectPreFabs [type * 2 - 2 + player]) as GameObject);

		unitGOs [player] [cIndex].transform.position = new Vector2 (units [player] [cIndex].getX (), units [player] [cIndex].getY ());
		
		unitGOs [player] [cIndex].transform.eulerAngles = new Vector3 (0.0f, 0.0f, units [player] [cIndex].getRotato () * 180.0f / PI);

	}

	//sets up Unit
	private Unit createUnit(int u, int team, float x, float y, float rotato, int id)
	{
		Unit tUnit = new Unit (unitData [u].getType(), unitData [u].getHealth(), unitData [u].getMoveSpeed(), 
		                 unitData [u].getAttackRange() , unitData [u].getAttackDamage(), team, x, y, rotato, id);
		return tUnit;

	}

	//Loads the AI that you choose
	private void loadAI()
	{
		players [0] = new SwarmAI ();
		players [1] = new SwarmAI ();
	}


	//initialize data for units
	private void initUnitData()
	{
		// base
		unitData [1] = new Unit (1, 150, 0.0f, 0.0f, 0, 0, 0.0f, 0.0f, 0.0f, 0);
		unitData [1].setCost (100000);

		//zergling
		unitData [2] = new Unit (3, 50, 0.2f, 0.2f, 10, 0, 0.0f, 0.0f, 0.0f, 0);
		unitData [2].setCost (50);

		// marine
		unitData [3] = new Unit (2, 50, 0.1f, 0.6f, 10, 0, 0.0f, 0.0f, 0.0f, 0);
		unitData [3].setCost (80);

	}
	private int minimum(int a, int b)
	{
		if(a<b)
		{
			return a;
		}
		return b;
	}
}

