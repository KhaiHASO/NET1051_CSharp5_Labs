using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoSlide4.Data;
using DemoSlide4.Models;
using System.Security.Claims;

namespace DemoSlide4.Controllers;

[Authorize]
public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IAuthorizationService _authorizationService;

    public ProductController(ApplicationDbContext context, IAuthorizationService authorizationService)
    {
        _context = context;
        _authorizationService = authorizationService;
    }

    // Index: Mọi người dùng đã đăng nhập đều có thể xem danh sách
    public async Task<IActionResult> Index()
    {
        return View(await _context.Products.ToListAsync());
    }

    // Create (GET): Yêu cầu policy CreateProductPolicy
    [Authorize(Policy = "CreateProductPolicy")]
    public IActionResult Create()
    {
        return View();
    }

    // Create (POST): Yêu cầu policy CreateProductPolicy
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = "CreateProductPolicy")]
    public async Task<IActionResult> Create([Bind("Name,Price")] Product product)
    {
        if (ModelState.IsValid)
        {
            // Lưu ID của người dùng hiện tại vào CreatedBy
            product.CreatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    // Details: Kiểm tra quyền xem dựa trên Admin hoặc Sales (có logic CreatedBy)
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
        if (product == null) return NotFound();

        // 1. Kiểm tra quyền Admin (AdminViewProductPolicy)
        var isAdmin = (await _authorizationService.AuthorizeAsync(User, "AdminViewProductPolicy")).Succeeded;

        // 2. Kiểm tra quyền Sales (SalesViewProductPolicy) kết hợp CreatedBy
        var isSales = (await _authorizationService.AuthorizeAsync(User, "SalesViewProductPolicy")).Succeeded;
        
        // Logic Bài 2: Sales chỉ được xem nếu họ là người tạo ra sản phẩm
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var canViewAsSales = isSales && product.CreatedBy == userId;

        if (isAdmin || canViewAsSales)
        {
            return View(product);
        }

        // Nếu không có quyền, trả về Forbidden hoặc thông báo
        return Forbid();
    }
}
