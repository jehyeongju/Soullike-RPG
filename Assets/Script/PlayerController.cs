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
	

	private int PlayerMaxHealth = 100;
	public int PlayerCurrHealth;

	private float DTime = 1f;
	public static PlayerController Instance;
	PlayerAttackCollision atkcollsion;
	public int money = 0;
	Rigidbody rigid;
	public bool check = true;
	public bool InAttack = false;
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
		if(DTime >= 0)
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
		
	}
	public void playerDamage(int damage)
	{
		PlayerCurrHealth -= damage;
		healthBar.SetHealth(PlayerCurrHealth);
		playerAnimator.OnDamage();
		if(PlayerCurrHealth <= 0)
        {
			GameOver();
        }
	}
	
	public void InAttackFalse()
    {
		InAttack = false;
    }

	IEnumerator WaitForit()
    {
		yield return new WaitForSeconds(2f);
		check = true;
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

