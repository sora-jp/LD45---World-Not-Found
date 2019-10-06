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
        string alphabet = "abcdefghijklmnopqrstuvwxyz";
        string characters = "i̸̛͓^q̷̧͌̓ḿ̵͔̞͋o̴͕̍̅y̴̖̦̿̾?r̵̒ċ̷͔͜͝q̵͉͝w̶͓̎̽k̸͕͐̂v̴͙̈ǎ̵̬u̸̲͐̽i̶̪͖̔̕#x̷͖̅͆t̴̰͐́l̵̒̈́͜m̷͔̗̀q̶̱̂*h̴̛̖̤͝u̶͎̚͝h̵̙͐";

        for (int i = 0; i < 26; i++)
        {
            alphabetMap[alphabet[i]] = characters[i];

            transform.GetChild(1).GetChild(i).GetComponent<AlphabetLetter>().text.text = characters[i].ToString();
        }

        UpdateAllText();
    }

    public void UpdateAllText()
    {
        foreach (AlphabetModuleText a in FindObjectsOfType<AlphabetModuleText>())
        {
            a.UpdateText();
        }
    }

    public void UpdateText(AlphabetModuleText a, string ogText)
    {
        TextMeshProUGUI UIText = a.GetComponent<TextMeshProUGUI>();

        string text = "";

        foreach (char c in ogText)
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
