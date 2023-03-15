using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // canvas-related fields
    [SerializeField]
    protected Vector3 canvasOffsets = new Vector3(0, 1.0f, 0.7f);

    // Movement-related fields
    protected Rigidbody rb;
    protected Vector3 moveDirection;
    [SerializeField]
    protected float baseMoveSpeed;
    protected float moveSpeed;

    protected abstract Vector3 GetMoveDirection();
    protected abstract void Die();

    // Health-related fields
    [SerializeField]
    protected int baseMaxHealth = 100;
    protected int maxHealth;
    protected int health;

    // Audio
    AudioSource audioSource;
    [SerializeField]
    AudioClip damageAudio;

    // Damage feedback
    MeshRenderer meshRenderer;
    Color baseColor;
    [SerializeField]
    float damageFeedbackDuration = 0.05f;
    [SerializeField]
    Color damageColor = Color.white;

    protected virtual void Start(){
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        baseColor = meshRenderer.material.color;
    }

    protected void setBaseStats() {
        moveSpeed = baseMoveSpeed;
        maxHealth = baseMaxHealth;
        health = maxHealth;
    }

    public Vector3 GetCanvasOffsets()
    {
        return canvasOffsets;
    }

    // Moves the object instance
    protected void Move()
    {
        rb.velocity = moveDirection.normalized * moveSpeed;
    }

    public virtual void Damage(int value) {
        audioSource.PlayOneShot(damageAudio);
        StartCoroutine(DamageFeedback());
        health = Mathf.Max(health - value, 0);
    }

    private IEnumerator DamageFeedback(){
        meshRenderer.material.color = damageColor;
        yield return new WaitForSeconds(damageFeedbackDuration);
        meshRenderer.material.color = baseColor;
    }

    public void Heal(int value) {
        health = Mathf.Min(health + value, maxHealth);
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public void AddMaxHealth(int value) {
        maxHealth += value;
    }

    public int GetCurrentHealth() {
        return health;
    }

    public void AddMovementSpeed(float value) {
        moveSpeed += value;
    }
}
