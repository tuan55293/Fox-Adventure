using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpDialog : DialogUI
{
    public SubHelpDialog firstSub;
    public SubHelpDialog lastSub;
    public override void Show(bool show)
    {
        base.Show(show);
        firstSub.Show(true);
        lastSub.Show(false);
    }
}
