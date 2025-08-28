using UnityEngine;

public class Bow : Weapon
{
    public GameObject arrowPrefab;
    public Transform firePoint;

    public override void Attack(Vector3 targetPos)
    {
        // 화살 생성
        GameObject arrow = Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);

        // Arrow 초기화 (플레이어 Collider 전달)
        Arrow arrowScript = arrow.GetComponent<Arrow>();
        Collider2D playerCol = GetComponentInParent<Collider2D>(); // Player의 콜라이더 찾기

        if (arrowScript != null)
        {
            arrowScript.Init(firePoint.position, targetPos, playerCol);
        }
    }
}
