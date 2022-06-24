using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
	public GameObject fistCollision;
	public void MonsterCollision()
	{
		fistCollision.SetActive(true);
	}
}
