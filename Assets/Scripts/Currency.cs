using UnityEngine;
using System.Collections;

public class Currency : MonoBehaviour {
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

    Word currentCurrency;
    public void DrawUIElement(Vector2 position)
    {
        EraseUIElement();
        currentCurrency = MenuScreen.instance.writer.WriteWord(currency.ToString(), 1);
        currentCurrency.transform.position = position;
    }

    public void EraseUIElement()
    {
        if (currentCurrency != null)
        {
            currentCurrency.Erase();
        }
    }
}
