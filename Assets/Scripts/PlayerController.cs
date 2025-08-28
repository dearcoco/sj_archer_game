using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; 
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator anim;
    public PlayerAttack attack;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        if (attack == null)
        {
            Debug.LogError("❌ PlayerAttack 컴포넌트를 Player에서 찾지 못했습니다!");
        }
        else
        {
            Debug.Log("✅ PlayerAttack 연결 완료");
        }
    }

    void Update()
    {
        float move = Input.GetAxisRaw("Horizontal"); 
        rb.linearVelocity = new Vector2(move * moveSpeed, rb.linearVelocity.y);

        // 좌우 방향 전환
        if (move > 0) sr.flipX = false;
        if (move < 0) sr.flipX = true;

        // 애니메이션 전환
        if (move != 0) anim.SetBool("isRunning", true);
        else anim.SetBool("isRunning", false);
    }
}
