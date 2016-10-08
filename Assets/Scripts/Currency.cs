using UnityEngine;

public class Currency : DrawableUI {

    [Header("Currency Regen")]
    public int passiveGeneration = 5;
    public float generationRate = 2f;

    int currency;

    void Update()
    {
        if (addCurrency)
        {
            currency += passiveGeneration;
            addCurrency = false;
        }

        //TODO: Nasty hack because interfaces are not supported in the inspector.
        UIValue = currency.ToString();
    }

    float currencyTimer = 0f;
    bool addCurrency;
    void FixedUpdate()
    {
        currencyTimer += Time.fixedDeltaTime;
        if (currencyTimer >= generationRate)
        {
            currencyTimer -= generationRate;
            addCurrency = true;
        }
    }

    public void Spend(int amount)
    {
        currency -= amount;
    }

    public int Get()
    {
        return currency;
    }    
}
