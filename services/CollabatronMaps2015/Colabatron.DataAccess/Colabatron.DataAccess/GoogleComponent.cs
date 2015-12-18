using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colabatron.DataAccess
{
    public class GoogleComponent : IAmAPlace
    {
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Name { get; set; }
        public string GoogleId { get; set; }
        public string Icon { get; set; }
    }
}
