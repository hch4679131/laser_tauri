namespace mes_winform
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            try
            {
                // ���� MesWebServiceClient ʵ��
                MesWebServiceClient client = new MesWebServiceClient();

                // �����ϴ����ݵķ���
                string response = await client.UploadDataToMesAsync();

                // �����Ӧ���
                Console.WriteLine("MES system response:");
                Console.WriteLine(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            Application.Run(new Form1());
        }
    }
}