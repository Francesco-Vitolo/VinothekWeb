using System.ComponentModel.DataAnnotations;

namespace VinothekManagerWeb.Models
{
    public class ImageModel
    {

        public ImageModel()
        {
        }

        public ImageModel(string filePath)
        {
            FilePath = filePath;
        }

        [Key]
        public int ImageId { get; set; }
        public string FilePath { get; set; }
        public virtual ProductModel? Product { get; set; }

    }
}
