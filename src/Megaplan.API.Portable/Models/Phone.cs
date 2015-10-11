using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Megaplan.API.Enums;
using Newtonsoft.Json;

namespace Megaplan.API.Models
{
    public class Phone
    {
        [JsonProperty("Phone")]
        public string Number { get; set; }

        [JsonProperty("PhoneType")]
        public PhoneType? Type { get; set; }

        [JsonProperty("PhoneComment")]
        public string Comment { get; set; }
    }
}
