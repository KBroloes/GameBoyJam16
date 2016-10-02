using UnityEngine;
using System.Collections;


public class MenuTile : Tile
{
    public TilePlacement Placement;
}

public enum TilePlacement
{
    TopLeftCorner,
    Top,
    TopRightCorner,
    LeftSide,
    BottomLeftCorner,
    Bottom,
    BottomRightCorner,
    RightSide,
    Center
}