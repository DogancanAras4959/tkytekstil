using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.CORE.Repository;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.CategoryData;
using tkytekstil.ENGINE.Interface;

namespace tkytekstil.ENGINE.Engines
{
    public class CategoryService : CrudService<Categories, CategoriesDto>, ICategoryService
    {
        public CategoryService(IRepository<Categories> repository, IMapper mapper) : base(repository, mapper)
        {

        }
    }
}
