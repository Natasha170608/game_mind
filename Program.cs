namespace WinFormsApp12
{
    internal static class Program
    {
        public static class AppState
        {
            public static Form MainForm { get; set; }
        }

        [STAThread]
        static void Main()
        {

            Application.SetHighDpiMode(HighDpiMode.DpiUnaware);
            ApplicationConfiguration.Initialize();
            Application.Run(new QuizeGame());


        }
    }
}