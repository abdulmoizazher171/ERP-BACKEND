using ERP_BACKEND.constracts;
using ERP_BACKEND.dtos;
namespace ERP_BACKEND.interfaces;


public interface IAttachment
{
    // Get all attachements
    Task<IEnumerable<readAttachmentDto>> GetAllAttachementsAsync();

    // Get one specific attachement by ID
    Task<readAttachmentDto?> GetAttachementByIdAsync(int id);

    // Add a new attachement to the database
    Task<readAttachmentDto> AddAttachementAsync(createAttachmentDto attachment);

    Task<Attachment> UploadAttachmentAsync(IFormFile file , int itemId);
}