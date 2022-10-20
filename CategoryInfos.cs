namespace hddeserializer
{
    /// <summary>
    /// Class defines Issue Information
    /// </summary>
    public class CategoryInfos
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int IssueId { get; set; }
        public int SeverityId { get; set; }

        public CategoryInfos(int productId, int categoryId, int issueId, int severityId)
        {
            this.ProductId = productId;
            this.CategoryId = categoryId;
            this.IssueId = issueId;
            this.SeverityId = severityId;
        }
    }
}
