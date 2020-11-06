using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Visio;
using Visio = Microsoft.Office.Interop.Visio;

namespace VisioAddInTest
{
    class Parser
    {
        public Parser()
        {

        }

        public static void Run(Shapes shapes, List<Term> terms)
        {
            ShapesToTerms(shapes, terms);

            foreach (Microsoft.Office.Interop.Visio.Shape shape in shapes)
            {
                SetConnectsBetweenTerms(shape, terms);
            }
        }

        private static void ShapesToTerms(Shapes shapes, List<Term> terms)
        {
            foreach (Microsoft.Office.Interop.Visio.Shape shape in shapes)
            {
                var shapeCustomType = GetShapeCustomType(shape);
                var shapeTime = GetShapeTime(shape);
                terms.Add(new Term(shape.ID, shape.NameU, shapeCustomType, shape.Text, shapeTime));
            }

        }

        private static double GetShapeTime(Shape shape)
        {
            string customPropertyTime = VisioHelper.ReadANamedCustomProperty(shape, "Time", false);
            return customPropertyTime != "" ? Double.Parse(customPropertyTime) : 0;
        }

        private static string GetShapeCustomType(Visio.Shape shape)
        {
            string UmlElementType = VisioHelper.ReadANamedCustomProperty(shape, "Type", false);
            var type = "";

            if (UmlElementType.Equals("Start"))
            {
                type = VisioHelper.ReadANamedCustomProperty(shape, "Type", false);
            }
            else if (UmlElementType.Equals("End"))
            {
                type = VisioHelper.ReadANamedCustomProperty(shape, "Type", false);
            }
            else if (UmlElementType.Equals("Rel"))
            {
                type = VisioHelper.ReadANamedCustomProperty(shape, "Type", false);
            }
            else if (UmlElementType.Equals("Action"))
            {
                type = VisioHelper.ReadANamedCustomProperty(shape, "Type", false);
            }
            else if (UmlElementType.Equals("ParallelGateway"))
            {
                type = VisioHelper.ReadANamedCustomProperty(shape, "Type", false);
            }
            else if (UmlElementType.Equals("InclusiveGateway"))
            {
                type = VisioHelper.ReadANamedCustomProperty(shape, "Type", false);
            }
            else if (UmlElementType.Equals("ExclusiveGateway"))
            {
                type = VisioHelper.ReadANamedCustomProperty(shape, "Type", false);
            }

            return type;
        }

        private static string Translate(string sentence)
        {
            Dictionary<String, String> translateMap = new Dictionary<string, string>();
            translateMap.Add("Событие", "Event");
            translateMap.Add("Соединяющий объект", "Connecting Object");
            translateMap.Add("Нет", "None");
            translateMap.Add("Начальное событие", "Start");
            translateMap.Add("Поток управления", "Dynamic Connector");
            translateMap.Add("Подпроцесс", "Task");
            translateMap.Add("Завершающее событие", "End");

            return translateMap.ContainsKey(sentence) ? translateMap[sentence] : "";
        }

        private static void SetConnectsBetweenTerms(Shape shape, List<Term> terms)
        {
            var term = terms.Find(t => t.Id == shape.ID);

            foreach (Visio.Connect c in shape.Connects)
            {
                if (c.FromCell.Name.Equals("EndX"))
                {
                    var nextId = c.ToSheet.ID;
                    term.NextTermsId.Add(nextId);

                    var nextTerm = terms.Find(t => t.Id == nextId);
                    nextTerm.PrevTermsId.Add(term.Id);

                }
                else if (c.FromCell.Name.Equals("PinX") || c.FromCell.Name.Equals("BeginX"))
                {
                    var prevId = c.ToSheet.ID;
                    var prevTerm = terms.Find(t => t.Id == prevId);
                    prevTerm.NextTermsId.Add(term.Id);

                    term.PrevTermsId.Add(prevId);
                }
            }
        }
    }
}
