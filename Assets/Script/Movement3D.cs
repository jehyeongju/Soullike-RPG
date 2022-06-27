using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement3D : MonoBehaviour
{
	[SerializeField]
	private float moveSpeed = 5;        // �̵� �ӵ�
	[SerializeField]
	private float gravity = -9.81f; // �߷� ���
	[SerializeField]
	private float jumpForce = 3.0f; // �پ� ������ ��
	private Vector3 moveDirection;      // �̵� ����

	private CharacterController characterController;

	PlayerAnimator playerAnim;

	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
		playerAnim = GetComponent<PlayerAnimator>();
	}

	private void Update()
	{
		// �߷� ����. �÷��̾ ���� ��� ���� �ʴٸ�
		// y�� �̵����⿡ gravity * Time.deltaTime�� �����ش�
		if (characterController.isGrounded == false)
		{
			moveDirection.y += gravity * Time.deltaTime;
		}

		// �̵� ����. CharacterController�� Move() �Լ��� �̿��� �̵�
		characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

		Run();
	}

	public void MoveTo(Vector3 direction)
	{
		moveDirection = new Vector3(direction.x, moveDirection.y, direction.z);
	}

	public void JumpTo()
	{
		// ĳ���Ͱ� �ٴ��� ��� ������ ����
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
		GUI.Label(new Rect(0, Screen.height - 50, 100, 100), "����ӵ� : " + moveSpeed,labelStyle);
	}
}
