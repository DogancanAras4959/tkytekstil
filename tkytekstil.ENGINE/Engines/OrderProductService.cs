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
    public class OrderProductService : CrudService<OrderProductsDto, OrderProductDto>, IOrderProductsService
    {
        public OrderProductService(IRepository<OrderProductsDto> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public List<OrderProductDto> orderToProducts(int orderId)
        {
            var entity = _repository.Where(x => x.OrderId == orderId).Include("order").Include("product").ToList();
            var entityDto = _mapper.Map<List<OrderProductsDto>, List<OrderProductDto>>(entity);
            return entityDto;
        }
    }
}
