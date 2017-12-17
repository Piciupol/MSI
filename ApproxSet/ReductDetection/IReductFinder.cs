using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReductDetection
{
    public interface IReductFinder
    {
        IList<int> GetReducts(Matrix matrix);
    }
}
