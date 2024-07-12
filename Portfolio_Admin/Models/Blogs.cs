namespace Portfolio_Admin.Models
{
    public class Blogs
    {
        public long? Id { get; set; }
        public IFormFile? Blog_Image { get; set; }
        public string? Blog_Image_String { get; set; }
        public string? Blog_Title { get; set; }
        public string? Blog_Content { get; set; }
        public string? Short_Description { get; set; }
        public DateTime? PublishedDate { get; set; }
        public byte? Visiable { get; set; }
        public long? TotalLikes { get; set; }
        public long? TotalComments { get; set; }
        public long? TotalShares { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedBy { get; set; }
    }


    public class BlogsDTO
    {
        public long? Id { get; set; }
        public IFormFile? Blog_Image { get; set; }
        public string? Blog_Image_String { get; set; }
        public string? Blog_Title { get; set; }
        public string? Blog_Content { get; set; }
        public string? Short_Description { get; set; }
        public DateTime? PublishedDate { get; set; }
        public byte? Visiable { get; set; }
        public long? TotalLikes { get; set; }
        public long? TotalComments { get; set; }
        public long? TotalShares { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? DeletedBy { get; set; }
        public string? CreatedByName { get; set; }
    }
}
