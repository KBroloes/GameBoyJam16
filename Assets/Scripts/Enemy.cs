﻿using UnityEngine;
using System.Collections;

public class Enemy : Unit {

    [Header("Movement")]
    //Tiles per second
    public float velocity = 0.2f;


    void Move()
    {
        // Play move animation
    }

    void Attack()
    {
        // Play move animation sans jump
    }

	void Start () {
	
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
        AIDirector.instance.enemies--;
    }

    void HandleMovement()
    {
        Coord currentPos = Coord.From(transform.position);

        // Check collision with neighbouring tile, and move one tile left
        if (move)
        {
            int neighbour = Mathf.Clamp(currentPos.x - 1, 0, 9);
            Unit unit = GameManager.instance.GetUnitAt(neighbour, currentPos.y);

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
                // TODO: Activate animation, and lerp movement
                transform.position = new Vector2(neighbour, currentPos.y);
            }

            move = false;
        }
    }

    void EatNeighbour(Unit unit)
    {
        unit.Life -= Strength;
    }
}
