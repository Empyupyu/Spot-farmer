using UnityEngine;
using UnityEngine.AI;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerData playerData;

    private Joystick joystick;
    private Vector3 moveDirection;
    private Quaternion cameraAngle;

    private void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        cameraAngle = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0); 
    }

    private void Update()
    {
        if (!HasInput())
        {
            MoveAnimation(0);
            return;
        }

        UpdateMoveDirection();
        Move();
        RotateToMoveDirection();
    }


    private bool HasInput()
    {
        if (joystick.Horizontal == 0 && joystick.Vertical == 0) return false;

        return true;
    }

    private void UpdateMoveDirection()
    {
        moveDirection = cameraAngle * new Vector3(joystick.Direction.x, 0, joystick.Direction.y);
    }

    private void Move()
    {
        navMeshAgent.Move(moveDirection.normalized * playerData.MovmentSpeed * Time.deltaTime);
        MoveAnimation(1);
    }

    private void MoveAnimation(float value)
    {
        if(animator == null) return;

        animator.SetFloat("Move", value);
    }

    private void RotateToMoveDirection()
    {
        transform.rotation = Quaternion.LookRotation(moveDirection);
    }
}
