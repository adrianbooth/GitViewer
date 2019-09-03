namespace GitViewer.Repositories.Entities
{
    /// <summary>
    /// If this were a long lived application I would use [JsonProperty]
    /// attribute with the response property and map it to a Pascal case .net style property to ease any mapping 
    /// </summary>
    public class RepositoryLicenseEntity
    {
        public string key { get; set; }
        public string name { get; set; }
        public string spdx_id { get; set; }
        public string url { get; set; }
        public string node_id { get; set; }
    }
}