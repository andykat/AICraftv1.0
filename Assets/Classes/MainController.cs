using UnityEngine;
using System.Collections;

public class MainController : MonoBehaviour {

	//50 fps
	private float framePeriod = 0.02f;

	//Board Data
	private float boardHeight = 8.0f;
	private float boardWidth = 5.0f;

	//All data for units
	private int unitN = 3;
	private Unit[] unitData = new Unit[4]; 

	void Start () {
		
	}
	

	void Update () {
	
	}

	//initialize data for units
	private void initUnitData()
	{
		// base
		unitData [1] = new Unit (1, 150, 0.0f, 0.0f, 0, 0, 0.0f, 0.0f, 0.0f);

		// marine
		unitData [2] = new Unit (2, 50, 0.1f, 0.6f, 10, 0, 0.0f, 0.0f, 0.0f);

		//zergling
		unitData [3] = new Unit (3, 50, 0.2f, 0.2f, 10, 0, 0.0f, 0.0f, 0.0f);
	}
}

