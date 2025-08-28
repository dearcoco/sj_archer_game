using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float duration = 1.5f;
    public float lifetime = 2f;
    public float scaleY = 0.6f;   // Y축 압축 비율

    public int damage = 20;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 center;
    private float radius;
    private float time;
    private float startAngleRad;
    private float endAngleRad;



    public void Init(Vector3 start, Vector3 end, Collider2D playerCol)
    {
        startPos = start;
        endPos = end;

        // ✅ 플레이어 충돌 무시
        Collider2D arrowCol = GetComponent<Collider2D>();
        if (arrowCol != null && playerCol != null)
            Physics2D.IgnoreCollision(arrowCol, playerCol);

        // ✅ X축 거리만 반지름 계산
        float distX = Mathf.Abs(endPos.x - startPos.x);
        radius = distX / 2f;

        // ✅ 중심은 X축 중점, Y는 시작점 높이
        center = new Vector3((startPos.x + endPos.x) / 2f, startPos.y, 0f);

        // 시작/끝 각도 (항상 위쪽 반원)
        startAngleRad = Mathf.PI; // 왼쪽
        endAngleRad = 0f;       // 오른쪽

        if (endPos.x < startPos.x)
        {
            // 왼쪽으로 쏠 때 반전
            startAngleRad = 0f;
            endAngleRad = Mathf.PI;
        }

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        time += Time.deltaTime;
        float t = time / duration;
        if (t > 1f) return;

        float theta = Mathf.Lerp(startAngleRad, endAngleRad, t);

        // ✅ 항상 X축 기준 반원 (y는 위쪽으로만)
        float x = center.x + radius * Mathf.Cos(theta);
        float y = center.y + (radius * Mathf.Sin(theta) * scaleY);

        transform.position = new Vector3(x, y, 0f);

        // 진행 방향 회전
        Vector2 dir = new Vector2(-Mathf.Sin(theta), Mathf.Cos(theta) * scaleY);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health hp = collision.GetComponent<Health>();
        if (hp != null)
        {
            hp.TakeDamage(damage);
            Destroy(gameObject); // 화살은 한 번 맞추면 사라짐
        }
    }

}
