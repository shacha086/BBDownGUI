using System.Diagnostics;

namespace BBDownGUI;

public partial class MainPage : ContentPage
{
    private readonly Console console;
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
        //string text = TextEdit.Text;
        //Trace.WriteLine(text);
        //console.Run(text);
        var manager = new FFmpegReleaseManager();
        Output.Text = string.Join(", ", manager.Release.assets.ConvertAll((it) => $"[name: {it.name}, url: {it.browser_download_url}]"));
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        TextEdit.CursorPosition = e.NewTextValue.Length;
        if (e.NewTextValue.Length == 0)
        {
            return;
        }
        if (e.NewTextValue[^1] is '\n' or '\r')
        {
            ((Editor)sender).Text = e.NewTextValue.Remove(e.NewTextValue.Length - 1);
            OnTextCompleted(null, null);
        }
    }

}


