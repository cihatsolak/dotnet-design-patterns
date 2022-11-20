using DinkToPdf;
using DinkToPdf.Contracts;
using ColorMode = DinkToPdf.ColorMode;
using Orientation = DinkToPdf.Orientation;
using PaperKind = DinkToPdf.PaperKind;

namespace MembershipSystem.Command.Commands
{
    public class PdfFile<T>
    {
        public readonly List<T> _list;
        public readonly HttpContext _context;

        public PdfFile(List<T> list, HttpContext context)
        {
            _list = list;
            _context = context;
        }

        public string FileName => $"{typeof(T).Name}.pdf";
        public string FileType => "application/octet-stream";

        public MemoryStream Create()
        {
            var type = typeof(T);

            StringBuilder stringBuilder = new();

            stringBuilder.Append($@"<html>
                          <head></head>
                          <body>
                            <div class='text-center'><h1>{type.Name} tablo</h1></div>
                            <table class='table table-striped' align='center'>");

            stringBuilder.Append("<tr>");
            type.GetProperties().ToList().ForEach(x =>
            {
                stringBuilder.Append($"<th>{x.Name}</th>");
            });
            stringBuilder.Append("</tr>");

            _list.ForEach(x =>
            {
                var values = type.GetProperties().Select(properyInfo => properyInfo.GetValue(x, null)).ToList();

                stringBuilder.Append("<tr>");

                values.ForEach(value =>
                {
                    stringBuilder.Append($"<td>{value}</td>");
                });

                stringBuilder.Append("</tr>");
            });

            stringBuilder.Append("</table></body></html");
            var cihat = stringBuilder.ToString();
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                },
                Objects = {
                new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = stringBuilder.ToString(),
                        WebSettings = { DefaultEncoding = "utf-8",UserStyleSheet=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/lib/bootstrap/dist/css/bootstrap.css") },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 }
                    }
                }
            };

            var converter = _context.RequestServices.GetRequiredService<IConverter>();

            return new(converter.Convert(doc));
        }
    }
}
