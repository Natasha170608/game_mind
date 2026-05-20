namespace WinFormsApp12
{
    internal static class Program
    {
        
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new QuizeGame());


        }
    }
}