using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ColorProductData;

namespace tkytekstil.ENGINE.Interface
{
    public interface IColorProductService : ICrudService<ColorProducts, ColorProductDto>
    {

        List<ColorProductDto> colorToProductId(int Id);
        List<ColorProductDto> colorToColorId(int id);
        ColorProductDto deleteColorFromProduct(int colorId, int productId);
    }
}
