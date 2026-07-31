using UnityEngine;

public class Enemy : Entity
{
    private bool playerDetected;
    private Transform player;

    [Header("Movement Details")]
    [SerializeField] protected float walkSpeed = 2f;

    [Header("Extra Details")]
    [SerializeField] private AudioClip[] damageSoundClips;

    protected override void Awake()
    {
        base.Awake();
        player = FindFirstObjectByType<Player>()?.transform;
    }

    protected override void Update()
    {
        base.Update();
        HandleAttack();
    }
    protected override void HandleMovement()
    {
        if (canMove)
            rb.velocity = new Vector2(facingDir * walkSpeed, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    protected override void HandleFlip()
    {
        if (player == null)
        {
            base.HandleFlip(); 
            return;
        }

        if (player.transform.position.x > transform.position.x && facingRight == false)
            Flip();
        else if (player.transform.position.x < transform.position.x && facingRight == true)
            Flip();
    }


    protected override void HandleAttack()
    {
        if (playerDetected)
        {
            anim.SetTrigger("Attack");
        }

    }

    protected override void HandleCollision()
    {
        base.HandleCollision();
        playerDetected = Physics2D.OverlapCircle(attackPoint.position, attackRadius, whatIsTarget);
    }

    protected override void TakeDamage(int amount)
    {
        base.TakeDamage(amount);
        SoundFXManager.Instance.PlayRandomSFXClip(damageSoundClips, transform, 0.25f);
    }
}