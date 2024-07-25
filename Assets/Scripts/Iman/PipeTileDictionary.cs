using System.Collections.Generic;
using UnityEngine;

public enum PipeType
{
    StraightHorizontal,
    StraightVertical,
    CornerTopLeft,
    CornerTopRight,
    CornerBottomLeft,
    CornerBottomRight,
    TTop,
    TBottom,
    TLeft,
    TRight,
    Cross
}

[System.Serializable]
public struct PipeTile
{
    public PipeType type;
    public bool top;
    public bool bottom;
    public bool left;
    public bool right;
}

public static class PipeTileDictionary
{
    public static Dictionary<PipeType, PipeTile> pipeTiles = new Dictionary<PipeType, PipeTile>
    {
        { PipeType.StraightHorizontal, new PipeTile { type = PipeType.StraightHorizontal, top = false, bottom = false, left = true, right = true } },
        { PipeType.StraightVertical, new PipeTile { type = PipeType.StraightVertical, top = true, bottom = true, left = false, right = false } },
        { PipeType.CornerTopLeft, new PipeTile { type = PipeType.CornerTopLeft, top = true, bottom = false, left = true, right = false } },
        { PipeType.CornerTopRight, new PipeTile { type = PipeType.CornerTopRight, top = true, bottom = false, left = false, right = true } },
        { PipeType.CornerBottomLeft, new PipeTile { type = PipeType.CornerBottomLeft, top = false, bottom = true, left = true, right = false } },
        { PipeType.CornerBottomRight, new PipeTile { type = PipeType.CornerBottomRight, top = false, bottom = true, left = false, right = true } },
        { PipeType.TTop, new PipeTile { type = PipeType.TTop, top = true, bottom = false, left = true, right = true } },
        { PipeType.TBottom, new PipeTile { type = PipeType.TBottom, top = false, bottom = true, left = true, right = true } },
        { PipeType.TLeft, new PipeTile { type = PipeType.TLeft, top = true, bottom = true, left = true, right = false } },
        { PipeType.TRight, new PipeTile { type = PipeType.TRight, top = true, bottom = true, left = false, right = true } },
        { PipeType.Cross, new PipeTile { type = PipeType.Cross, top = true, bottom = true, left = true, right = true } }
    };
}


