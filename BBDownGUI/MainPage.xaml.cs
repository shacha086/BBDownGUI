using System.Diagnostics;
using System.Text;
using static BBDownGUI.Console;

namespace BBDownGUI;

public partial class MainPage : ContentPage
{
    Console console;
    public MainPage()
    {
        InitializeComponent();
        console = new Console(
            new OutputHandler((str) =>
            {
                Trace.WriteLine(str);
                Output.Text = str;
            }
        ));
    }

    private void OnTextCompleted(object sender, EventArgs e)
    {
        string text = TextEdit.Text;
        Trace.WriteLine(text);
        console.Run(text);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        TextEdit.CursorPosition = e.NewTextValue.Length;
    }

}


