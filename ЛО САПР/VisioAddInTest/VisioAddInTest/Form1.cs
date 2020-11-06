using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisioAddInTest
{
    public partial class Form1 : Form
    {
        private Microsoft.Office.Interop.Visio.Page _tarPage;
        private List<Error> _errors;

        public Form1(Microsoft.Office.Interop.Visio.Page targetPage, List<Error> errors)
        {
            InitializeComponent();

            _tarPage = targetPage;
            _errors = new List<Error>();
            _errors.AddRange(errors);
            var shapes = targetPage.Shapes;
            foreach (var err in _errors)
            {
                dataGridView1.Rows.Add(err.ShapeId, err.Name);
            }


            foreach (Microsoft.Office.Interop.Visio.Shape shape in shapes)
            {
                VisioHelper.changeColor(shape, "RGB(0,0,0)");
                foreach (var error in errors)
                {
                    if (shape.ID == error.ShapeId)
                    {
                        VisioHelper.changeColor(shape, "RGB(255,0,0)");
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
    }
}
