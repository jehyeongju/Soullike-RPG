using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private	KeyCode			jumpKeyCode = KeyCode.Space;
	[SerializeField]
	private	Transform		cameraTransform;
	private	Movement3D		movement3D;
	private	PlayerAnimator	playerAnimator;
	public HealthBar healthBar;
	private StoreControl store;
	public GameObject healEffect;
	public GameObject hitEffect;
	private int PlayerMaxHealth = 100;
	public int PlayerCurrHealth;
	private float DTime = 1f;
	public Text moneytext;
	public Text HpText;
	public Image skill_img;
	public static PlayerController Instance;
	PlayerAttackCollision atkcollsion;
	public int money = 0;
	Rigidbody rigid;
	public bool IsCool = true;	
	public bool check = true;
	bool isDamage;
	private void Awake()
	{
		Cursor.visible	 = false;					// 마우스 커서를 보이지 않게
		Cursor.lockState = CursorLockMode.Locked;   // 마우스 커서 위치 고정
		Instance = this;
		movement3D		= GetComponent<Movement3D>();
		playerAnimator	= GetComponentInChildren<PlayerAnimator>();
		atkcollsion = GetComponent<PlayerAttackCollision>();
		rigid = GetComponent<Rigidbody>();
		store = FindObjectOfType<StoreControl>();
		healthBar = FindObjectOfType<HealthBar>();
		GameStateManager.Instance.OnGameStateChanged += OnGameStateChanged;
	}

    private void Start()
    {
		PlayerCurrHealth = PlayerMaxHealth;
		healthBar.SetMaxHealth(PlayerMaxHealth);
    }

    private void OnDestroy()
    {
		GameStateManager.Instance.OnGameStateChanged -= OnGameStateChanged;
	}
    private void Update()
	{
		healthBar.SetHealth(PlayerCurrHealth);
		if (DTime >= 0)
        {
			DTime -= Time.deltaTime;
			gameObject.tag = "Shield";
        }
        else
        {
			gameObject.tag = "Player";
        }
		// 방향키를 눌러 이동
		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");

		// 애니메이션 파라미터 설정 (horizontal, vertical)
		playerAnimator.OnMovement(x, z);
		// 이동 함수 호출 (카메라가 보고있는 방향을 기준으로 방향키에 따라 이동)
		movement3D.MoveTo(cameraTransform.rotation * new Vector3(x, 0, z));

		// 회전 설정 (항상 앞만 보도록 캐릭터의 회전은 카메라와 같은 회전 값으로 설정)
		transform.rotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);



		if (Input.GetKeyDown(jumpKeyCode))
		{
			
            
				check = false;
				playerAnimator.OnJump();    // 애니메이션 파라미터 설정 (onJump)
				movement3D.JumpTo();        // 점프 함수 호출
				StartCoroutine(WaitForit());
			
		}



		// 마우스 오른쪽 버튼을 누르면 무기 공격 (연계)
		if (Input.GetMouseButtonDown(0))
		{
			check = false;
			playerAnimator.OnWeaponAttack();
			StartCoroutine(WaitForit());
			
		}
		if (Input.GetMouseButtonDown(1))
		{
			check = false;
			playerAnimator.OnWeaponShield();
			DTime = 1;
			
		}
		if(Input.GetKeyDown(KeyCode.Q))
        {
			if(PlayerCurrHealth < 100)
            {
				if (store.HealBuyed == true)
				{
					if(IsCool == true)
                    {
						PlayerCurrHealth += 10;
						healEffect.SetActive(true);
						Invoke("EffectOff", 3);
						IsCool = false;
						StartCoroutine(CoolTime(8f));
						Invoke("Cooltrue", 8);
					}
					
				}
			}
			
        }
		moneytext.text = "$ : " + money;
		HpText.text = "HP : 100/ " + PlayerCurrHealth;
	}
	public void Cooltrue()
    {
		IsCool = true;
    }
	public void healEffectOff()
    {
		healEffect.SetActive(false);
    }

	public void playerDamage(int damage)
	{
		PlayerCurrHealth -= damage;
		healthBar.SetHealth(PlayerCurrHealth);
		playerAnimator.OnDamage();
		hitEffect.SetActive(true);
		Invoke("HitEffectOff", 1);
		if(PlayerCurrHealth <= 0)
        {
			GameOver();
        }
	}
	public void HitEffectOff()
	{
		hitEffect.SetActive(false);
	}

	IEnumerator WaitForit()
    {
		yield return new WaitForSeconds(2f);
		check = true;
    }

	IEnumerator CoolTime(float cool) 
	{
		while (cool > 1.0f)
		{ 
			cool -= Time.deltaTime; 
			skill_img.fillAmount = (1.0f / cool); 
			yield return new WaitForFixedUpdate(); 
		}
		
	}
	


	private void OnGameStateChanged(GameState newGameState)
    {
		enabled = newGameState == GameState.Gameplay;
    }
	private void GameOver()
    {
		SceneManager.LoadScene("GameOver");
		Cursor.visible = true;                
		Cursor.lockState = CursorLockMode.None;
	}
}

