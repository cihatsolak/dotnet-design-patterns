using Microsoft.Extensions.Caching.Memory;

namespace MembershipSystem.Decorator.Repositories.Decorators
{
    public class ProductRepositoryCacheDecorator : BaseProductRepositoryDecorator
    {
        private const string ProductsCacheName = "products";

        private readonly IMemoryCache _memoryCache;

        public ProductRepositoryCacheDecorator(
            IProductRepository productRepository,
            IMemoryCache memoryCache) : base(productRepository)
        {
            _memoryCache = memoryCache;
        }

        public async override Task<List<Product>> GetAll()
        {
            if (_memoryCache.TryGetValue(ProductsCacheName, out List<Product> cacheProducts))
            {
                return cacheProducts;
            }

            await UpdateCache();

            return _memoryCache.Get<List<Product>>(ProductsCacheName);
        }

        public async override Task<List<Product>> GetAll(string userId)
        {
            return await GetAll(userId);
        }

        public async override Task<Product> Save(Product product)
        {
            await base.Save(product);

            await UpdateCache();
            return product;
        }

        public async override Task Update(Product product)
        {
            await base.Update(product);

            await UpdateCache();
        }

        public async override Task Remove(Product product)
        {
            await base.Remove(product);
            await UpdateCache();
        }

        private async Task UpdateCache()
        {
            _memoryCache.Set(ProductsCacheName, await base.GetAll());
        }
    }
}
