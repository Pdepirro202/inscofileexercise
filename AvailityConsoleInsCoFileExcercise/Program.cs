using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvailityConsoleInsCoFileExcercise
{
    class Program
    {
        static void Main(string[] args)
        {

            var historicalData = File.ReadAllLines("Files/MemberUpdates06012019.csv").Skip(1);

            var MemberList = (from historyPrice in historicalData
                              let data = historyPrice.Split(',')
                              orderby data[2], data[1], data[3] descending
                        
                              select new
                              {
                                  UserID = data[0],
                                  FirstName = data[1],
                                  LastName = data[2],
                                  Version = data[3],
                                  InsuranceCompany = data[4],

                              }).GroupBy(x => x.UserID).Select(y => y.First());
            //prompt the user to enter the output path

            Console.WriteLine("Enter the Output location for the the Enrollment Files: ");

            var OutputLoc = (Console.ReadLine());
            
            //validate that the path ends with a backslash
            var PathCheck = OutputLoc[OutputLoc.Length - 1].ToString();

            if (PathCheck == "\\")
            {
                //do nothing
            }
            else
            {
                OutputLoc = OutputLoc + @"\";
                    }
            //identify the rows that meet the length criteria and write the file to the output directory
            foreach (var listedmember in MemberList)
            {

                //Identify rows in the Data that meet the Linq criteria and write the file to the output directory 
                string[] lines = { listedmember.UserID + "|" + listedmember.FirstName + "|" + listedmember.LastName + "|" + listedmember.Version + "|" + listedmember.InsuranceCompany };
                
                //write the file to the output directory
                               File.AppendAllLines(OutputLoc + listedmember.InsuranceCompany + DateTime.Now.ToString("_" + "MM.dd.yyyy.h.m.tt") + ".txt", lines);
            }

        }
    }
}
