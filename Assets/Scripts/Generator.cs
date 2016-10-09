using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

    Animator animator;

    [Header("Rate of generation per second")]
    public float GenerationRate = 0.25f;

    bool canHarvest;
    bool harvesting;
    float deltaTime;

    Unit unit;
    
    void Start()
    {
        unit = GetComponent<Unit>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        deltaTime += Time.fixedDeltaTime;

        if (deltaTime >= 1 / GenerationRate)
        {
            canHarvest = true;
            deltaTime = 0f;
        }
    }

    void Update()
    {
        if(canHarvest)
        {
            animator.speed = 1;
            if (!harvesting && GameManager.instance.cursor.transform.position == transform.position)
            {
                Harvest();
            }
        } else
        {
            animator.speed = GenerationRate;
        }
    }

    public void HarvestDone()
    {
        canHarvest = false;
        harvesting = false;
    }

    public void Harvest()
    {
        if(canHarvest)
        {
            int currencyGenerated = 0;
            if (unit != null)
            {
                currencyGenerated = unit.Strength;
            }
            GameManager.instance.currency.Add(currencyGenerated);

            animator.SetTrigger("Harvest");
            harvesting = true;
        }        
    }
}
