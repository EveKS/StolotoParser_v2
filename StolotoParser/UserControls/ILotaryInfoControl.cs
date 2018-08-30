using StolotoParser_v2.Models;
using System;

namespace StolotoParser_v2.UserControls
{
    public interface ILotaryInfoControl
    {
        Element Element { get; set; }

        LotaryToolTip ToolTip { set; }

        ProcessInfo ProcessInfo { set; }

        bool Canceled { set; }

        event EventHandler BtnLotaryClick;
    }
}