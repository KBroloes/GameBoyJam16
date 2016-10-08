using UnityEngine;
using System.Collections;

public class Gunner : MonoBehaviour {

    public Projectile Projectile;

    [Header("Projectiles per Second")]
    public float FiringRate = 0.5f;
    public int range = 10;

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
        if(canFire && DetectEnemy())
        {
            Projectile p = Instantiate(Projectile);

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

    bool DetectEnemy()
    {
        Vector2 rayCastFrom = transform.position;
        rayCastFrom.y -= 0.5f; //Account for y position being top of coordinate and will detect two lanes at once.

        RaycastHit2D[] hits = Physics2D.RaycastAll(rayCastFrom, new Vector2(1, 0), range);
        Debug.DrawRay(transform.position, new Vector2(1, 0), Color.red, range);
        foreach(RaycastHit2D hit in hits)
        {
            if(hit.collider.gameObject.tag == "Enemy")
            {
                return true;
            }
        }
        return false;
    }
}
