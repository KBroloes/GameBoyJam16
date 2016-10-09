using UnityEngine;
using System.Collections;

public class Enemy : Unit {

    [Header("Movement")]
    //Tiles per second
    public float velocity = 0.2f;
    Animator animator;
    
	void Start () {
        animator = GetComponent<Animator>();
	}

    bool move;
    float deltaTime;
    void FixedUpdate()
    {
        deltaTime += Time.fixedDeltaTime;
        if(deltaTime >= 1/velocity)
        {
            move = true;
            deltaTime = 0;
        }
    }

	void Update () {
        HandleMovement();
        if(Life <= 0)
        {
            Destroy(gameObject);
        }
	}
    
    void OnDestroy()
    {
        AIDirector.instance.EnemiesLeft--;
    }

    Coord currentPos;
    int currentNeighbour;
    void HandleMovement()
    {
        currentPos = Coord.From(transform.position);

        // Check collision with neighbouring tile, and move one tile left
        if (move)
        {
            currentNeighbour = Mathf.Clamp(currentPos.x - 1, 0, 9);
            Unit unit = GameManager.instance.GetUnitAt(currentNeighbour, currentPos.y);

            // Detect endgame, last square and no units blocking.
            if (currentPos.x == 0 && unit == null)
            {
                GameManager.instance.EndGame();
            }

            // We have a player unit
            if (unit != null)
            {
                EatNeighbour(unit);
            }
            else
            {
                animator.SetBool("Move", true);
            }
            move = false;
        }
    }

    void Jump()
    {
        transform.position = new Vector2(currentNeighbour + 0.5f, currentPos.y + 0.25f);
        animator.transform.position = transform.position;
    }

    void Land()
    {
        transform.position = new Vector2(currentNeighbour, currentPos.y);
        animator.transform.position = transform.position;
        animator.SetBool("Move", false);
    }

    void EatNeighbour(Unit unit)
    {
        unit.Life -= Strength;
    }
}
