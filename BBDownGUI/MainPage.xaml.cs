using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using static BBDownGUI.Console;

namespace BBDownGUI;

public partial class MainPage : ContentPage
{
    Console console;
    public MainPage()
    {
        InitializeComponent();

        console = new Console((str) =>
            {
                Application.Current.Dispatcher.Dispatch(() => Output.Text = str);
            }
        );
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
        if (e.NewTextValue.Length == 0)
        {
            return;
        }
        if (new char[] { '\r', '\n' }.Contains(e.NewTextValue[^1]))
        {
            ((Editor)sender).Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            OnTextCompleted(null, null);
        }
    }

}


