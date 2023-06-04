using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia3.Behaviors;
using AvaloniaEdit;
using AvaloniaEdit.CodeCompletion;
using AvaloniaEdit.Document;
using AvaloniaEdit.Editing;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Indentation.CSharp;
using AvaloniaEdit.Rendering;
using AvaloniaEdit.Search;

using AvaloniaEdit.TextMate;
using AvaloniaEdit.TextMate.Grammars;
using System;
using TextMateSharp.Themes;

namespace Avalonia3.Views
{
    public partial class EditorControl : UserControl
    {
        public TextEditor Editor;

        public static readonly StyledProperty<string> StyledProperty =
          AvaloniaProperty.Register<DocumentTextBindingBehavior, string>(nameof(BackgroundProp));

        public string BackgroundProp
        {
            get => GetValue(StyledProperty);
            set => SetValue(StyledProperty, value);
        }


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
