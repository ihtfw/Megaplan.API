namespace Megaplan.API.Queries
{
    using System;
    using System.IO;
    using Newtonsoft.Json;

    public class Attachment
    {
        public Attachment()
        {
        }

#if !PCL
        public Attachment(string pathToFile)
        {
            Name = Path.GetFileName(pathToFile);
            Content = Convert.ToBase64String(File.ReadAllBytes(pathToFile));
        }
#endif

        public Attachment(string name, byte[] content)
        {
            Name = name;
            Content = Convert.ToBase64String(content);
        }

        [JsonProperty("[Name]")]
        public string Name { get; set; }
        [JsonProperty("[Content]")]
        public string Content { get; set; }
    }
}