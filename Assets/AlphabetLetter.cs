using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AlphabetLetter : MonoBehaviour
{
    private bool inFocus = false;
    public TextMeshProUGUI text;
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
            if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.LeftShift) && !Input.GetKeyDown(KeyCode.RightShift))
            {
                char c = Input.inputString[0];
                text.text = char.ToUpper(c).ToString();
                string alphabet = "abcdefghijklmnopqrstuvwxyz";
                manager.UpdateMap(alphabet[transform.GetSiblingIndex()], char.ToLower(c));

                inFocus = false;
                Debug.Log(transform.GetSiblingIndex() + 1);
                if (transform.GetSiblingIndex() + 1 < transform.parent.childCount)
                {
                    StartCoroutine(Wait());
                }
            }
        }

        IEnumerator Wait()
        {
            yield return null;
            transform.parent.GetChild(transform.GetSiblingIndex() + 1).GetComponent<AlphabetLetter>().inFocus = true;
            EventSystem.current.SetSelectedGameObject(transform.parent.GetChild(transform.GetSiblingIndex() + 1).gameObject, new BaseEventData(EventSystem.current));
        }
    }

    public void PutInFocus()
    {
        inFocus = true;
    }
}
