using ERP_BACKEND.constracts;
using ERP_BACKEND.dtos;
using ERP_BACKEND.interfaces;
namespace ERP_BACKEND.services;
using ERP_BACKEND.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
[Authorize]

public class AttachmentController : ControllerBase
{
    private readonly IAttachment _attachmentService;

    public AttachmentController(IAttachment attachmentService)
    {
        _attachmentService = attachmentService;
    }

    [HttpGet]
    
    public async Task<ActionResult<IEnumerable<readAttachmentDto>>> GetAllAttachments()
    {
        var attachments = await _attachmentService.GetAllAttachementsAsync();
        return Ok(attachments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<readAttachmentDto>> GetAttachmentById(int id)
    {
        var attachment = await _attachmentService.GetAttachementByIdAsync(id);
        if (attachment == null)
        {
            return NotFound();
        }
        return Ok(attachment);
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage([FromForm] IFormFile file, 
    [FromForm] int assetId)
    {
        if (file == null || file.Length == 0) return BadRequest("No file uploaded");

            var attachment = await _attachmentService.UploadAttachmentAsync(file, assetId);
    // 1. Define the folder path
        return attachment != null ? Ok(attachment) : BadRequest("File upload failed");
    }
}
