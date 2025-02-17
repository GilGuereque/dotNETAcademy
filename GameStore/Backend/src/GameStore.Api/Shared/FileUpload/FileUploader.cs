namespace GameStore.Api.Shared.FileUpload;

public class FileUploader(
    IWebHostEnvironment environment,
    IHttpContextAccessor httpContextAccessor)
{
    public async Task<FileUploadResult> UploadFileAsync(
        IFormFile file, 
        string folder)
    {
        var result = new FileUploadResult();

        if (file == null || file.Length == 0)
        {
            result.IsSuccess = false;
            result.ErrorMessage = "No file uploaded";
            return result;
        }

        if (file.Length > 10 * 1024 * 1024) //converting to 10 megabytes since Length is counted as bytes by default
        {
            result.IsSuccess = false;
            result.ErrorMessage = "File is too large for uploading.";
            return result;
        }

        var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf"};
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (string.IsNullOrEmpty(fileExtensions) || !permittedExtensions.Contains(fileExtension))
        {
            result.IsSuccess = false;
            result.ErrorMessage = "This is an invalid file type.";
            return result;
        }

        var uploadFolder = Path.Combine(environment.WebRootPath, folder);
        if (!Directory.Exists(uploadFolder))
        {
            Directory.CreateDirectory(uploadFolder);
        }

        var safeFileName = $"{Guid.NewGuid(){fileExtension}}";
        var fullPath = Path.Combine(uploadFolder, safeFileName);

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        var httpContext = httpContextAccessor.HttpContext;
        var fileUrl = $"{httpContext?.Request.Scheme}://{httpContext?.Request.Host}/{folder}/{safeFileName}";

        result.IsSuccess = true;
        result.FileUrl = fileUrl;
        return result;
    }
}
