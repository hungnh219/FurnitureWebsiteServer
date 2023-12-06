using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using WebApi.Entities_FurnitureWebsite;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FurnitureController : ControllerBase
    {
        private readonly FurnitureWebsiteContext _context;
        public FurnitureController(FurnitureWebsiteContext ctx)
        {
            _context = ctx;
        }

        [HttpGet("GetAllProducts")]
        public IActionResult GetAllProducts()
        {
            var query = _context.Products.FromSqlInterpolated($"EXEC sp_GetAllProduct").ToList();

            return Ok(query);
        }

        [HttpGet("GetProductByName{productName}")]
        public IActionResult GetProductByName(string productName)
        {
            var query = _context.Products.FromSqlInterpolated($"EXEC sp_GetProductByName @Name = {productName}").ToList();

            return Ok(query);
        }
        //var hocViens = _context.Hocviens.FromSqlRaw("SELECT * FROM Hocviens WHERE GioiTinh = 'Nam'").ToList();
        [HttpGet("ByMaterial{material}")]
        public IActionResult GetProductByMaterial(string material)
        {
            var query = _context.Products.Where(p => p.Material == material).ToList();

            return Ok(query);
        }

        [HttpGet("ByDesc{material}")]
        public IActionResult GetProductByMaterialDesc(string material)
        {
            //var materialParam = new SqlParameter("@Material", material);

            // var query = _context.Products.FromSqlRaw("SELECT * FROM PRODUCTS WHERE Material = @Material ORDER BY PRICE", materialParam).ToList();

            var query = _context.Products
                                .FromSqlInterpolated($"SELECT * FROM PRODUCTS WHERE Material = {material} ORDER BY PRICE")
                                .ToList();

            return Ok(query);
        }
        [HttpGet("UpdateLastProductHeight")]
        public IActionResult UpdateLastProductHeight()
        {
            //_context.Database.ExecuteSqlCommand("UPDATE Products SET Height = 50 WHERE Id = (SELECT TOP 1 Id FROM Products ORDER BY Id DESC)");

            return Ok("Last product height updated successfully");
        }

        [HttpGet("GetProductByTagName{tag}")]
        public IActionResult GetProductByTagName(string tag)
        {
            var query = _context.Products.FromSqlInterpolated($"EXEC sp_GetProductsByTagName @TagName = {tag}").ToList();

            return Ok(query);
        }

        [HttpGet("GetUserInfo{id}")]
        public IActionResult GetUserInfo(string id)
        {
            //var query = _context.Users
                               // .FromSqlInterpolated($"SELECT userName FROM USERS WHERE Id = {id}")    
                                //.ToList();
            var query = _context.Users
                                .Where(u => u.Id.ToString() == id)
                                .Select(u => new { u.Id, u.UserName, u.FirstName, u.LastName, u.Mail, u.Phone, u.Address, u.RegDate, u.BirthYear })
                                .ToList();

            return Ok(query);
        }


        [HttpGet("GetCartlistUserById{id}")]
        public IActionResult GetCartlistUserById(string id)
        {
            var query = from receipt in _context.Receipts
                        join receiptDetail in _context.ReceiptDetails on receipt.Id equals receiptDetail.ReceiptId
                        join product in _context.Products on receiptDetail.ProductId equals product.Id
                        where receipt.UserId.ToString() == id
                        orderby receipt.Id, product.Name
                        select new
                        {
                            product.Id,
                            product.Name,
                            product.Price,
                            product.Material,
                            product.ImgDirect
                        };

            return Ok(query.ToList());
        }

        [HttpGet("GetWishlistUserById{id}")]
        public IActionResult GetWishlistUserById(string id)
        {
            var query = from likeProduct in _context.LikeProducts
                        join product in _context.Products on likeProduct.ProductId equals product.Id
                        where likeProduct.UserId.ToString() == id
                        select new
                        {
                            product.Id,
                            product.Name,
                            product.Price,
                            product.Material,
                            product.ImgDirect
                        };
            return  Ok(query.ToList());
        }


        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel loginModel)
        {
            // Kiểm tra thông tin đăng nhập với cơ sở dữ liệu
            var user = _context.Users.FirstOrDefault(u => u.UserName == loginModel.Username);

            if (user != null && user.PassWord == loginModel.Password)
            {
                // Đăng nhập thành công 
                return Ok(user.Id);
            }
            else
            {
                // Đăng nhập thất bại
                return BadRequest(new { Message = "Invalid username or password" });
            }
        }

        // update profile
        public class UpdateUserModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Mail { get; set; }
            public string Phone { get; set; }
            public string Address { get; set; }
            public DateTime RegDate { get; set; }
        }

        [HttpPut("UpdateUser/{id}")]
        public IActionResult UpdateUser(int id, [FromForm] UpdateUserModel updatedUserData)
        {
            if (updatedUserData == null)
            {
                return BadRequest("Invalid data");
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            existingUser.FirstName = updatedUserData.FirstName;
            existingUser.LastName = updatedUserData.LastName;
            existingUser.Phone = updatedUserData.Phone;
            existingUser.Mail = updatedUserData.Mail;
            existingUser.Address = updatedUserData.Address;
            existingUser.RegDate = updatedUserData.RegDate;

            _context.SaveChanges();

            return Ok(existingUser);
        }

        // update password
        public class UpdatePasswordModel
        {
            public string UserName { get; set; }
            public string NewPassword { get; set; }
        }
        [HttpPut("UpdatePassword")]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordModel updatePasswordModel)
        {
            if (updatePasswordModel == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == updatePasswordModel.UserName);

            if (existingUser == null)
            {
                return NotFound("User not found");
            }

            existingUser.PassWord = updatePasswordModel.NewPassword;

            _context.SaveChanges();

            return Ok("Password updated successfully");
        }

        //create account
        public class CreateUserModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        [HttpPost("CreateAccount")]
        public IActionResult CreateAccount(CreateUserModel userModel)
        {
            if (userModel == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }

            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == userModel.UserName);
            if (existingUser != null)
            {
                return Conflict("User already exists");
            }

            var newUser = new User
            {
                UserName = userModel.UserName,
                PassWord = userModel.Password,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            return Ok("Account created successfully");
        }

    }
}
