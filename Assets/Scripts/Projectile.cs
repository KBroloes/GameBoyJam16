using UnityEngine;
using System.Collections;

public class Projectile : Unit {

    [Header("Movement")]
    // Tiles per second
    public float velocity = 0.5f;

    bool move;
    float deltaTime;
    void FixedUpdate()
    {
        deltaTime += Time.fixedDeltaTime;
        if (deltaTime >= 1 / velocity)
        {
            move = true;
            deltaTime = 0;
        }
    }

    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        Coord currentPos = Coord.From(transform.position);

        // Check collision with neighbouring tile, and move one tile left
        if (move)
        {
            int neighbour = Mathf.Clamp(currentPos.x + 1, 0, 10);
            
            // Detect end of lane, last square and no enemies.
            if (currentPos.x == 10)
            {
                Destroy(gameObject);
            }
            
            //Activate animation, and move
            transform.position = new Vector2(neighbour, currentPos.y);

            move = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        GameObject other = col.gameObject;
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            Damage(enemy);
            Destroy(gameObject);

            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlayOnce(SoundType.Hit);
            }
        }
    }

    void Damage(Enemy enemy)
    {
        enemy.Life -= Strength;
        Animator enemyAnimator = enemy.gameObject.GetComponent<Animator>();
        enemyAnimator.SetTrigger("Hit");
    }
}
