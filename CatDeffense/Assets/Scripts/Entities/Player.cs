using UnityEngine;

public class Player : Entity
{
    private float xInput;

    [Header("Movement Details")]
    [SerializeField] protected float walkSpeed = 2f;
    [SerializeField] private float runSpeed = 5f;
    [SerializeField] private float jumpForce = 8;
    private bool canJump = true;

    [Header("Extra Details")]
    [SerializeField] private AudioClip[] attackSoundClips;
    [SerializeField] private AudioClip hurtSoundClip;

    [Header("Footstep Details")]
    [SerializeField] private AudioClip footstepClip;
    private float footstepInterval = 0.4f; 
    private float footstepTimer = 0f;


    protected override void Update()
    {
        base.Update();
        HandleInput();
        HandleFootsteps();
    }

    /* -------------------- MOVIMIENTO DEL JUGADOR ----------------------- */ 

    protected override void HandleMovement()
    {
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

        if (canMove)
            rb.velocity = new Vector2(xInput * speed, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y);
    }

    private void TryToJump()
    {
        if (isGrounded && canJump)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            TryToJump();

        if (Input.GetKeyDown(KeyCode.Mouse0))
            HandleAttack();
    }

    protected override void HandleFlip()
    {
        if (xInput > 0 && facingRight == false)
            Flip();
        else if (xInput < 0 && facingRight == true)
            Flip();
    }

    private void HandleFootsteps()
    {
        float currentSpeed = Mathf.Abs(rb.velocity.x);

        if (isGrounded && currentSpeed > 0.1f)
        {
            footstepTimer -= Time.deltaTime;

            if (footstepTimer <= 0f)
            {
                SoundFXManager.Instance.PlaySFXClip(footstepClip, transform, 0.1f);
                float interval = Input.GetKey(KeyCode.LeftShift) ? footstepInterval / 1.5f : footstepInterval;
                footstepTimer = interval;
            }
        }
        else
        {
            footstepTimer = 0f;
        }
    }

    public override void EnableMovement(bool enable)
    {
        base.EnableMovement(enable);
        canJump = enable;
    }

    /* -------------------- MUERTE Y FIN DEL JUEGO ----------------------- */

    protected override void Die()
    {
        base.Die();
        GameManager.instance.EnableGameOverUI();
    }

    /* -------------------- ATAQUE Y DAÑO -------------------------------- */

    protected override void HandleAttack()
    {
        if (isGrounded)
        {
            anim.SetTrigger("Attack");
            SoundFXManager.Instance.PlayRandomSFXClip(attackSoundClips, transform, 0.5f);
        }
    }

    protected override void TakeDamage(int amount) 
    {
        base.TakeDamage(amount);
        SoundFXManager.Instance.PlaySFXClip(hurtSoundClip, transform, 0.5f);
    }
}
