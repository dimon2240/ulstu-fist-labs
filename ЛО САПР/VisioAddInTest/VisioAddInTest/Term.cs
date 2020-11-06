using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisioAddInTest
{
    public class Term
    {
        public string name;
        public TermType type;
        private int NextId;

        public int Id { get; set; }
        public string Text { get; set; }
        public double Time { get; set; }
        public List<int> NextTermsId { get; set; }
        public List<int> PrevTermsId { get; set; }
        public List<int> Path { get; set; }
        public bool isChecked { get; set; }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                this.name = SetName(value);
            }
        }
       
        public string Type {
            get {
                return type.ToString();
            }
            set {
                this.type = this.SetType(value);
            }

        }
       
        public Term(int id, string name, string type, string text = "", double time = 0)
        {
            this.Id = id;
            this.Name = name;
            this.Text = text;
            this.Time = time;
            this.Type = type;
            this.NextTermsId = new List<int>();
            this.PrevTermsId = new List<int>();
            this.Path = new List<int>();
            this.NextId = 0;
            this.isChecked = false;
        }

        private string SetName(string value)
        {
            var dotIndex = value.IndexOf('.');
            string newName = dotIndex != -1 ? value.Substring(0, dotIndex) : value;
            return newName;
        }

        private TermType SetType(string value)
        {
            if (value.Equals("Start"))
                return TermType.Start;
            else if (value.Equals("End"))
                return TermType.End;
            else if (value.Equals("Таймер"))
                return TermType.InterEventTimer;
            else if (value.Equals("Action"))
                return TermType.Action;
            else if (value.Equals("Rel"))
                return TermType.Rel;
            else if (value.Equals("ParallelGateway"))
                return TermType.ParallelGateway;
            else if (value.Equals("InclusiveGateway"))
                return TermType.InclusiveGateway;
            else if (value.Equals("ExclusiveGateway"))
                return TermType.ExclusiveGateway;

            return TermType.None;
        }

        public int GetNextIdIndex()
        {
            return this.NextId;
        }

        public int GetNextTermId()
        {
            if (this.NextId > this.NextTermsId.Count() - 1)
            {
                return -1;
            }
            return this.NextTermsId[this.NextId++ % this.NextTermsId.Count()];
        }

        public override bool Equals(object obj)
        {
            var item = obj as Term;

            if (item == null)
            {
                return false;
            }

            return this.Id.Equals(item.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}
