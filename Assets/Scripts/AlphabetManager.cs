using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TMPro;

public class AlphabetManager : SerializedMonoBehaviour
{
    [SerializeField] private Dictionary<char, char> alphabetMap;

    private void Start()
    {
        UpdateAllText();
    }

    public void UpdateAllText()
    {
        foreach (AlphabetModuleText a in FindObjectsOfType<AlphabetModuleText>())
        {
            UpdateText(a);
        }
    }

    public void UpdateText(AlphabetModuleText a)
    {
        TextMeshProUGUI UIText = a.GetComponent<TextMeshProUGUI>();

        string text = "";

        foreach (char c in UIText.text)
        {
            char map;
            if (!alphabetMap.TryGetValue(char.ToLower(c), out map))
            {
                map = c;
            }

            if (char.IsLower(c)) map = char.ToLower(map);
            else map = char.ToUpper(map);

            text += map;
        }

        UIText.text = text;
    }

    public void UpdateMap(char key, char value)
    {
        alphabetMap[key] = value;
        UpdateAllText();
    }
}
