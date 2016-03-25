using System;
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

        [DataMember(Name = "price")]
        public decimal Price { get; set; }

        public static explicit operator Product(ProductToAdd toAdd)
        {
            return new Product
            {
                Description = toAdd.Description,
                Name = toAdd.Name,
                Price = toAdd.Price
            };
        }

        public static explicit operator ProductToAdd(Product product)
        {
            return new ProductToAdd
            {
                Description = product.Description,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}