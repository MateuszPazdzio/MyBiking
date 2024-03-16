using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBiking.Entity.Constants
{
    public class Month
    {

        public static Dictionary<string, int> Months = new Dictionary<string, int>()
        {
            {"January",1},
            {"February",2 },
            {"March",3 },
            {"April",4 },
            {"May" ,5},
            {"June",6},
            {"July",7 },
            { "August",8 },
            {"September",9 },
            {"October",10 },
            {"November" ,11},
            {"December",12 },
        };

        //public static Dictionary<int, string> Months = new Dictionary<int, string>()
        //{
        //    {1,"January" },
        //    {2,"February" },
        //    {3,"March" },
        //    {4,"April" },
        //    {5,"May" },
        //    {6,"June" },
        //    {7,"July" },
        //    {8,"August" },
        //    {9,"September" },
        //    {10,"October" },
        //    {11,"November" },
        //    {12,"December" },
        //};

    }
}
