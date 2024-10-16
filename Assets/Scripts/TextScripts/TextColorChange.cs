using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextColorChange : MonoBehaviour
{
    //text ref
    [SerializeField]
    private TMP_Text text;

    private ColorManager _colorManager;

    public Color CurrentColor { get; private set; }

    void Start()
    {
        _colorManager = FindObjectOfType<ColorManager>();
        StartCoroutine(ChangeColorofText());

    }
    void SetRandomColor()
    {
        CurrentColor = _colorManager.GetRandomColor();
        text.color = CurrentColor;
    }

    //Coroutine to change the players color
    IEnumerator ChangeColorofText()
    {
        while (true)
        {
            SetRandomColor();

            //waiting eloted seconds
            yield return new WaitForSeconds(0.75f);

        }
    }
}
