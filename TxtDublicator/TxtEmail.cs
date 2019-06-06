using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace TxtDublicator
{
    class Txtemail : IComparable<Txtemail>
    {
        public Txtemail(string firtFirstmail, string domain)
        {
            this._firstmail = firtFirstmail;
            this._domain = domain;

        }

        public string _firstmail;
        public string _domain;

        public string RetOneString()
        {
            return _firstmail + "@" + _domain;
        }

        public override string ToString()
        {
            return _firstmail + "@" + _domain;
        }

        public void StringToLower()
        {
            _firstmail = _firstmail.ToLower();
            _domain = _domain.ToLower();
        }

        public void StringToUpper()
        {
            _firstmail = _firstmail.ToUpper();
            _domain = _domain.ToUpper();
        }



        public int CompareTo(Txtemail otherTxtemail)
        {
            var compare = 0;
            compare = string.Compare(this._firstmail, otherTxtemail._firstmail, false);
            compare = +compare;
            return compare;

        }

        
    }


    //class for sort
    class EmailComparer : IComparer<Txtemail>
    {
        public SortCriteria SortBy = SortCriteria.ByZtoA;

        public int Compare(Txtemail x, Txtemail y)
        {
            var compare = 0;
            if (SortBy == SortCriteria.ByZtoA)
            {
                compare = string.Compare(x.RetOneString(), y.RetOneString(), true);
                compare = -compare;
                return compare;
            }
            else if (SortBy == SortCriteria.ByDomain)
            {
                compare = string.Compare(x._domain, y._domain, false);
                compare = +compare;
                return compare;
            }

            return compare;
        }
    }

    //class for find and delete duplicates
    class EmailDuplicator : IEqualityComparer<Txtemail>
    {
        public bool Equals(Txtemail x, Txtemail y)
        {

            return string.Equals(x.RetOneString(), y.RetOneString(), StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(Txtemail obj)
        {
            return obj.RetOneString().GetHashCode();
        }
    }

    enum SortCriteria
    {
        ByAtoZ,
        ByZtoA,
        ByDomain,
    }


    class EmailDP
    {
        private List<Txtemail> _txtemail = new List<Txtemail>();

        public void Add(string path)
        {
            Regex lineget = new Regex(@"([a-z0-9_\.-]+)@([a-z0-9_\.-]+)$", RegexOptions.IgnoreCase);
            StreamReader readerlines = File.OpenText(path);
            string line;
            while ((line = readerlines.ReadLine()) != null)
            {
                Match match = lineget.Match(line);
                if (match.Success)
                {
                    string usern = match.Groups[1].Value;
                    string domain = match.Groups[2].Value;

                    _txtemail.Add(new Txtemail(usern, domain));
                }
            }
        }

        public int Count => _txtemail.Count;
        public IEnumerable<Txtemail> EmailTexts => _txtemail;



        //delete double in list
        public void DeleteDouble()
        {
            List<Txtemail> _txtemail2 = new List<Txtemail>(_txtemail.Distinct(new EmailDuplicator()));
            _txtemail.Clear();
            _txtemail = _txtemail2;


        }


        //Call methods sort
        public void Sort()
        {
            _txtemail.Sort();
        }
        public void Sort(SortCriteria sortby)
        {
            EmailComparer comparer = new EmailComparer();
            comparer.SortBy = sortby;
            _txtemail.Sort(comparer);
        }

        public void ClearList()
        {
            _txtemail.Clear();
        }


        public string ReturnString()
        {
            string tmpstr = string.Empty;
            foreach (var tx in EmailTexts)
            {
                tmpstr += tx.RetOneString() + "\n";
            }
            return tmpstr;
        }



        public string[] ToArray()
        {
            string[] arr = new string[_txtemail.Count];

            int i = 0;
            foreach (var tx in _txtemail)
            {
                arr[i] = tx.RetOneString();
                i++;
            }
            return arr;
        }
    }

}
