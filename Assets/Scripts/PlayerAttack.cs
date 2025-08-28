using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Weapon equippedWeapon;  // 현재 장착 무기 (Inspector에 Bow 드래그)
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭
        {
            Vector3 mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            if (equippedWeapon != null)
                equippedWeapon.Attack(mouseWorldPos);
        }
    }
}
