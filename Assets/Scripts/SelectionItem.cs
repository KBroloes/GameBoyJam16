using UnityEngine;

[System.Serializable]
public class SelectionItem : DrawableUI {

    [Header("Selection")]
    public Tile selectionTile;
    public Tile selectionBackground;
    public int Cost;
    public Unit unit;
    public Vector2 labelRelativePosition;

    void Start()
    {
        UIValue = Cost.ToString();
    }

    void Update()
    {
        if (!MenuScreen.instance.IsActive)
        {
            Writer writer = FindObjectOfType<Writer>();
            if (writer != null)
            {
                Vector2 position = transform.position;
                position.x += labelRelativePosition.x;
                position.y += labelRelativePosition.y;
                EraseUIElement();
                DrawUIElement(position);
            }
        }
        else
            EraseUIElement();
        
    }
}
