using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PPM.Model
{
    public interface IEntityOperation<T>
    {
        public void AddOperation(T objectModule);
        public List<T> ListAll();
        public T ListById(int id);
        public void DeleteOperation(int id);
    }
}
