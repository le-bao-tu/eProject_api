﻿using Shared;

namespace Business.Product
{
    public interface IProductHandler
    {
        /// <summary>
        /// Lấy danh sách Product
        /// </summary>
        /// <returns></returns>
        Task<Response> getAllProduct(PageModel model);

        /// <summary>
        /// Lấy Product theo Id
        /// </summary>
        /// <returns></returns>
        Task<Response> getProductById(Guid? productId);

        /// <summary>
        /// Lọc Product
        /// </summary>
        /// <returns></returns>
        Task<Response> searchProduct(PageModel model, string? name, int? quantity, float? priceMin, float? priceMax, string? address, bool status, Guid? categoryId);


        /// <summary>
        /// thêm mới sản phẩm 
        /// </summary>
        /// <param name="ProductModel"></param>
        /// <returns></returns>

        Task<Response> CreateProduct(ProductCreateModel ProductModel);
        /// <summary>
        /// cập nhật sản phẩm
        /// </summary>
        /// <param name="ProductModel"></param>
        /// <returns></returns>
        Task<Response> UpdateProduct(ProductCreateModel ProductModel);


        /// <summary>
        /// xóa sản phẩm
        /// </summary>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        Task<Response> DeleteProduct(Guid? ProductId);
    }
}
