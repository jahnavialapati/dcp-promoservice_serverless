using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace PromoServiceCosmos.Models
{
    public class ProductPromo
    {

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        public string name { get; set; }


        public string code { get; set; }


        public string description { get; set; }


        public string imageRef { get; set; }

        public string tenant { get; set; }


        public DateTime fromDate { get; set; }

        public DateTime throughDate { get; set; }

        // [BsonElement("LimitPerCust")]
        public int useLimitPerCustomer { get; set; }


        // [BsonElement("LimitPerCode")]
        public int useLimitPerCode { get; set; }


        // [BsonElement("Condition")]
       public List<ProductPromoCondition> conditions { get; set; }


        // [BsonElement("action")]
       public ProductPromoAction action { get; set; }


        public override string ToString()
        {
            // return JsonConvert.DeserializeObject(this);
            return JsonConvert.SerializeObject(this);
        }

    }
}
