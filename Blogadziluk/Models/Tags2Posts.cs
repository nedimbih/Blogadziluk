namespace Blogadziluk.Models {
    public class Tags2Posts {
        public string PostId { get; set; }
        public Post Post { get; set; }

        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
