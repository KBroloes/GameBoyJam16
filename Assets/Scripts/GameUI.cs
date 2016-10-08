using UnityEngine;

public class GameUI : MonoBehaviour {

    public int HUDPosition = 8;

    public Currency currency;
    

    void Update()
    {
        if (!MenuScreen.instance.IsActive)
            currency.DrawUIElement(new Vector2(1, 1.5f));
        else
            currency.EraseUIElement();
    }

}
