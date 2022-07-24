using System.ComponentModel.DataAnnotations;

namespace GenerateBarcodeMVCCore6_Demo.Models
{
    public class GenerateBarcodeModel
    {
        [Display(Name = "Enter Barcode Text")]
        public string BarcodeText
        {
            get;
            set;
        }
    }
}
