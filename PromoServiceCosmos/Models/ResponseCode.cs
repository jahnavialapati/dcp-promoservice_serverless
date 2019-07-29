using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace PromoServiceCosmos.Models
{
    public class ResponseCode
    {
        [JsonProperty(PropertyName = "correlationId")]
        public string correlationalId { get; set; }
        public int statusCode { get; set; }
        public string statusReason { get; set; }
        public bool success { get; set; }
        public string Id { get; set; }


        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
