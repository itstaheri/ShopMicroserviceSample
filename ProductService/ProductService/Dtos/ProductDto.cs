﻿namespace ProductService.Dtos
{
    public class ProductDto
    {
        public Guid PoductId { get; set; }
        public string ProductName { get;  set; }
        public Guid ProductCode { get;  set; }
        public string Description { get;  set; }
        public double Price { get;  set; }
        public bool IsInStock { get;  set; }
        public int QuantityInStock { get;  set; }
        public Guid CategoryId { get;  set; }
    }
}
