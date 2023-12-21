using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotrabot
{
    public interface IEnum<T>
    {
        public T Value { get; }
    }
}
