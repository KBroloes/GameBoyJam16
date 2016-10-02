using UnityEngine;
using System.Collections;

public class Gunner : MonoBehaviour {

    public Projectile Projectile;

    [Header("Projectiles per Second")]
    public float FiringRate = 0.5f;

    bool canFire;
    float deltaTime;

    Unit unit;

    void Start()
    {
        unit = GetComponent<Unit>();
    }

    void FixedUpdate()
    {
        deltaTime += Time.fixedDeltaTime;

        if(deltaTime >= 1/FiringRate)
        {
            canFire = true;
            deltaTime = 0f;
        }
    }

    void Update()
    {
        if(canFire)
        {
            Projectile p = Instantiate(Projectile);
            Vector2 pos = transform.position;

            // TODO: Do some animation magic and maybe spawn the projectile in front of the worm
            p.transform.position = transform.position;
            if(unit != null)
            {
                // Assign the unit strength for convenience in strength checking
                p.Strength = unit.Strength;
            }
            canFire = false;
        }
    }
}
