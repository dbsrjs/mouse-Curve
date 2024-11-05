using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float initialSpeed = 2f;         // �ʱ� �ӵ�
    public float acceleration = 0.1f;       // ���ӵ�
    public float inertiaFactor = 1.5f;      // �浹 ���� �� ��� ���ư��� �Ÿ� ����

    private Vector3 targetPosition;         // ��ǥ ��ġ (���콺 ��ġ)
    private float currentSpeed;             // ���� �ӵ�
    private bool isMoving = true;           // ������ ���� üũ

    void Start()
    {
        currentSpeed = initialSpeed;        // �ӵ� �ʱ�ȭ
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
        // ���콺 ��ġ�� ��ǥ�� ����
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0;

        // ���콺�� ���� �����Ͽ� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

        // �� �����Ӹ��� �ӵ� ���� (���ӵ� ����)
        currentSpeed += acceleration * Time.deltaTime;
    }

    void CheckCollisionWithRay()
    {
        // ���콺 ��ġ���� ������ ���ϴ� Ray�� ����
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Ray�� ��ֹ��� ��Ҵ��� Ȯ��
        if (Physics.Raycast(ray, out hit))
        {
            // �浹 ����� ���� ��ֹ����� Ȯ��
            if (hit.transform == transform)
            {
                // �浹 �� ���� ���� ó��
                isMoving = false;
                Debug.Log("Game Over!");  // ���� ���ӿ����� ���� ���� ȭ�� ��ȯ ���� ó��
            }
        }
        else
        {
            Debug.Log("�浹 �÷���");
            // �浹���� �ʰ� ����ģ ��� ������ ���� �ָ� �̵�
            transform.position += (transform.position - targetPosition).normalized * inertiaFactor * currentSpeed * Time.deltaTime;
        }
    }
}
