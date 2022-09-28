using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.SizeNumProductData;

namespace tkytekstil.ENGINE.Interface
{
    public interface ISizeNumberProductService : ICrudService<SizeNumProduct, SizeNumProductDto>
    {
        List<SizeNumProductDto> listSizeNumProductToProduct(int Id);
        List<SizeNumProductDto> listSizeNumProductToSize(int? Id);

        SizeNumProductDto deleteSizeFromProduct(int sizeId, int productId);
    }
}
