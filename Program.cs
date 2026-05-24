namespace WinFormsApp12
{
    internal static class Program
    {
        
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
            ApplicationConfiguration.Initialize();
            Application.Run(new QuizeGame());


        }
    }
}