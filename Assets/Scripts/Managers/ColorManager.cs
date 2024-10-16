using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    [SerializeField] 
    private List<Color> _availableColors;

    // Get a random color from the list
    public Color GetRandomColor()
    {
        return _availableColors[Random.Range(0, _availableColors.Count)];
    }
}
