namespace MVCTaskProject.Configuration
{
    public class MailSettings
    {
        public string Server { get; set; } = "smtp.gmail.com";
        public int port { get; set; } = 587;
        public string SenderName { get; set; } = "Nirav Tandel";
        public string SenderEmail { get; set; } = "niravtandelph@gmail.com";
        public string UserName { get; set; } = "niravtandelph@gmail.com";
        public string Password { get; set; } = "mxdftgqtmumtrgpt";
    }
}
