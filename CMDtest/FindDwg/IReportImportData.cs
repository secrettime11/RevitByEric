using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.FindDwg
{
    interface IReportImportData
    {
        bool init(string projectName);
        void startReportSection(string sectionName);
        void logItem(string item);
        void setWarning();
        void done();
        string getLogFileName();
    }
}
