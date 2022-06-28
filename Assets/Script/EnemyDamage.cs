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
		// �÷��̾ Ÿ���ϴ� ����� �±�, ������Ʈ, �Լ��� �ٲ� �� �ִ�
		if (other.gameObject.CompareTag("Player") == true)
		{
			if(other.GetComponent<PlayerController>()!=null)
			other.GetComponent<PlayerController>().playerDamage(MonsterDamage);
		}
	}

	private IEnumerator AutoDisable()
	{
		// 0.1�� �Ŀ� ������Ʈ�� ��������� �Ѵ�
		yield return new WaitForSeconds(1f);

		gameObject.SetActive(false);
	}

	
}
