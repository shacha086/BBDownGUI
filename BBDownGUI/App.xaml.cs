using System.Runtime.InteropServices;

namespace BBDownGUI;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
	}
}
