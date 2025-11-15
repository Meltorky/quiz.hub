using Microsoft.AspNetCore.Http;
using quiz.hub.Application.Common.Exceptions;
using quiz.hub.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Helpers
{
    public static class HandleImageFileHelper
    {
        public static async Task<byte[]> HandleImage(this IFormFile file)
        {
            if (!ImgFileValidator(file))
                throw new ValidationException(ImageFileOptions.ErrorMessage);

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            return stream.ToArray();
        }

        public static bool ImgFileValidator(IFormFile imageFile)
        {
            return (imageFile.Length > Convert.ToInt32(ImageFileOptions.MaxSizeInMB) * 1048576 ||
                !ImageFileOptions.AllowedExtentions.Contains(Path.GetExtension(imageFile.FileName).ToLower())) ?
                false : true;
        }
    }
}
