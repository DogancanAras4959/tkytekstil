using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.CORE.Repository;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.BrandData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class BrandService : CrudService<Brands, BrandDto>, IBrandService
    {
        public BrandService(IRepository<Brands> repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}
