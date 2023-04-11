using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Dialog
{
    public string contents;
    public List<string> answers;
    public Action onClickButtons;

    public int Count;

    public Dialog(string contents, List<string> answers, Action onClickButtons)
    {
        this.contents = contents;
        this.answers = answers;
        this.onClickButtons = onClickButtons;

        Count = answers.Count;
    }
}
