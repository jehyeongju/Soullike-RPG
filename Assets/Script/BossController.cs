using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossController : MonoBehaviour
{
	private	Animator animator;
	public Transform target;
	
	public int maxHealth = 600;
	public int currHealth;
	public bool isChase;
	public static BossController Instance;
	public bool isAttack;
	public BoxCollider meleeArea;
	public GameObject HitEffect;
	private EnemySpawn enemySpawn;

	private BossHealthBar bossHealthBar;
	private bool isDead = false;
	Rigidbody rigid;
	BoxCollider boxCollider;
	NavMeshAgent nav;

	PlayerController playercont;
	private void Awake()
	{
		bossHealthBar = FindObjectOfType<BossHealthBar>();
		animator = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody>();
		Instance = this;
		boxCollider = GetComponent<BoxCollider>();
		nav = GetComponent<NavMeshAgent>();
		playercont = GetComponent<PlayerController>();
		enemySpawn = FindObjectOfType<EnemySpawn>();
		enemySpawn.SendMessage("PlusEnemy");
		Invoke("ChaseStart", 1);
	}

    private void Start()

    {
		target = FindObjectOfType<PlayerAnimator>().transform;
		
		currHealth = maxHealth;
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
		float targetradius = 5f;
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
		currHealth -= damage;
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
			if (currHealth <= 0)
			{
				isDead = true;
				isChase = false;
				nav.enabled = false;
				animator.SetTrigger("Die");
				PlayerController.Instance.money += 5000;
				Destroy(gameObject, 1.3f);
				GameClear();
				OrcScore.orcKilled += 1;
			}
		}
		
	}

	public void GameClear()
    {
		SceneManager.LoadScene("GameClear");
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
	
}

