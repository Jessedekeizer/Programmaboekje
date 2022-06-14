using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using QRCoder;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DigitaalProgrammaBoekje.Pages;


public class Testje : PageModel
{
    
    public string QRCodeImage { get; set; }
    public IActionResult OnGet()
    {
        using (MemoryStream ms = new MemoryStream())
        {
            PayloadGenerator.Url generator = new PayloadGenerator.Url("https://github.com/codebude/QRCoder/");
            string payload = generator.ToString();

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            var qrCodeAsBitmap = qrCode.GetGraphic(20);
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                bitMap.Save(ms, ImageFormat.Png);
                QRCodeImage = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
            }
        }

        return Page();
    }
 
    
}

