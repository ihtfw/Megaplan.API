namespace Megaplan.API.Queries
{
    using System;
    using System.IO;
    using System.Net;

    using Newtonsoft.Json;

    public class Attachment
    {
        public Attachment()
        {
        }

        public Attachment(string path)
        {
            Name = Path.GetFileName(path);
            Content = Convert.ToBase64String(File.ReadAllBytes(path));
        }
        [JsonProperty("[Name]")]
        public string Name { get; set; }
        [JsonProperty("[Content]")]
        public string Content { get; set; }
    }
}