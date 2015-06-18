using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface AI
{
	List<Command> loop(int resources, List<Unit> myUnits, List<Unit> enemyUnits);
}
