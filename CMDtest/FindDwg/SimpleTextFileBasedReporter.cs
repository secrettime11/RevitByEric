using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDtest.FindDwg
{
    class SimpleTextFileBasedReporter: IReportImportData
    {
        public SimpleTextFileBasedReporter()
        {
        }
        public bool init(string projectFileName)
        {
            bool outcome = false;
            m_currentSection = null;
            m_warnUser = false;

            if (0 != projectFileName.Length)
            {
                m_projectFileName = projectFileName;
            }
            else
            {
                m_projectFileName = "Default";
            }

            m_logFileName = System.IO.Path.Combine(
              System.IO.Path.GetDirectoryName(m_projectFileName),
              System.IO.Path.GetFileNameWithoutExtension(
                m_projectFileName)) + "-ListOfImportedData.txt";

            // Construct log file name from projectFileName 
            // and try to open file. Project file name is 
            // assumed to be valid (expected to be called 
            // on an open doc).

            try
            {
                m_outputFile = new StreamWriter(m_logFileName);
                m_outputFile.WriteLine("List of imported CAD data in "
                  + projectFileName);
                outcome = true;
            }
            catch (System.UnauthorizedAccessException)
            {
                TaskDialog.Show("FindImports",
                  "You are not authorized to create "
                    + m_logFileName);
            }
            catch (System.ArgumentNullException) // oh, come on.
            {
                TaskDialog.Show("FindImports",
                  "That's just not fair. Null argument for StreamWriter()");
            }
            catch (System.ArgumentException)
            {
                TaskDialog.Show("FindImports",
                  "Failed to create " + m_logFileName);
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                TaskDialog.Show("FindImports",
                  "That's not supposed to happen: directory not found: "
                  + System.IO.Path.GetDirectoryName(m_projectFileName));
            }
            catch (System.IO.PathTooLongException)
            {
                TaskDialog.Show("FindImports",
                  "The OS thinks the file name " + m_logFileName
                  + " is too long");
            }
            catch (System.IO.IOException)
            {
                TaskDialog.Show("FindImports",
                  "An IO error has occurred while writing to "
                  + m_logFileName);
            }
            catch (System.Security.SecurityException)
            {
                TaskDialog.Show("FindImports",
                  "The OS thinks your access rights to "
                  + System.IO.Path.GetDirectoryName(m_projectFileName)
                  + " are insufficient");
            }
            return outcome;
        }
        public void startReportSection(string sectionName)
        {
            endReportSection();
            m_outputFile.WriteLine();
            m_outputFile.WriteLine(sectionName);
            m_outputFile.WriteLine();

            m_currentSection = sectionName;
        }

        public void logItem(string item)
        {
            m_outputFile.WriteLine(item);
        }

        public void setWarning()
        {
            m_warnUser = true;
        }

        public void done()
        {
            endReportSection();
            m_outputFile.WriteLine();
            m_outputFile.WriteLine("The End");
            m_outputFile.WriteLine();
            m_outputFile.Close();

            // Display "done" dialog, potentially open log file

            TaskDialog doneMsg = null;

            if (m_warnUser)
            {
                doneMsg = new TaskDialog(
                  "Potential issues found. Please review the log file");
            }
            else
            {
                doneMsg = new TaskDialog(
                  "FindImports completed successfully");
            }

            doneMsg.AddCommandLink(
              TaskDialogCommandLinkId.CommandLink1,
              "Review " + m_logFileName);

            switch (doneMsg.Show())
            {
                default:
                    break;

                case TaskDialogResult.CommandLink1:
                    // Display the log file
                    Process.Start("notepad.exe", m_logFileName);
                    break;
            }
        }

        public string getLogFileName()
        {
            return m_logFileName;
        }

        private void endReportSection()
        {
            if (null != m_currentSection)
            {
                m_outputFile.WriteLine();
                m_outputFile.WriteLine("End of "
                  + m_currentSection);
                m_outputFile.WriteLine();
            }
        }

        private string m_projectFileName;
        private string m_logFileName;
        private StreamWriter m_outputFile;
        private string m_currentSection;

        /// <summary>
        /// Tell the user to review the log file
        /// </summary>
        private bool m_warnUser;
    }
}
