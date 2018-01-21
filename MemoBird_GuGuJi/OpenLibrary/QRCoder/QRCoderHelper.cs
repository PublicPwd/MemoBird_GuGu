using QRCoder;
using System.Drawing;

namespace MemoBird_GuGu.OpenLibrary.QRCoder
{
    class QRCoderHelper
    {
        public static Bitmap Generate(string content)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.L);
            QRCode qrCode = new QRCode(qrCodeData);
            return qrCode.GetGraphic(10);
        }
    }
}
