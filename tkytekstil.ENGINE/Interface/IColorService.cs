using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tkytekstil.DAL.Models;
using tkytekstil.ENGINE.Crud;
using tkytekstil.ENGINE.Dtos.ColorData;

namespace tkytekstil.ENGINE.Interface
{
    public interface IColorService : ICrudService<Colors, ColorDto>
    {
        ColorDto getColorByName(string name);
    }
}
