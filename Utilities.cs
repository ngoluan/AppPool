using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;

namespace AppPool
{
    class Utilities
    {
        Dictionary<string, string>[] apps = new Dictionary<string, string>[] 
        {
            new Dictionary<string, string>(),
            new Dictionary<string, string>(),
            new Dictionary<string, string>()
        };

        public Utilities()
        {

            apps[0]["shortName"] = "ArcGIS";
            apps[0]["fullName"] = "ArcGIS 10.2";
            apps[0]["location"] = "C:\\Program Files (x86)\\ArcGIS\\Desktop10.2\\bin\\ArcMap.exe";


            apps[1]["shortName"] = "GIMP";
            apps[1]["fullName"] = "GIMP";
            apps[1]["location"] = "C:\\Program Files\\GIMP 2\\bin\\gimp-2.8.exe";


            apps[2]["shortName"] = "Stata";
            apps[2]["fullName"] = "Stata";
            apps[2]["location"] = "C:\\Program Files (x86)\\Stata13\\StataSE-64.exe";
        }
        public void OpenApp(string appName)
        {
            Console.WriteLine("Openning {0}", appName);
            foreach (IDictionary<string, string> app in apps){
                if(app["shortName"]==appName){
                    try
                    {
                        Process.Start(app["location"]);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }
    }
}
