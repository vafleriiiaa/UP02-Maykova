using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP02_Maykova
{
    internal class MaterialDeffect
    {
        public static double? MatDef(int productId, int materialType, int quantity, double par1, double par2)
        {
            double? result;
            var TypeProduct = Entities.GetContext().Product_type.Where(x => x.ID_type_product == productId);
            var typeMaterial = Entities.GetContext().Material_type.Where(x => x.ID_type_material == materialType);
            if (TypeProduct.Count() < 1 | typeMaterial.Count() < 1 | quantity < 1 | par1 <= 0 | par2 <= 0)
            {
                return -1;
            }
            else
            {
                double coeff = (double)TypeProduct.Select(x => x.Сoefficient_product).FirstOrDefault();
                double? materCount = quantity + quantity * typeMaterial.Select(x => x.Procent_material).FirstOrDefault();
                result = materCount * par1 * par2 * coeff;
                return result;
            }

        }
    }
}
