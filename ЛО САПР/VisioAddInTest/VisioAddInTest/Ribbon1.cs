using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Visio;
using Microsoft.Office.Tools.Ribbon;
using System.Windows.Forms;
using Visio = Microsoft.Office.Interop.Visio;

namespace VisioAddInTest
{
    public partial class Ribbon1
    {
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }


        private void Analyze_Click(object sender, RibbonControlEventArgs e)
        {
            bool flagInclusiveGateway = false;
            int count = 0;
            bool flagEnd = true;
            var shapes = VisioAddInTest.Globals.ThisAddIn.Application.ActivePage.Shapes;
            var terms = new List<Term>();
            var errors = new List<Error>();
            Parser.Run(shapes, terms);
            Dictionary<int, int> CurrentVisitCount = new Dictionary<int, int>();
            Dictionary<int, int> TotalVisitCount = new Dictionary<int, int>();
            List<Term> CommonStack = new List<Term>();
            foreach (var term in terms)
            {
                CurrentVisitCount[term.Id] = 0;
                TotalVisitCount[term.Id] = 0;
            }
            var startTerms = terms.Where(t => t.Type.ToString().Equals(TermType.Start.ToString())).ToList();

            if (startTerms.Count == 0)
            {
                errors.Add(new Error(0, "Нет начального элемента"));
            }
            else {
                var currentTerm = startTerms[0];
                var state = 0;
                while (state != 7)
                {
                    currentTerm.isChecked = true;
                    switch (state)
                    {
                        case 0:
                            if (currentTerm.Type.ToString().Equals(TermType.Start.ToString()))
                            {
                                currentTerm.isChecked = true;
                                CommonStack.AddRange(startTerms);
                                state = 2;
                            }
                            break;
                        case 1:
                            if (currentTerm.Type.ToString().Equals(TermType.Rel.ToString()))
                            {
                                int nextIndex = currentTerm.GetNextTermId();
                                try
                                {
                                    currentTerm = terms.Where(t => t.Id == nextIndex).First();
                                }
                                catch
                                {
                                    errors.Add(new Error(currentTerm.Id, "Несоединенный элемент"));
                                    state = 6;
                                    break;
                                }
                                state = 4;
                            }
                            break;
                        case 2:
                            if (currentTerm.Type.ToString().Equals(TermType.Start.ToString()))
                            {
                                if (count > 1 && flagInclusiveGateway)
                                {
                                    CommonStack.Reverse();
                                    errors.Add(new Error(count, "Deadlock"));
                                    Console.WriteLine("Deadlock");
                                    state = 6;
                                    break;
                                }
                                currentTerm = CommonStack.Last();
                                if (currentTerm.GetNextIdIndex() >= currentTerm.NextTermsId.Count() - 1)
                                {
                                    CommonStack.Reverse();
                                    CommonStack.Remove(currentTerm);
                                    CommonStack.Reverse();
                                }
                                int nextIndex = currentTerm.GetNextTermId();
                                if (nextIndex == -1)
                                {
                                    state = 5;
                                    break;
                                }
                                try
                                {
                                    currentTerm = terms.Where(t => t.Id == nextIndex).First();
                                }
                                catch
                                {
                                    errors.Add(new Error(currentTerm.Id, "Несоединенный элемент"));
                                    state = 6;
                                    break;
                                }
                                state = 1;
                            }
                            if (currentTerm.Type.ToString().Equals(TermType.Action.ToString()) || currentTerm.Type.ToString().Equals(TermType.InterEventTimer.ToString()))
                            {
                                if (currentTerm.Type.ToString().Equals(TermType.Action.ToString()) && flagInclusiveGateway && count > 0)
                                {
                                    flagInclusiveGateway = false;
                                    count = 0;
                                }
                                currentTerm = CommonStack.Last();
                                if (currentTerm.GetNextIdIndex() >= currentTerm.NextTermsId.Count() - 1)
                                {
                                    CommonStack.Reverse();
                                    CommonStack.Remove(currentTerm);
                                    CommonStack.Reverse();
                                }
                                int nextIndex = currentTerm.GetNextTermId();
                                if (nextIndex == -1)
                                {
                                    state = 5;
                                    break;
                                }
                                try
                                {
                                    currentTerm = terms.Where(t => t.Id == nextIndex).First();
                                }
                                catch
                                {
                                    errors.Add(new Error(currentTerm.Id, "Несоединенный элемент"));
                                    state = 6;
                                    break;
                                }
                                state = 1;
                            }

                            if (currentTerm.Type.ToString().Equals(TermType.ExclusiveGateway.ToString()))
                            {
                                if (count > 1 && flagInclusiveGateway)
                                {
                                    CommonStack.Reverse();
                                    errors.Add(new Error(count, "Deadlock"));
                                    Console.WriteLine("Deadlock");
                                    state = 6;
                                    break;
                                }
                                currentTerm = CommonStack.Last();
                                if (currentTerm.GetNextIdIndex() >= currentTerm.NextTermsId.Count() - 1)
                                {
                                    CommonStack.Reverse();
                                    CommonStack.Remove(currentTerm);
                                    CommonStack.Reverse();
                                }
                                int nextIndex = currentTerm.GetNextTermId();
                                if (nextIndex == -1)
                                {
                                    state = 5;
                                    break;
                                }
                                try
                                {
                                    currentTerm = terms.Where(t => t.Id == nextIndex).First();
                                }
                                catch
                                {
                                    errors.Add(new Error(currentTerm.Id, "Несоединенный элемент"));
                                    state = 6;
                                    break;
                                }
                                state = 1;
                            }
                            if (currentTerm.Type.ToString().Equals(TermType.ParallelGateway.ToString()) || currentTerm.Type.ToString().Equals(TermType.InclusiveGateway.ToString()))
                            {
                                if (currentTerm.Type.ToString().Equals(TermType.InclusiveGateway.ToString()) && flagInclusiveGateway && count > 0)
                                {
                                    flagInclusiveGateway = false;
                                    count = 0;
                                }
                                else if (currentTerm.Type.ToString().Equals(TermType.ParallelGateway.ToString()) && count > 1 && flagInclusiveGateway)
                                {
                                    CommonStack.Reverse();
                                    errors.Add(new Error(count, "Deadlock"));
                                    Console.WriteLine("Deadlock");
                                    state = 6;
                                    break;
                                }
                                else if (currentTerm.Type.ToString().Equals(TermType.InclusiveGateway.ToString()))
                                {
                                    flagInclusiveGateway = true;
                                    count = currentTerm.Id;
                                }
                                var flag = false;
                                CommonStack.Reverse();
                                foreach (var sTerm in CommonStack)
                                {
                                    if ((sTerm.Type.ToString().Equals(TermType.ParallelGateway.ToString()) || sTerm.Type.ToString().Equals(TermType.InclusiveGateway.ToString())) && CurrentVisitCount[sTerm.Id] != TotalVisitCount[sTerm.Id])
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (sTerm.GetNextIdIndex() >= sTerm.NextTermsId.Count() - 1)
                                        {
                                            CommonStack.Remove(sTerm);
                                        }
                                        int nextIndex = sTerm.GetNextTermId();
                                        if (nextIndex == -1)
                                        {
                                            state = 5;
                                            flag = true;
                                            break;
                                        }
                                        if (flag)
                                        {
                                            break;
                                        }
                                        try
                                        {
                                            currentTerm = terms.Where(t => t.Id == nextIndex).First();
                                        }
                                        catch
                                        {
                                            errors.Add(new Error(sTerm.Id, "Несоединенный элемент"));
                                            state = 6;
                                            break;
                                        }
                                        state = 1;
                                        flag = true;
                                        break;
                                    }
                                }
                                CommonStack.Reverse();
                                if (flag)
                                {
                                    break;
                                }
                                CommonStack.Reverse();
                                errors.Add(new Error(currentTerm.Id, "Deadlock"));
                                Console.WriteLine("Deadlock");
                                state = 6;
                                break;
                            }
                            break;
                        case 3:
                            break;
                        case 4:
                            if (currentTerm.Type.ToString().Equals(TermType.Action.ToString()) || currentTerm.Type.ToString().Equals(TermType.InterEventTimer.ToString()))
                            {
                                CommonStack.Add(currentTerm);
                                if (CurrentVisitCount[currentTerm.Id] != 0 && CurrentVisitCount[currentTerm.Id] != TotalVisitCount[currentTerm.Id])
                                {
                                    CurrentVisitCount[currentTerm.Id]++;
                                }
                                if (CurrentVisitCount[currentTerm.Id] == 0)
                                {
                                    CurrentVisitCount[currentTerm.Id] = 1;
                                    TotalVisitCount[currentTerm.Id] = currentTerm.PrevTermsId.Count();
                                }
                                state = 2;
                            }
                            if (currentTerm.Type.ToString().Equals(TermType.ExclusiveGateway.ToString()))
                            {
                                CommonStack.Add(currentTerm);
                                if (CurrentVisitCount[currentTerm.Id] != 0 && CurrentVisitCount[currentTerm.Id] != TotalVisitCount[currentTerm.Id])
                                {
                                    CurrentVisitCount[currentTerm.Id]++;
                                }
                                if (CurrentVisitCount[currentTerm.Id] == 0)
                                {
                                    CurrentVisitCount[currentTerm.Id] = 1;
                                    TotalVisitCount[currentTerm.Id] = currentTerm.PrevTermsId.Count();
                                }

                                state = 2;
                            }
                            if (currentTerm.Type.ToString().Equals(TermType.ParallelGateway.ToString()) || currentTerm.Type.ToString().Equals(TermType.InclusiveGateway.ToString()))
                            {
                                if (!CommonStack.Contains(currentTerm))
                                {
                                    CommonStack.Add(currentTerm);
                                }
                                if (CurrentVisitCount[currentTerm.Id] != 0 && CurrentVisitCount[currentTerm.Id] != TotalVisitCount[currentTerm.Id])
                                {
                                    CurrentVisitCount[currentTerm.Id]++;
                                }
                                if (CurrentVisitCount[currentTerm.Id] == 0)
                                {
                                    CurrentVisitCount[currentTerm.Id] = 1;
                                    TotalVisitCount[currentTerm.Id] = currentTerm.PrevTermsId.Count();
                                }

                                state = 2;
                            }
                            if (currentTerm.Type.ToString().Equals(TermType.End.ToString()))
                            {
                                if (CurrentVisitCount[currentTerm.Id] != 0 && CurrentVisitCount[currentTerm.Id] != TotalVisitCount[currentTerm.Id])
                                {
                                    CurrentVisitCount[currentTerm.Id]++;
                                }
                                if (CurrentVisitCount[currentTerm.Id] == 0)
                                {
                                    CurrentVisitCount[currentTerm.Id] = 1;
                                    TotalVisitCount[currentTerm.Id] = currentTerm.PrevTermsId.Count();
                                }
                                state = 5;
                                flagEnd = false;
                            }
                            break;
                        case 5:
                            if (CommonStack.Count() > 0)
                            {
                                currentTerm = CommonStack.Last();
                                state = 2;
                                break;
                            }
                            state = 6;
                            break;
                        case 6:
                            var MissTerms = CurrentVisitCount.Where(t => TotalVisitCount[t.Key] != t.Value).ToDictionary(t => t.Key, t => t.Value);
                            if (MissTerms.Count() != 0)
                            {
                                errors.AddRange(MissTerms.Select(mt => new Error(mt.Key, "Неверное количество соединений")));
                            }
                            state = 7;
                            break;
                    }
                }
            }
            errors.AddRange(terms.Where(t => t.isChecked == false).Select(t => new Error(t.Id, "Несоединенный элемент")).ToList());
            if (flagEnd)
                errors.Add(new Error(1, "Нет завершающего элемента"));
            foreach (Microsoft.Office.Interop.Visio.Shape shape in shapes)
            {
                VisioHelper.changeColor(shape, "RGB(0,255,0)");
            }

            if (errors.Count() > 0)
            {
                var _form1 = new Form1(VisioAddInTest.Globals.ThisAddIn.Application.ActivePage, errors);
                _form1.Show();
            }
        }
    }
} 
