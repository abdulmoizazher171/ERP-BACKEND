namespace ERP_BACKEND.dtos;

public record readAttachmentDto
(
    int AttachmentId,
    int ItemId,
    string AttachmentUrl
);

public record createAttachmentDto
(
    int ItemId,
    string AttachmentUrl
);

public record modifyAttachmentDto
(
    string AttachmentUrl
);
