using System;
using StolotoParser_v2.Models;

namespace StolotoParser_v2.UserControls
{
    public interface IElementButton
    {
        Element Element { get; set; }
        LotaryToolTip ToolTip { set; }

        event EventHandler ElementButtonClick;

        void Canceled();
        void Continue();
        void Pause();
    }
}