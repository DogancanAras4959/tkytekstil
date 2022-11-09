using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.OrderData;

namespace tkytekstil.ENGINE.Interface
{
    public interface IOrderService : ICrudService<Order, OrderDto>
    {
        int insertOrder(OrderDto order);
        List<OrderDto> ordersByShopper();
        List<OrderDto> ordersByCompletedShopper();
        List<OrderDto> ordersToShopper(int shopperId);
    }
}
