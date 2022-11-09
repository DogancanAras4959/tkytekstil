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
using tkytekstil.ENGINE.Dtos.OrderData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class OrderService : CrudService<Order, OrderDto>, IOrderService
    {
        public OrderService(IRepository<Order> repository, IMapper mapper) : base(repository, mapper)
        {

        }

        public int insertOrder(OrderDto order)
        {
            var entity = _mapper.Map<OrderDto, Order>(order);
            _repository.Add(entity);
            _repository.Save();
            return entity.Id;
        }

        public List<OrderDto> ordersToShopper(int shopperId)
        {
            var entity = _repository.Where(x => x.ShopperId == shopperId).Include("shopper").ToList();
            var entityDto = _mapper.Map<List<Order>, List<OrderDto>>(entity);
            return entityDto;
        }

        public List<OrderDto> ordersByShopper()
        {
            var entity = _repository.Where(x => x.Id > 0).Include("shopper").ToList();
            var entityDto = _mapper.Map<List<Order>, List<OrderDto>>(entity);
            return entityDto;
        }

        public List<OrderDto> ordersByCompletedShopper()
        {
            var entity = _repository.Where(x => x.Id > 0).Include("shopper").ToList();
            var entityDto = _mapper.Map<List<Order>, List<OrderDto>>(entity);
            return entityDto;
        }
    }
}
