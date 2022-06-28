using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
	public int MonsterDamage = 5;
	private void Start()
	{
		
		
	}
	private void OnEnable()
	{
		StartCoroutine("AutoDisable");
	}

	private void OnTriggerEnter(Collider other)
	{
		// 플레이어가 타격하는 대상의 태그, 컴포넌트, 함수는 바뀔 수 있다
		if (other.gameObject.CompareTag("Player") == true)
		{
			if(other.GetComponent<PlayerController>()!=null)
			other.GetComponent<PlayerController>().playerDamage(MonsterDamage);
		}
	}

	private IEnumerator AutoDisable()
	{
		// 0.1초 후에 오브젝트가 사라지도록 한다
		yield return new WaitForSeconds(1f);

		gameObject.SetActive(false);
	}

	
}
