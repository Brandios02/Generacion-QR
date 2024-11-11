using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System.IO;
using System;


namespace qr_generation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrController : Controller
    {
        [HttpGet("generate-qr")]
        public IActionResult GenerateQr(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return BadRequest("El contenido del código QR no puede estar vacío.");
            }

            try
            {
                // Usar QRCoder para generar el código QR
                var qrGenerator = new QRCodeGenerator();  // No es necesario 'using' porque QRCodeGenerator no implementa IDisposable

                // Generar la versión del código QR (versión 6 es buena para textos largos)
                var qrData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);

                // Usar un generador de código QR para crear una imagen de tipo PNG
                var qrCode = new QRCode(qrData);  // No es necesario 'using' para QRCode

                using (var ms = new MemoryStream())
                {
                    // Crear la imagen y guardarla en un MemoryStream
                    var qrImage = qrCode.GetGraphic(20);  // Aquí se crea la imagen
                    qrImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);  // Guardar en PNG

                    // Retornar la imagen en formato PNG como respuesta
                    return File(ms.ToArray(), "image/png");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error generando el QR: {ex.Message}");
            }
        }
    }
}
