using System;
using System.Runtime.Serialization;

namespace CoolBlueTask.Products.Models
{
    [DataContract]
    public class ProductWriteDto
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "price")]
        public decimal Price { get; set; }

        public static explicit operator Product(ProductWriteDto writeDto)
        {
            return new Product
            {
                Description = writeDto.Description,
                Name = writeDto.Name,
                Price = writeDto.Price
            };
        }

        public static explicit operator ProductWriteDto(Product product)
        {
            return new ProductWriteDto
            {
                Description = product.Description,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}