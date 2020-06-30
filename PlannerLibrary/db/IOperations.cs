using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerLibrary.db
{
    interface IOperations<T>
    {
        T Reed(int id);
        ICollection<T> Reed();
        void Update(T obj);
        void Delete(int id);
        void Add(T obj);

    }
}
