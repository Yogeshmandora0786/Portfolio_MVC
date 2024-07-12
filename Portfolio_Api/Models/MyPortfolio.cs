namespace Portfolio_Api.Models
{
    public class MyPortfolio
    {
        public long? Id { get; set; }
        public IFormFile? Portfolio_Image { get; set; }
        public string? Portfolio_Image_String { get; set; }
        public string? Portfolio_Title { get; set; }
        public string? Short_Description { get; set; }
        public DateTime? PublishedDate { get; set; }
        public byte? Visiable { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedBy { get; set; }
        public string? CreatedByName { get; set; }
    }
}
