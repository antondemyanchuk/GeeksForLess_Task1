using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task1.DAO;

namespace Task1.Controllers;
public class HomeController : Controller
{
    private readonly SampleContext _context;

    public HomeController(SampleContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        var currentFolderId = 1;
        var currentFolder = await _context.Cataloges
            .FirstOrDefaultAsync(catalog => catalog.Id == currentFolderId);
        if (currentFolder == null)
            return NotFound();
        ViewData["FolderName"] = $"''Folder'' - {currentFolder.Name}";
        var childFolders = await _context.Cataloges
            .Where(catalog => catalog.ParentId == currentFolderId)
            .ToListAsync();
        return View(childFolders);
    }

    public async Task<IActionResult> ViewFolder(int folderId)
    {
        var currentFolder = await _context.Cataloges
            .FirstOrDefaultAsync(catalog => catalog.Id == folderId);
        if (currentFolder == null) return NotFound();
        ViewData["FolderName"] = $"''Folder'' - {currentFolder.Name}";
        var childFolders = await _context.Cataloges
            .Where(catalog => catalog.ParentId == currentFolder.Id)
            .ToListAsync();
        return View("FolderView", childFolders);
    }
}