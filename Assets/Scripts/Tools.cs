using UnityEngine;

public static class Tools
{
    public static Vector2Int WindDirection(Weather wind)
    {
        return wind switch {
            Weather.WindW => Vector2Int.right,
            Weather.WindE => Vector2Int.left,
            Weather.WindN => Vector2Int.down,
            Weather.WindS => Vector2Int.up,
            _ => Vector2Int.zero
        };
    }
}