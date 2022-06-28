using System.Collections;
using UnityEngine;

public class PlayerAttackCollision : MonoBehaviour
{
	PlayerController playerctrl;
	public int AttackDamage = 10;
	public static PlayerAttackCollision Inst;

	private void Start()
    {
		Inst = this;
		playerctrl = GetComponent<PlayerController>();
    }
    private void OnEnable()
	{
		StartCoroutine("AutoDisable");
	}

	private void OnTriggerEnter(Collider other)
	{
		// 플레이어가 타격하는 대상의 태그, 컴포넌트, 함수는 바뀔 수 있다
		if ( other.gameObject.CompareTag("Enemy") ==true )
		{
			other.GetComponent<EnemyController>().TakeDamage(AttackDamage);
			other.GetComponent<EnemyController>().HitEffect.SetActive(true);
			other.GetComponent<EnemyController>().Invoke("HitEffectOff", 0.5f);
			
		}
		else if(other.gameObject.CompareTag("Boss") == true)
        {
			other.GetComponent<BossController>().TakeDamage(AttackDamage);
			other.GetComponent<BossController>().HitEffect.SetActive(true);
			other.GetComponent<BossController>().Invoke("HitEffectOff", 0.5f);
		}
	}

	private IEnumerator AutoDisable()
	{
		// 0.1초 후에 오브젝트가 사라지도록 한다
		yield return new WaitForSeconds(0.1f);

		gameObject.SetActive(false);
	}

    void OnCollisionEnter(Collision collision)
    {
        
    }
}

