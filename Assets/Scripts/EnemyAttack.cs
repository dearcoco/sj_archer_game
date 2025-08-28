using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("프리팹/포인트")]
    public EnemyProjectile projectilePrefab;
    public Transform firePoint;               // 있으면 이 위치에서, 없으면 오프셋 사용
    public Vector2 fireOffset = new Vector2(-0.6f, 0.3f); // 적 몸 기준 왼쪽/약간 위

    [Header("쿨타임")]
    public float attackInterval = 1.5f;

    [Header("타겟")]
    public Transform player; // 없으면 Start에서 태그로 찾음

    float timer;

    void Start()
    {
        if (!player)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p) player = p.transform;
        }
    }

    void Update()
    {
        if (!player) return;

        timer += Time.deltaTime;
        if (timer >= attackInterval)
        {
            FireOnce();
            timer = 0f;
        }
    }

    public void FireOnce()
    {
        if (!projectilePrefab) return;

        // 시작점: firePoint 있으면 그 위치, 없으면 오프셋로 계산
        Vector3 start = firePoint ? firePoint.position
                                  : transform.position + (Vector3)fireOffset;

        var proj = Instantiate(projectilePrefab, start, Quaternion.identity);
        var enemyCol = GetComponent<Collider2D>();
        proj.Init(start, player.position, enemyCol);
    }
}
