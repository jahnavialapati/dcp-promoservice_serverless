using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PromoServiceCosmos.Models
{
    public class ProductPromoCondition
    {
        public ProductPromoParameter parameter { get; set; }

        public ProductPromoOperator promoOperator { get; set; }

        public float conditionValue { get; set; }

        public float otherValue { get; set; }

        public override string ToString()
        {
            // return JsonConvert.DeserializeObject(this);
            return JsonConvert.SerializeObject(this);
        }

    }
}
