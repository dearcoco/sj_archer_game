using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public float moveSpeed = 2f;      // 이동 속도
    public Transform moveZone;        // 이동 영역 스프라이트 (빈 오브젝트 가능)

    private float leftLimit;
    private float rightLimit;
    private bool movingRight = true;

    void Start()
    {
        if (moveZone != null)
        {
            // 이동 영역 SpriteRenderer 크기 기준
            SpriteRenderer sr = moveZone.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                float halfWidth = sr.bounds.size.x / 2f;
                leftLimit = moveZone.position.x - halfWidth;
                rightLimit = moveZone.position.x + halfWidth;
            }
            else
            {
                // SpriteRenderer 없으면 그냥 위치 기준 +2, -2
                leftLimit = moveZone.position.x - 2f;
                rightLimit = moveZone.position.x + 2f;
            }
        }
        else
        {
            // moveZone 안 넣으면 기본값
            leftLimit = transform.position.x - 2f;
            rightLimit = transform.position.x + 2f;
        }
    }

    void Update()
    {
        if (movingRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x >= rightLimit)
                movingRight = false;
        }
        else
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if (transform.position.x <= leftLimit)
                movingRight = true;
        }
    }
}
