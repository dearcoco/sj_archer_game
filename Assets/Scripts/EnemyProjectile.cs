using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Header("궤적/수명/데미지")]
    public float duration = 1.2f;   // 곡선 이동 시간
    public float lifetime = 2.5f;   // 자동 삭제 시간
    public float scaleY = 0.8f;     // 납작 아치용
    public int damage = 10;

    // 내부 상태
    private Vector3 startPos, endPos, center;
    private float time, radius;
    private float startAngleRad, endAngleRad;

    public void Init(Vector3 start, Vector3 end, Collider2D ownerCol)
    {
        startPos = start;
        endPos   = end;

        // 소유자(적)와 충돌 무시
        var myCol = GetComponent<Collider2D>();
        if (myCol && ownerCol) Physics2D.IgnoreCollision(myCol, ownerCol);

        // X축 거리 기준 반원 (항상 위쪽 아치)
        float distX = Mathf.Abs(endPos.x - startPos.x);
        radius = distX * 0.5f;

        center = new Vector3((startPos.x + endPos.x) * 0.5f, startPos.y, 0f);

        // 오른쪽→왼쪽(적이 오른쪽에 있고 왼쪽으로 쏘는 경우가 기본)
        // end.x < start.x 이면 0→pi, 아니면 pi→0
        if (endPos.x < startPos.x) { startAngleRad = 0f;           endAngleRad = Mathf.PI; }
        else                       { startAngleRad = Mathf.PI;     endAngleRad = 0f;       }

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        time += Time.deltaTime;
        float t = time / duration;
        if (t > 1f) return;

        float theta = Mathf.Lerp(startAngleRad, endAngleRad, t);

        float x = center.x + radius * Mathf.Cos(theta);
        float y = center.y + (radius * Mathf.Sin(theta) * scaleY);

        transform.position = new Vector3(x, y, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var hp = other.GetComponent<Health>();
            if (hp) hp.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
