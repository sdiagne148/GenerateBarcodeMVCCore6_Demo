using GenerateBarcodeMVCCore6_Demo.Models;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Drawing;

namespace GenerateBarcodeMVCCore6_Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        public HomeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult CreateBarcode()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateBarcode(GenerateBarcodeModel generateBarcode)
        {
            try
            {
                GeneratedBarcode barcode = IronBarCode.BarcodeWriter.CreateBarcode(generateBarcode.BarcodeText, BarcodeWriterEncoding.Code128);
                barcode.ResizeTo(400, 120);
                barcode.AddBarcodeValueTextBelowBarcode();
                // Styling a Barcode and adding annotation text
                barcode.ChangeBarCodeColor(Color.BlueViolet);
                barcode.SetMargins(10);
                string path = Path.Combine(_environment.WebRootPath, "GeneratedBarcode");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string filePath = Path.Combine(_environment.WebRootPath, "GeneratedBarcode/barcode.png");
                barcode.SaveAsPng(filePath);
                string fileName = Path.GetFileName(filePath);
                string imageUrl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}" + "/GeneratedBarcode/" + fileName;
                ViewBag.QrCodeUri = imageUrl;
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }

    }
}