using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.CORE.Repository;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.OrderProductData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class OrderProductService : CrudService<OrderProducts, OrderProductsDto>, IOrderProductsService
    {
        public OrderProductService(IRepository<OrderProducts> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public List<OrderProductsDto> orderToProducts(int orderId)
        {
            var entity = _repository.Where(x => x.OrderId == orderId).Include("order").Include("product").ToList();
            var entityDto = _mapper.Map<List<OrderProducts>, List<OrderProductsDto>>(entity);
            return entityDto;
        }
    }
}
