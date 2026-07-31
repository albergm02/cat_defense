using UnityEngine;

public class ObjectToProtect : Entity
{
    [Header("Extra Details")]
    private Transform player;
    [SerializeField] private AudioClip[] hurtSoundClips;

    protected override void Awake()
    {
        base.Awake();
        player = FindFirstObjectByType<Player>().transform;
    }
    protected override void Update()
    {
        HandleFlip();
    }
    protected override void HandleFlip()
    {
        if (player == null) 
            return;

        if (player.transform.position.x > transform.position.x && facingRight == false)
            Flip();
        else if (player.transform.position.x < transform.position.x && facingRight == true)
            Flip();
    }

    protected override void Die()
    {
        base.Die();
        GameManager.instance.EnableGameOverUI();
    }

    protected override void TakeDamage(int amount)
    {
        base.TakeDamage(amount); 
        SoundFXManager.Instance.PlayRandomSFXClip(hurtSoundClips, transform, 0.25f);
    }

}
