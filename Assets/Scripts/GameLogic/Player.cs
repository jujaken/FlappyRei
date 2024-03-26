using UnityEngine;
using UnityEngine.Events;

namespace Scripts.GameLogic
{
    public class Player : MonoBehaviour
    {
        [SerializeField] UnityEvent onDead;
        [SerializeField] UnityEvent onJump;

        [SerializeField] float jumpHeight = 1f;
        [SerializeField] Sprite[] sprites;

        public bool IsActive { get; private set; }
        public bool IsDead { get; private set; }

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;

        private float defaultGravityScale;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();

            defaultGravityScale = rb.gravityScale;
            StopDown();
        }

        void FixedUpdate()
        {
            if (!IsActive || IsDead) return;

            if (Input.GetButtonDown("Jump"))
                Jump();

            if (rb.velocity.y > 0)
                spriteRenderer.sprite = sprites[1]; // eye closed
            else
                spriteRenderer.sprite = sprites[0]; // eye open
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Finish"))
                return;

            if (IsDead) return;

            IsDead = true;
            spriteRenderer.sprite = sprites[2]; // cry sprite

            onDead?.Invoke();
        }

        // TODO: улучшить бы 
        public void Jump()
        {
            if (!IsActive || IsDead) return;
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            onJump?.Invoke();
        }

        public void StartDown()
        {
            IsActive = true;
            rb.gravityScale = defaultGravityScale;
        }

        public void StopDown()
        {
            IsActive = false;
            rb.gravityScale = 0;
        }
    }
}