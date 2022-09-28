using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ProvinceData;

namespace tkytekstil.ENGINE.Interface
{
    public interface IProvinceService : ICrudService<Provinces, ProvinceDto>
    {
        List<ProvinceDto> provinceList(int Id);
    }
}
