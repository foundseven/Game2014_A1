using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    Color[] _colors = {Color.green, Color.cyan,
    Color.magenta, Color.yellow};

    // Get a random color from the list
    public Color GetRandomColor()
    {
        return _colors[Random.Range(0, _colors.Length)];
    }
}
