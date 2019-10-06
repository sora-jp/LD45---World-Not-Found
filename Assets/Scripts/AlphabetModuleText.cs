using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlphabetModuleText : MonoBehaviour
{
    private string lastText = "";
    public string text = "";
    private TextMeshProUGUI textUI;

    private void Awake()
    {
        textUI = GetComponent<TextMeshProUGUI>();
        text = textUI.text;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastText != text)
        {
            UpdateText();
        }

        lastText = text;
    }

    public void UpdateText()
    {
        FindObjectOfType<AlphabetManager>().UpdateText(this, text);
    }
}
