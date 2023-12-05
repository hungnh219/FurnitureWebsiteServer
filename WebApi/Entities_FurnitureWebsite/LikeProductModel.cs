using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities_FurnitureWebsite;

public class LikeProductModel
{
    // Các trường từ bảng LIKE_PRODUCT
    public int LikeProductId { get; set; }
    public int ProductId { get; set; }
    public int UserId { get; set; }

    // Các trường từ bảng PRODUCTS
    public int ProductIdFromProducts { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    // Thêm các trường khác nếu cần thiết
}
