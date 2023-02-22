using System;
using UnityEngine;

[Serializable]
public class ResolutionScript
{
    [SerializeField]
    private int horizontal;
    [SerializeField]
    private int vertical;

    public int GetHorizontal()
    {
        return horizontal;
    }

    public int GetVertical()
    {
        return vertical;
    }

    public override string ToString()
    {
        return string.Join(" x ", new string[] { horizontal.ToString(), vertical.ToString() });
    }
}