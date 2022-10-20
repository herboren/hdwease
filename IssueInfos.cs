namespace hddeserializer
{
    /// <summary>
    /// Class defines Category Information
    /// </summary>
    public class IssueInfos
    {
        public int IssueId { get; set; }
        public string IssueName { get; set; }
        public string IssueDescription { get; set; }

        public IssueInfos (int issueId, string issueName, string issueDescription)
        {
            IssueId = issueId;
            IssueName = issueName;
            IssueDescription = issueDescription;
        }
    }    
}
