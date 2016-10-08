using UnityEngine;
using System.Collections;

public class DrawableUI : MonoBehaviour {
    public string UIValue;
    public Sprite background;

    Word drawable;
    public void DrawUIElement(Vector2 position)
    {
        EraseUIElement();
        //TODO: Maybe use a different writer.
        drawable = MenuScreen.instance.writer.WriteWord(UIValue, 1);
        drawable.transform.position = position;
    }

    public void EraseUIElement()
    {
        if (drawable != null)
        {
            drawable.Erase();
        }
    }
}
