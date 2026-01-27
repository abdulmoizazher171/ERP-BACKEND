using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
using ERP_BACKEND.constracts;
using ERP_BACKEND.data;
namespace ERP_BACKEND.services;
using Microsoft.EntityFrameworkCore;
public class AttachmentService : IAttachment
{
    public readonly AppDbContext _context;

    public AttachmentService(AppDbContext context)
    {
        _context = context;
    }
    public Task<readAttachmentDto> AddAttachementAsync(createAttachmentDto attachment)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<readAttachmentDto>> GetAllAttachementsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<readAttachmentDto?> GetAttachementByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<Attachment> UploadAttachmentAsync(IFormFile file , int itemId)
    {


    var itemExists = await _context.Assets.AnyAsync(a => a.ITEM_ID == itemId);
    if (!itemExists)
    {
        throw new Exception($"Cannot upload attachment: Asset with ID {itemId} does not exist.");
    }

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

    // 2. Create a unique filename
    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
    var filePath = Path.Combine(uploadsFolder, fileName);

    // 3. Save the file to disk
    using (var stream = new FileStream(filePath, FileMode.Create))
    {
        await file.CopyToAsync(stream);
    }

    // 4. Return the URL (e.g., /uploads/xyz.jpg)
    var fileUrl = $"/uploads/{fileName}";

    var newAttachment = new Attachment
    {
        ATTACHMENT_URL = fileUrl,
        ITEM_ID =  itemId,
    };

    // --- MISSING STEP: Save to DB ---
    _context.Attachments.Add(newAttachment);
    await _context.SaveChangesAsync();

       return  newAttachment;
    }
    }