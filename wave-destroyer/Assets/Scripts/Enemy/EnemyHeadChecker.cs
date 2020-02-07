using UnityEngine;

public class EnemyHeadChecker : MonoBehaviour
{
    private CapsuleCollider2D capCollider;

    public bool IsCharacterIn { get; private set; }

    private void Awake()
    {
        capCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Character"))
        {
            IsCharacterIn = true;
        }
    }
}
