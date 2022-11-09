using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.OrderProductData;

namespace tkytekstil.ENGINE.Interface
{
    public interface IOrderProductsService : ICrudService<OrderProducts, OrderProductsDto>
    {
        List<OrderProductsDto> orderToProducts(int orderId);
    }
}
