using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerLibrary.db
{
    class OperationsDb<T> : IOperations<T>
    {
        public void Add(T obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public T Reed(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> Reed()
        {
            throw new NotImplementedException();
        }

        public void Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
