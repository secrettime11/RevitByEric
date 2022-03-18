using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.FindDwg
{
    public class Model
    {
        public string name { get; set; }
        public string position { get; set; }
        public string project { get; set; }

        public static List<string> head = new List<string> { "CAD 圖檔名稱", "所在位置","專案名稱" };
        public static DataTable result_import { get; set; }
        public static DataTable result_link { get; set; }
    }
}
