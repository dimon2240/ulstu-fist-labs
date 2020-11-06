using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisioAddInTest
{
    public partial class FormErrors : Form
    {
        private Microsoft.Office.Interop.Visio.Page _tarPage;
        private List<Error> _errors;

        public FormErrors(Microsoft.Office.Interop.Visio.Page targetPage, List<Error> errors)
        {
            //InitializeComponent();
            _tarPage = targetPage;
            _errors.AddRange(errors);

            foreach (var err in _errors)
            {
                //dataGridView1.Rows.Add(err.descr, err.Id);
            }

        }

        //private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    if (e.RowIndex == -1)
        //        return;
        //    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
        //    string idShape = row.Cells[1].Value.ToString();

        //    Visio.Shape _shape = _tarPage.Shapes.get_ItemFromID(Int32.Parse(idShape));

        //    foreach (Visio.Shape shape in _tarPage.Shapes)
        //    {
        //        var l = _error.Descendants().Where(r => r.Name.LocalName == "error"
        //            && r.Element("VisioLinkId").Value == idShape && (r.Element("Id").Value == shape.ID.ToString()
        //            || r.Element("VisioLinkId").Value == shape.ID.ToString())).Select(r => r).ToList();

        //        if (l.Count > 0)
        //        {
        //            shape.Application.ActiveWindow.CenterViewOnShape(shape, Visio.VisCenterViewFlags.visCenterViewSelectShape);
        //            ThisAddIn.changeColor(shape, "RGB(255,0,0)");
        //        }
        //        else
        //        {
        //            ThisAddIn.changeColor(shape, "RGB(0,0,0)");
        //        }
        //    }
        //    //System.Text.StringBuilder messageBoxCS = new System.Text.StringBuilder();
        //    //messageBoxCS.AppendFormat("{0} = {1}", "RowIndex", e.RowIndex);
        //    //messageBoxCS.AppendLine();
        //    //MessageBox.Show(messageBoxCS.ToString(), "CellMouseClick Event");
        //}
    }
}
