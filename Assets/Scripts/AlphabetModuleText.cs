using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlphabetModuleText : MonoBehaviour
{
    private string lastText;
    private TextMeshProUGUI textUI;

    private void Start()
    {
        textUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lastText != textUI.text)
        {
            FindObjectOfType<AlphabetManager>().UpdateText(this);
        }
    }
}
