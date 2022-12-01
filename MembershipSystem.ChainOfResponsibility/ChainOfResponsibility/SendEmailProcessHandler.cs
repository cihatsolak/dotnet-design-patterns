namespace MembershipSystem.ChainOfResponsibility.ChainOfResponsibility
{
    public class SendEmailProcessHandler : ProcessHandler
    {
        private readonly string _fileName;
        private readonly string _toEmail;

        public SendEmailProcessHandler(string fileName, string toEmail)
        {
            _fileName = fileName;
            _toEmail = toEmail;
        }

        public override object Handle(object obj)
        {
            var zipMemoryStream = obj as MemoryStream;
            zipMemoryStream.Position = 0;
            

            SmtpClient smptClient = new("srvm11.trwww.com");

            MailMessage mailMessage = new()
            {
                From = new("deneme@kariyersistem.com")
            };

            mailMessage.To.Add(new(_toEmail));

            mailMessage.Subject = "Zip dosyası";
            mailMessage.Body = "<p>Zip dosyası ektedir.</p>";

            Attachment attachment = new(zipMemoryStream, _fileName, MediaTypeNames.Application.Zip);
            mailMessage.Attachments.Add(attachment);

            mailMessage.IsBodyHtml = true;
            smptClient.Port = 587;
            smptClient.Credentials = new NetworkCredential("deneme@kariyersistem.com", "Password12*");

            smptClient.Send(mailMessage);

            return base.Handle(null);
        }
    }
}
