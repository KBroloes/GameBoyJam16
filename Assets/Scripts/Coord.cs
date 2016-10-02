using UnityEngine;

struct Coord
{
    public int x;
    public int y;

    public Coord(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Coord From(Vector2 location)
    {
        int x = (int)location.x;
        int y = (int)location.y;
        return new Coord(x, y);
    }
}
