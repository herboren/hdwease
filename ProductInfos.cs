namespace hddeserializer
{
    /// <summary>
    /// Class defines Product Information
    /// </summary>
    public class ProductInfos
    {
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int ProductID { get; set; } = 0;
        public string Keywords { get; set; } = string.Empty;

        public ProductInfos(string name, string desc, int id, string kw)
        {
            ProductName = name;
            Description = desc;
            ProductID = id;
            Keywords = kw;  
        }
    }
}
