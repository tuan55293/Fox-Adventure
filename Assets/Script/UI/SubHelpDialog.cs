using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SubHelpDialog : DialogUI
{
    public SubHelpDialog nextSub;
    public HelpDialog helpDialog;
    public void Next()
    {
        if (nextSub)
        {
            nextSub.Show(true);
            gameObject.SetActive(false);
        }
        if(nextSub == null)
        {
            helpDialog.Show(false);
            GUIManager.Ins.helpAndpausePanel.gameObject.SetActive(true);
            Time.timeScale = 1;
            base.Show(false);
        }
    }
}
