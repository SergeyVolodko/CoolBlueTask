using System.Runtime.Serialization;

namespace CoolBlueTask.SalesCombinations
{
    [DataContract]
    public class SalesCombination
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "products")]
        public string Products { get; set; }
    }
}