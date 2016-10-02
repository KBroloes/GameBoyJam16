using UnityEngine;
using System.Collections;

public class Enemy : Unit {

    [Header("Movement")]
    //Tiles per second
    public float velocity = 0.5f;

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
	
	void Update () {
	    // Check collision with neighbouring tile, and move one tile left
	}
}
