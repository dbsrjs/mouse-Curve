using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float initialSpeed = 2f;         // 초기 속도
    public float acceleration = 0.1f;       // 가속도
    public float inertiaFactor = 1.5f;      // 충돌 실패 시 계속 날아가는 거리 조정

    private Vector3 targetPosition;         // 목표 위치 (마우스 위치)
    private float currentSpeed;             // 현재 속도
    private bool isMoving = true;           // 움직임 여부 체크

    void Start()
    {
        currentSpeed = initialSpeed;        // 속도 초기화
    }

    void Update()
    {
        if (isMoving)
        {
            MoveTowardsMouse();
            CheckCollisionWithRay();
        }
    }

    void MoveTowardsMouse()
    {
        // 마우스 위치를 목표로 설정
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;

        // 마우스를 향해 가속하여 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

        // 매 프레임마다 속도 증가 (가속도 적용)
        currentSpeed += acceleration * Time.deltaTime;
    }

    void CheckCollisionWithRay()
    {
        // 마우스 위치에서 앞으로 향하는 Ray를 생성
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Ray가 장애물에 닿았는지 확인
        if (Physics.Raycast(ray, out hit))
        {
            // 충돌 대상이 현재 장애물인지 확인
            if (hit.transform == transform)
            {
                // 충돌 시 게임 종료 처리
                isMoving = false;
                Debug.Log("Game Over!");  // 실제 게임에서는 게임 오버 화면 전환 등을 처리
            }
        }
        else
        {
            Debug.Log("충돌 시류ㅐ");
            // 충돌하지 않고 지나친 경우 관성에 따라 멀리 이동
            transform.position += (transform.position - targetPosition).normalized * inertiaFactor * currentSpeed * Time.deltaTime;
        }
    }
}
