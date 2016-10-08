using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {

    public int HUDPosition = 8;

    public List<HUDElement> elements;

    void Update()
    {
        foreach(HUDElement element in elements)
        {
            if (!MenuScreen.instance.IsActive)
                element.drawable.DrawUIElement(element.position);
            else
                element.drawable.EraseUIElement();
        }
    }
}
