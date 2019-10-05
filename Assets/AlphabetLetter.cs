using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlphabetLetter : MonoBehaviour
{
    private bool inFocus = false;
    [SerializeField] TextMeshProUGUI text;
    private AlphabetManager manager;

    private void Start()
    {
        manager = FindObjectOfType<AlphabetManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) inFocus = false;

        if (inFocus)
        {
            if (Input.anyKeyDown)
            {
                char c = Input.inputString[0];
                text.text = char.ToUpper(c).ToString();
                string alphabet = "abcdefghijklmnopqrstuvwxyz";
                manager.UpdateMap(alphabet[transform.GetSiblingIndex()], char.ToLower(c));
            }
        }
    }

    public void PutInFocus()
    {
        inFocus = true;
    }
}
