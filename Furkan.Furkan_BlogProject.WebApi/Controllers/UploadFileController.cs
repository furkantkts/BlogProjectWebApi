﻿using Furkan.Furkan_BlogProject.WebApi.Enums;
using Furkan.Furkan_BlogProject.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Furkan.Furkan_BlogProject.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        public async Task<UploadModel> UploadFileAsync(IFormFile file,string contentType)
        {
            UploadModel uploadModel = new UploadModel();

            if (file != null)
            {
                if (file.ContentType != contentType)
                {
                    uploadModel.ErrorMessage = "Uygunsuz Dosya Türü";
                    uploadModel.UploadState = UploadState.Error;
                    return uploadModel;
                }
                else
                {
                    var newName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(Directory.GetCurrentDirectory() + "/wwwroot/img/" + newName);
                    var stream = new FileStream(path, FileMode.Create);
                    await file.CopyToAsync(stream);

                    
                    uploadModel.UploadState = UploadState.Succes;
                    uploadModel.newName = newName;
                    return uploadModel;
                }
                
            }

            uploadModel.UploadState = UploadState.NotExits;
            uploadModel.ErrorMessage = "Dosya Yok";
            return uploadModel;

        }
    }
}
