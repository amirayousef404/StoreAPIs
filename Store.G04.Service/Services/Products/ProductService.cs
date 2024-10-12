using AutoMapper;
using Store.G04.Core;
using Store.G04.Core.Dtos.Products;
using Store.G04.Core.Entities;
using Store.G04.Core.Helper;
using Store.G04.Core.Services.Contract;
using Store.G04.Core.Specifications;
using Store.G04.Core.Specifications.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PaginationResponse<ProductDto>> GetAllProductsAsync(ProductSpecParams productSpec)
        {
            var spec = new ProductSpecifications(productSpec);
            var products = await _unitOfWork.Repository<Product, int>().GetAllWithSpecAsync(spec);
            var mappedProducts = _mapper.Map<IEnumerable<ProductDto>>(products);

            var countSpec = new ProductWithCountSpecifications(productSpec);

            var count = await _unitOfWork.Repository<Product, int>().GetCountAsync(countSpec);

            return new PaginationResponse<ProductDto>(productSpec.PageSize, productSpec.PageIndex, count, mappedProducts);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var spec = new ProductSpecifications(id);
            return _mapper.Map<ProductDto>(await _unitOfWork.Repository<Product, int>().GetWhithSpecAsync(spec));
        }
        public async Task<IEnumerable<TypeBrandDto>> GetAllBrandsAsync()
        => _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductBrand, int>().GetAllAsync());

        

        public async Task<IEnumerable<TypeBrandDto>> GetAllTypesAsync()
        => _mapper.Map<IEnumerable<TypeBrandDto>>(await _unitOfWork.Repository<ProductType, int>().GetAllAsync());

    }
}
