using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemanticKernel.FakeAPI.Dto_s
{
    public class WatchList
    {
        public WatchList(string name)
        {
            Name = name;
            Movies = new List<Movie>();
        }

        public string Name { get; set; }

        public List<Movie> Movies { get; set; }
    }
}
