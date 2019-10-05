using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hover : MonoBehaviour
{
    private Button button;
    [SerializeField] Color c1, c2;
    [SerializeField] Image image;
    [SerializeField] float time;

    Coroutine c;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerEnter()
    {
        if (c != null) StopCoroutine(c);
        c=StartCoroutine(Animate(image.color, c2, time));
    }

    public void OnPointerExit()
    {
        if (c != null) StopCoroutine(c);
        c =StartCoroutine(Animate(image.color, c1, time));
    }

    private IEnumerator Animate(Color c1, Color c2, float time)
    {
        float t = 0;

        while (t < 1)
        {
            image.color = Color.Lerp(c1, c2, t);

            yield return null;
            t += Time.deltaTime / time;
        }

        image.color = c2;
    }
}
