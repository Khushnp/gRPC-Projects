using Grpc.Core;
using gRPCService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensibility;

namespace gRPC_Server
{
    public class ProductService : ProductSrv.ProductSrvBase
    {
        private readonly ProductDbContext _Context;
        public ProductService(ProductDbContext context)
        {
            _Context = context;
        }
        public override Task<Products> GetAll(Empty request,ServerCallContext context)
        {
            var responce = new Products();
            var products = (from c in _Context.Product
                            select new Product
                            {
                                Id = c.Id,
                                Name = c.Name,
                                Price = c.Price,
                            }).ToArray();
            responce.Items.AddRange(products);
            return Task.FromResult(responce);
        }
        public override Task<Product> GetById(ProductRowIdFilter request,ServerCallContext context)
        {
            var product = _Context.Product.Where(w=>w.Id==request.ProductRowId).FirstOrDefault();
            var sProduct = new Product
            {
                Name = product.Name,
                Id = product.Id,
                Price = product.Price,

            };
            return Task.FromResult(sProduct);   
        }
        public override Task<Product> Post(Product request ,ServerCallContext context)
        {
            var Product = new gRPCService.Entity.Product()
            {
                Name = request.Name,
                Price = request.Price,
                Id= request.Id,
            };
            var responce = _Context.Product.Add(Product);
            _Context.SaveChanges();
            var resProduct = new Product()
            {
                Name = responce.Entity.Name,
                Price = responce.Entity.Price,
                Id = responce.Entity.Id,
            };
            return Task.FromResult(resProduct); 
        }

        public override Task<Product> Put(Product request,ServerCallContext context)
        {
            var Product = _Context.Product.Where(w=>w.Id== request.Id).FirstOrDefault();
            if (Product == null)
                return Task.FromResult<Product>(null);
            Product.Id = request.Id;
            Product.Name = request.Name;
            Product.Price = request.Price;
            _Context.Update(Product);
            _Context.SaveChanges();

            var sProduct = new Product
            {
                Id = request.Id,
                Name = request.Name,
                Price = request.Price,
            };
            return Task.FromResult(sProduct);
        }

        public override Task<Empty> Delete(ProductRowIdFilter request,ServerCallContext context)
        {
            var Product = _Context.Product.Where(w => w.Id == request.ProductRowId).FirstOrDefault();
            if (Product == null)
                return Task.FromResult<Empty>(null);
            _Context.Remove(Product);
            _Context.SaveChanges();
            return Task.FromResult<Empty>(new Empty());
        }

    }
}