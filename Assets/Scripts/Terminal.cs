using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class Terminal : ScreenCanvas
{
    public TextMeshProUGUI text;
    public string terminalGroup;
    public float kps;

    private string m_currentText = "> ";
    private string m_currentInput;
    private int m_currentSubstringLength;

    private bool m_isCommandRunning;

    void Start()
    {
        StartCoroutine(UpdateSubstring());
    }

    void Update()
    {
        if (Screen.IsSelected && !m_isCommandRunning && m_currentSubstringLength == m_currentText.Length)
        {
            if (AccumulateCurrentInput())
            {
                m_currentText += m_currentInput;
                m_currentSubstringLength = m_currentText.Length;
                ExecuteCommand(m_currentInput.Trim());
                m_currentInput = "";
            }
        }

        text.text = m_currentText.Substring(0, m_currentSubstringLength) + m_currentInput;
    }

    public void WriteLine(string line)
    {
        m_currentText += line + "\n";
    }

    public void Write(string text)
    {
        m_currentText += text;
    }

    IEnumerator UpdateSubstring()
    {
        while (true)
        {
            if (m_currentSubstringLength >= m_currentText.Length)
            {
                yield return null;
            }
            else
            {
                m_currentSubstringLength++;
                yield return new WaitForSeconds(1f / kps);
            }
        }
    }

    bool AccumulateCurrentInput()
    {
        string input = Input.inputString;
        foreach (var c in input)
        {
            if (c == '\b')
            {
                if (m_currentInput.Length > 0)
                    m_currentInput = m_currentInput.Substring(0, m_currentInput.Length - 1);
            }
            else if (c == '\n' || c == '\r')
            {
                m_currentInput += '\n';
                return true;
            }
            else
            {
                m_currentInput += c;
            }
        }

        return false;
    }

    IEnumerator CommandWrapper(IEnumerator command)
    {
        m_isCommandRunning = true;
        yield return command;
        m_isCommandRunning = false;
        Write("> ");
    }

    private void ExecuteCommand(string input)
    {
        var data = input.Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries);

        var dict = ConsoleCommandLoader.GetCommandsWithGroup(terminalGroup);

        ConsoleCommand cmd = null;
        if (dict.ContainsKey(data[0].ToLower()))
        {
            cmd = dict[data[0].ToLower()];
        }

        StartCoroutine(CommandWrapper(cmd?.Execute(this, data.Skip(1).ToArray())));

        if (cmd == null)
        {
            WriteLine("Command not found!");
        }
    }
}