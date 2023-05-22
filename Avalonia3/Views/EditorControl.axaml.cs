using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using AvaloniaEdit;
using AvaloniaEdit.CodeCompletion;
using AvaloniaEdit.Document;
using AvaloniaEdit.Editing;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Indentation.CSharp;
using AvaloniaEdit.Rendering;
using AvaloniaEdit.Search;
using Microsoft.Win32;
using System;

namespace Avalonia3.Views
{
    public partial class EditorControl : UserControl
    {
        public TextEditor Editor;
        public EditorControl()
        {
            InitializeComponent();
            Editor = this.FindControl<TextEditor>("textCode");
           
            var search = SearchPanel.Install(Editor);
            search.Reactivate();
            Editor.TextArea.SelectionChanged += CursorHandler;

        }
        private void CursorHandler(object sender, EventArgs e)
        {
            Editor.TextArea.ScrollToLine(Editor.CaretOffset);
        }

    }
}
