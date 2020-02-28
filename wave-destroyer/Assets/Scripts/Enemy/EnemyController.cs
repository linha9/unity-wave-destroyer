using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private float baseSpeed = 100f;
    [SerializeField]
    private float startDirection = 1f;
    [SerializeField]
    private bool isStatic = false;

    private Animator animator;
    private CapsuleCollider2D capCollider;
    private LayerMask groundLayer;
    private EnemyHeadChecker headChecker;
    private Rigidbody2D rb2d;

    private float direction;
    private bool isFacingRight;
    private float speed;

    public static GameObject Create(GameObject prefab, Vector3 position, float initDirection)
    {
        GameObject enemyObject = Instantiate(prefab, position, Quaternion.identity);
        EnemyController enemyController = enemyObject.GetComponent<EnemyController>();
        enemyController.direction = initDirection;
        return enemyObject;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        capCollider = GetComponent<CapsuleCollider2D>();
        groundLayer = 1 << LayerMask.NameToLayer("Ground");
        headChecker = GetComponentInChildren<EnemyHeadChecker>();
        rb2d = GetComponent<Rigidbody2D>();

        direction = startDirection;
        speed = baseSpeed;
        isFacingRight = false;
    }

    private void Update()
    {
        HandleAnimation();
        HandleFlip();
        HandleHeadCollision();
    }

    private void HandleAnimation()
    {
        if (speed > 0f && animator.GetBool("Run") == false)
        {
            animator.SetBool("Run", true);
        }
    }

    private void HandleFlip()
    {
        if (direction == 1f && isFacingRight == false)
        {
            Flip();
            isFacingRight = true;
        }
        else if (direction == -1f && isFacingRight == true)
        {
            Flip();
            isFacingRight = false;
        }
    }

    private void HandleHeadCollision()
    {
        if (headChecker.IsCharacterIn)
        {
            Destroy(gameObject);
            EventHandler.Instance.Invoke(EventType.EnemyDeath);
        }
    }

    private void Flip()
    {
        Vector2 scale = gameObject.transform.localScale;
        gameObject.transform.localScale = new Vector2(-scale.x, scale.y);
    }

    private void FixedUpdate()
    {
        detectWallHit();

        if (!isStatic)
        {
            rb2d.velocity = new Vector2(direction * speed * Time.deltaTime, rb2d.velocity.y);
        }

    }

    private void detectWallHit()
    {
        Vector2 startPoint = new Vector2(capCollider.bounds.min.x - 0.5f, capCollider.bounds.center.y);
        Vector2 endPoint = new Vector2(capCollider.bounds.max.x + 0.5f, capCollider.bounds.center.y);

        RaycastHit2D hit = Physics2D.Linecast(startPoint, endPoint, groundLayer);

        if (hit)
        {
            direction = -direction;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Character"))
        {
            GameManager.Instance.EndGame();
        }
    }
}
