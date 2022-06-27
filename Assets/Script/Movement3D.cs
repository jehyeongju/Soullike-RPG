using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed = 5;        // 이동 속도
	[SerializeField]
	private float gravity = -9.81f; // 중력 계수
	[SerializeField]
	private float jumpForce = 3.0f; // 뛰어 오르는 힘
	private Vector3 moveDirection;      // 이동 방향

	private CharacterController characterController;

	PlayerAnimator playerAnim;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		playerAnim = GetComponent<PlayerAnimator>();
	}

	private void Update()
	{
		// 중력 설정. 플레이어가 땅을 밟고 있지 않다면
		// y축 이동방향에 gravity * Time.deltaTime을 더해준다
		if (characterController.isGrounded == false)
		{
			moveDirection.y += gravity * Time.deltaTime;
		}

		// 이동 설정. CharacterController의 Move() 함수를 이용한 이동
		characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

		Run();
	}

	public void MoveTo(Vector3 direction)
	{
		moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
	}

	public void JumpTo()
	{
		// 캐릭터가 바닥을 밟고 있으면 점프
		if (characterController.isGrounded == true)
		{
			moveDirection.y = jumpForce;
		}
	}
	
	private void Run()
    {
		if (Input.GetKey(KeyCode.LeftShift))
		{
			moveSpeed = 7;
			playerAnim.OnRun();
		}
		else
        {
			moveSpeed = 5;
			playerAnim.OffRun();
		}
	}
    private void OnGUI()
    {
		var labelStyle = new GUIStyle();
		labelStyle.fontSize = 50;
		labelStyle.normal.textColor = Color.white;
		GUI.Label(new Rect(0, Screen.height - 50, 100, 100), "현재속도 : " + moveSpeed,labelStyle);
	}
}
