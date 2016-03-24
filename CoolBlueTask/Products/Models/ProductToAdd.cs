using System.Runtime.Serialization;

namespace CoolBlueTask.Products.Models
{
    [DataContract]
    public class ProductToAdd
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}