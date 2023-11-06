using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3.DAO
{
    public class SBMFactory
    {
        public static IDAO CreateSBMDAO(string fileName)
        {
            return new SBMDAO(fileName);
        }
    }
}
