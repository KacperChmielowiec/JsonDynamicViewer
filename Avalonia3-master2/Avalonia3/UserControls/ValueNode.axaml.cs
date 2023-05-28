﻿using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;

namespace Avalonia3.UserControls;

public partial class ValueNode : UserControl
{
    public ValueNode()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void TextBox_OnPointerReleased(object? sender, PointerReleasedEventArgs _)
    {
        if (sender == null)
        {
            return;
        }
        
        var textBox = (TextBlock)sender;
        //textBox.SelectAll();
    }
}