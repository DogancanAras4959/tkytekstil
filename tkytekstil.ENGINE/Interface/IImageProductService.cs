using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ImagesProductData;

namespace tkytekstil.ENGINE.Interface
{
    public interface IImageProductService : ICrudService<ImagesProduct, ImagesProductDto>
    {
        bool imageInsertInProduct(List<ImagesProductDto> images);

        List<ImagesProductDto> getToIntProduct(int Id);
    }
}
