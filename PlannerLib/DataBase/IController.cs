using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerLib.DataBase
{
    public interface IController<E, K>
    {
        E Reed(K id);

        int Create(E obj);

        int Delete(K id);

        int Update(E obj);

    }
}
