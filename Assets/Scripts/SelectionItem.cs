using UnityEngine;
using System.Collections;

[System.Serializable]
public class SelectionItem : DrawableUI {

    [Header("Selection")]
    public Tile selectionTile;
    public Tile selectionBackground;
    public int cost;
    public Unit unit;
    public Vector2 labelRelativePosition;

    void Start()
    {
        UIValue = cost.ToString();
    }

    void Update()
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
}
