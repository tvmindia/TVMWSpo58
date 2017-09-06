using SPOffice.BusinessService.Contracts;
using SPOffice.DataAccessObject.DTO;
using SPOffice.RepositoryServices.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPOffice.BusinessService.Services
{
    public class FileUploadBusiness : IFileUploadBusiness
    {
        IFileUploadRepository _fileRepository;
        public FileUploadBusiness(IFileUploadRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
        public FileUpload InsertAttachment(FileUpload fileUploadObj)
        {
            return _fileRepository.InsertAttachment(fileUploadObj);
        }
        public List<FileUpload> GetAttachments(Guid ID)
        {
            return _fileRepository.GetAttachments(ID);
        }
        public object DeleteFile(Guid ID)
        {
            return _fileRepository.DeleteFile(ID);
        }
    }
}