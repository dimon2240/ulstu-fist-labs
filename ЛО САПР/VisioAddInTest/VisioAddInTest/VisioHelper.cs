using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisioAddInTest
{
    class VisioHelper
    {
        public static string ReadANamedCustomProperty(Microsoft.Office.Interop.Visio.Shape customPropertyShape, string cellName, bool isLocalName)
        {
            Microsoft.Office.Interop.Visio.Cell customPropertyCell = null;
            if (customPropertyShape != null || cellName != null)
            {
                const string CUST_PROP_PREFIX = "Prop.";
                string propName;
                try
                {
                    if (cellName.Length == 0)
                    {
                        throw new System.ArgumentNullException("cellName", "Zero length string input.");
                    }

                    propName = CUST_PROP_PREFIX + cellName;

                    if (isLocalName)
                    {
                        if (customPropertyShape.get_CellExists(propName, (short)Microsoft.Office.Interop.Visio.VisExistsFlags.visExistsAnywhere) != 0)
                        {

                            customPropertyCell = customPropertyShape.get_Cells(propName);
                        }
                    }
                    else if (customPropertyShape.get_CellExistsU(propName, (short)Microsoft.Office.Interop.Visio.VisExistsFlags.visExistsAnywhere) != 0)
                    {

                        customPropertyCell = customPropertyShape.get_CellsU(propName);
                    }
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message);
                    throw;
                }
            }
            return (customPropertyCell != null) ? customPropertyCell.get_ResultStr(Microsoft.Office.Interop.Visio.VisUnitCodes.visNoCast) : "";
        }

        public static void changeColor(Microsoft.Office.Interop.Visio.Shape shape, String formula)
        {
            try
            {
                shape.Cells["LineColor"].FormulaU = formula;
            }
            catch
            {
            }
            foreach (Microsoft.Office.Interop.Visio.Shape subShape in shape.Shapes)
            {
                try
                {
                    subShape.Cells["LineColor"].FormulaForceU = formula;
                }
                catch
                {
                }
            }
        }
    }
}
