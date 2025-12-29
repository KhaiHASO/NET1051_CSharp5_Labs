using System.ComponentModel.DataAnnotations;

namespace DemoSlide4.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    // Lưu trữ ID của người dùng tạo sản phẩm (theo yêu cầu Lab 2)
    public string CreatedBy { get; set; } = string.Empty;
}
