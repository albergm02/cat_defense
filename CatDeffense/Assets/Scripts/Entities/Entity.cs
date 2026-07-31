using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{

    protected Animator anim;
    protected Rigidbody2D rb;
    protected Collider2D col;
    protected SpriteRenderer sr;

    [Header("Health Details")]
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private int currentHealth;
    [SerializeField] private Material damageMaterial;
    [SerializeField] private float damageFeedBackDuration = .2f;
    private Coroutine damageFeedBackCoroutine;

    [Header("Attack Details")]
    [SerializeField] protected float attackRadius;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] protected int attackDamage; 

    [Header("Collision Details")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGrounded;
    protected bool isGrounded;

    [Header("UI Sprite Health Bar")]
    [SerializeField] private HealthBar spriteHealthBarUI;


    [SerializeField] protected bool facingRight = true;
    protected int facingDir = 1;
    protected bool canMove = true;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();

        sr.material = new Material(sr.sharedMaterial);
        currentHealth = maxHealth;

        if (spriteHealthBarUI != null)
            spriteHealthBarUI.SetHealth(currentHealth, maxHealth);

    }

    protected virtual void Update()
    {
        HandleCollision();
        HandleAnimations();
        HandleFlip();
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
    }

    /* -------------------- MOVIMIENTO DEL JUGADOR ----------------------- */
    protected virtual void HandleMovement()
    {
        
    }
    public virtual void EnableMovement(bool enable)
    {
        canMove = enable;
    }

    /* -------------------- ANIMACIONES ----------------------- */
    protected void HandleAnimations()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("IsGrounded", isGrounded);

    }

    /* -------------------- COLISIONES ----------------------- */
    protected virtual void HandleCollision() 
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGrounded);
    }

    /* -------------------- GIROS ----------------------- */
    protected virtual void HandleFlip()
    {
        if (rb.velocity.x > 0 && facingRight == false)
            Flip();
        else if (rb.velocity.x < 0 && facingRight == true)
            Flip();

    }

    public void Flip()
    {
        transform.Rotate(0, 180, 0);
        facingRight = !facingRight;
        facingDir = facingDir * -1;
    }

    /* -------------------- ATAQUE ----------------------- */
    protected virtual void HandleAttack()
    {
        if (isGrounded)
            anim.SetTrigger("Attack");
          
    }

    public void DamageTargets()
    {
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, whatIsTarget);
        foreach (Collider2D enemy in enemyColliders)
        {
            Entity entityTarget = enemy.GetComponent<Entity>();
            if (entityTarget != null)
            {
                entityTarget.TakeDamage(attackDamage);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -groundCheckDistance));
        if (attackPoint != null)
            Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    /* -------------------- DAÑOS ----------------------- */

    protected virtual void TakeDamage(int amount)
    {
        currentHealth -= amount; 
        PlayDamageFeedBack();

        if (spriteHealthBarUI != null)
            spriteHealthBarUI.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void PlayDamageFeedBack()
    {
        if (damageFeedBackCoroutine != null)
            StopCoroutine(damageFeedBackCoroutine);

        damageFeedBackCoroutine = StartCoroutine(DamageFeedBackCoroutine());
    }
    private IEnumerator DamageFeedBackCoroutine()
    {
        Material originalMaterial = sr.material;
        sr.material = damageMaterial;

        yield return new WaitForSeconds(damageFeedBackDuration);

        sr.material = originalMaterial;
    }

    /* -------------------- MUERTE ----------------------- */

    protected virtual void Die()
    {
        anim.enabled = false;
        col.enabled = false;

        rb.gravityScale = 12;
        rb.velocity = new Vector2(rb.velocity.x, 15);

        Destroy(gameObject, 3);
    }

}
