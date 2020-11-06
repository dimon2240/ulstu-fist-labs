using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisioAddInTest
{
    public class Error
    {
        public int ShapeId { get; set; }
        public string Name { get; set; }

        public Error(int id, string name)
        {
            this.ShapeId = id;
            this.Name = name;
        }
    }
}
