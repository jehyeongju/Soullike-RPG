using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
	private	Animator animator;
	public Transform target;
	
	private int maxHealth = 100;
	public int curHealth;
	public bool isChase;
	public bool isAttack;
	public BoxCollider meleeArea;
	public GameObject HitEffect;

	private EnemySpawn enemySpawn;

	private bool isDead = false;
	Rigidbody rigid;
	NavMeshAgent nav;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody>();
		nav = GetComponent<NavMeshAgent>();
		enemySpawn = FindObjectOfType<EnemySpawn>();
		enemySpawn.SendMessage("PlusEnemy");
		Invoke("ChaseStart", 1);
	}

    private void Start()
    {
		target = FindObjectOfType<PlayerAnimator>().transform;
		curHealth = maxHealth;
    }
    void FreezeVelocity()
    {
        if (isChase)
        {
			rigid.velocity = Vector3.zero;
			rigid.angularVelocity = Vector3.zero;
		}
		

    }
	void Targeting()
    {
		float targetradius = 1.5f;
		float targetRange = 1.5f;
		RaycastHit[] rayHits =
			Physics.SphereCastAll(transform.position, targetradius, transform.forward, targetRange, LayerMask.GetMask("Player"));
		if(rayHits.Length > 0 && !isAttack)
        {
			StartCoroutine(Attack());
        }
    }

	IEnumerator Attack()
    {
		isChase = false;
		isAttack = true;
		animator.SetBool("onAttack", true);
		yield return new WaitForSeconds(0.1f);
		animator.SetBool("onAttack", false);

		yield return new WaitForSeconds(1f);
		meleeArea.enabled = true;


		yield return new WaitForSeconds(0.1f);
		meleeArea.enabled = false;

		isChase = true;
		isAttack = false;
    }
	void FixedUpdate()
    {
		FreezeVelocity();
    }
	void ChaseStart()
	{
		isChase = true;
		animator.SetBool("isRun", true);
	}

    public void TakeDamage(int damage)
	{
		curHealth -= damage;
		animator.SetTrigger("OnHIt");
	}
    private void Update()
    {
        if (nav.enabled)
        {
			nav.SetDestination(target.position);
			nav.isStopped = !isChase;
		}

		Targeting();
		 
		Die();
    }
	
	public void HitEffectOff()
    {
		HitEffect.SetActive(false);
    }
	private void Die()
    {
		if(!isDead)
        {
			if (curHealth <= 0)
			{
				isDead = true;
				isChase = false;
				nav.enabled = false;
				animator.SetTrigger("Die");
				PlayerController.Instance.money += 250;
				Destroy(gameObject, 1.3f);
				OrcScore.orcKilled += 1;
				enemySpawn.SendMessage("MinusEnemy");
			}
		}
		
	}
	
}

