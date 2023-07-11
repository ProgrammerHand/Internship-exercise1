using File_sending.Data;
using File_sending.Repository;
using File_sending.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace File_Sending_Tests
{
    public class FileTransferIntegrationTests
    {
        private (FileTransferRepository, FileTransferService) GetDependencies()
        {
            var csvHelperService = new CSVHelperService();
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var context = new AppDbContext(optionsBuilder.Options);
            var repository = new FileTransferRepository(context);
            var service = new FileTransferService(csvHelperService, repository);

            return (repository, service);
        }

        [Fact]
        public async Task GivenCSVFile_WhenNotInDatabase_ShouldBeAddedToDatabase()
        {
            //Arrange
            (var repository, var service) = GetDependencies();

            //Act
            using (var stream = System.IO.File.OpenRead("test2.csv"))
            {
                IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                await service.UploadFile(file);
                var result = await repository.UserFileInfoExist(file.FileName.Substring(0, file.FileName.LastIndexOf('.')));

                //Assert
                Assert.True(result);
            }            
        }
        [Fact]
        public async Task GivenCSVFile_WhenInDatabase_ShouldBeUpdatedToDatabase()
        {
            //Arrange
            (var repository, var service) = GetDependencies();

            //Act
            using (var stream = System.IO.File.OpenRead("test2.csv"))
            {
                IFormFile file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                await service.UploadFile(file);
                var inserted = await repository.GetUserFileInfo(file.FileName.Substring(0, file.FileName.LastIndexOf('.')));
                await service.UploadFile(file);
                var updated = await repository.GetUserFileInfo(file.FileName.Substring(0, file.FileName.LastIndexOf('.')));

                //Assert
                Assert.True(inserted.Updated != updated.Updated);


                // jeden rekord na bazie (nadpisywania jak jest ten sam name)
            }
        }
    }
}