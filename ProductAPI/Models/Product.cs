using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductAPI.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DisplayName("ชื่อสินค้า")]
        public string Name { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DisplayName("ราคา")]
        [Range(0.01, double.MaxValue, ErrorMessage = "กรุณากรอกเป็นตัวเลขที่มีค่ามากกว่า 0")]
        public decimal? Price { get; set; }
    }


    public class NewProduct
    {
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DisplayName("ชื่อสินค้า")]
        public string Name { get; set; }

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        [DisplayName("ราคา")]
        [Range(0.01, double.MaxValue, ErrorMessage = "กรุณากรอกเป็นตัวเลขที่มีค่ามากกว่า 0")]
        public decimal? Price { get; set; }
    }
}
