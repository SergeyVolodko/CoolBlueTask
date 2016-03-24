using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CoolBlueTask.SalesCombinations
{
    [DataContract]
    public class SalesCombination
    {
        [DataMember(Name = "id")]
        public Guid Id { get; set; }

        [DataMember(Name = "products")]
        public IList<Guid> Products { get; set; }
    }
}