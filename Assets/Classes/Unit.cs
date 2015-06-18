using UnityEngine;
using System.Collections;

public class Unit {
	private int type = 0;
	private int health = 0;
	private float moveSpeed = 0.0f;
	private float attackRange = 0.0f;
	private int attackDamage = 0;
	private int team = 0;
	private float x = 0.0f;
	private float y = 0.0f;
	private float rotato = 0.0f;

	public Unit(int tType, int tHealth, float tMoveSpeed, float tAttackRange, int tAttackDamage, int tTeam, float tX, float tY, float tRotato)
	{
		type = tType;
		health = tHealth;
		moveSpeed = tMoveSpeed;
		attackRange = tAttackRange;
		attackDamage = tAttackDamage;
		team = tTeam;
		x = tX;
		y = tY;
		rotato = tRotato;
	}

}
