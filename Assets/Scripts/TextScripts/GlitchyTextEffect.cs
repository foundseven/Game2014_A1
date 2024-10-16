using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GlitchyTextEffect : MonoBehaviour
{
    //how long
    [SerializeField] 
    private float glitchDuration = 0.5f; 

    //how far
    [SerializeField] 
    private float glitchMagnitude = 20f;

    //text ref
    [SerializeField] 
    private TMP_Text text;

    private Vector3 originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.localPosition;
        StartCoroutine(GlitchText());
    }

    IEnumerator GlitchText()
    {
        //loop foreverrrrr
        while (true)
        {
            yield return new WaitForSeconds(0.85f);

            //start the glitch effect
            Vector3 randomOffset = new Vector3(
                Random.Range(-glitchMagnitude, glitchMagnitude),
                Random.Range(-glitchMagnitude, glitchMagnitude),
                0);

            //create a random new pos
            Vector3 targetPosition = originalPosition + randomOffset;

            float elapsed = 0f;

            //move to that random pos
            while (elapsed < glitchDuration)
            {
                transform.localPosition = Vector3.Lerp(originalPosition, targetPosition, elapsed / glitchDuration);
                elapsed += Time.deltaTime;
                yield return null; // Wait until the next frame
            }

            //go back to original position
            elapsed = 0f;
            while (elapsed < glitchDuration)
            {
                transform.localPosition = Vector3.Lerp(targetPosition, originalPosition, elapsed / glitchDuration);
                elapsed += Time.deltaTime;
                yield return null; // Wait until the next frame
            }

            transform.localPosition = originalPosition;
        }
    }
}
