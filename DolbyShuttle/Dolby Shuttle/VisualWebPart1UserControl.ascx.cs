using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;
using System.Collections.Generic;
using Microsoft.SharePoint;
using System.Drawing;
using System.Xml;


class times
{
    public int start = 0;
    public int end = 0;
    public string color;
}


namespace DolbyShuttle.VisualWebPart1
{

    class times
    {
        public int start = 0;
        public int end = 0;
        public string color;
    }

    public partial class VisualWebPart1UserControl : UserControl
    {
        XmlTextReader reader; // may not use this now - as thinking performance will be better with a cobtant table
        
        DateTime currentTime;
        
        /* String Array containing the schedule - Fields are as follows::
         * Color - BLUE, GREEN, ORANGE, RED which represent the route
         * Whether its a schedule for everyday ALL, Monday-Thursday MTH, or Friday only FRI
         * Number of items in the record (i.e. how many stops
         * Then the remaining fields are a combination of STOP, TIME (in hhmm format) */

        string[,] schedule =
        {                    
            {"BLUE","ALL","4","BART","0650","999","0659","100","0701","BART","0710","","","",""},
            {"BLUE","ALL","4","BART","0712","999","0721","100","0723","BART","0732","","","",""},
            {"BLUE","ALL","4","BART","0734","999","0743","100","0745","BART","0754","","","",""},
            {"BLUE","ALL","4","BART","0756","999","0805","100","0807","BART","0816","","","",""},
            {"BLUE","ALL","4","BART","0818","999","0827","100","0829","BART","0838","","","",""},
            {"BLUE","ALL","4","BART","0840","999","0849","100","0851","BART","0900","","","",""},
            {"BLUE","ALL","4","BART","0902","999","0911","100","0913","BART","0922","","","",""},
            {"BLUE","ALL","3","BART","0924","999","0933","100","0935","","","","","",""},
            {"BLUE","MTH","4","100","1635","999","1637","BART","1646","100","1655","","","",""},
            {"BLUE","MTH","4","100","1657","999","1659","BART","1708","100","1717","","","",""},
            {"BLUE","MTH","4","100","1719","999","1721","BART","1730","100","1739","","","",""},
            {"BLUE","MTH","4","100","1741","999","1743","BART","1752","100","1801","","","",""},
            {"BLUE","MTH","4","100","1803","999","1805","BART","1814","100","1823","","","",""},
            {"BLUE","MTH","4","100","1825","999","1827","BART","1836","100","1845","","","",""},
            {"BLUE","MTH","4","100","1847","999","1849","BART","1858","100","1907","","","",""},
            {"BLUE","MTH","3","100","1909","999","1911","BART","1920","","0000","","","",""},
            {"BLUE","FRI","4","100","1535","999","1537","BART","1546","100","1555","","","",""},
            {"BLUE","FRI","4","100","1557","999","1559","BART","1608","100","1617","","","",""},
            {"BLUE","FRI","4","100","1619","999","1621","BART","1630","100","1639","","","",""},
            {"BLUE","FRI","4","100","1641","999","1643","BART","1652","100","1701","","","",""},
            {"BLUE","FRI","4","100","1703","999","1705","BART","1714","100","1723","","","",""},
            {"BLUE","FRI","4","100","1725","999","1727","BART","1736","100","1745","","","",""},
            {"BLUE","FRI","4","100","1747","999","1749","BART","1758","100","1807","","","",""},
            {"BLUE","FRI","3","100","1809","999","1811","BART","1820","","","","","",""},


            {"GREEN","ALL","5","One","0710","Transbay","0716","999","0728","475","0734","One","0746","",""},
            {"GREEN","ALL","5","One","0750","Transbay","0756","999","0808","475","0814","One","0826","",""},
            {"GREEN","ALL","5","One","0830","Transbay","0836","999","0848","475","0854","One","0906","",""},
            {"GREEN","ALL","5","One","0910","Transbay","0916","999","0928","475","0934","One","0946","",""},
            {"GREEN","ALL","4","One","0950","Transbay","0956","999","1008","475","1014","","","",""},

            {"GREEN","MTH","5","999","1632","475","1638","One","1650","Transbay","1656","999","1708","",""},
            {"GREEN","MTH","5","999","1712","475","1718","One","1730","Transbay","1736","999","1748","",""},
            {"GREEN","MTH","5","999","1752","475","1758","One","1810","Transbay","1816","999","1828","",""},
            {"GREEN","MTH","5","999","1832","475","1838","One","1850","Transbay","1856","999","1908","",""},
            {"GREEN","MTH","4","999","1912","475","1918","One","1924","Transbay","1930","","","",""},

            {"GREEN","FRI","5","999","1532","475","1538","One","1550","Transbay","1556","999","1608","",""},
            {"GREEN","FRI","5","999","1612","475","1618","One","1630","Transbay","1636","999","1648","",""},
            {"GREEN","FRI","5","999","1652","475","1658","One","1710","Transbay","1716","999","1728","",""},
            {"GREEN","FRI","5","999","1732","475","1738","One","1750","Transbay","1756","999","1808","",""},
            {"GREEN","FRI","4","999","1812","475","1818","One","1830","Transbay","1836","","","",""},

        
            {"ORANGE","ALL","4","100","1025","999","1028","475","1033","100","1043","","","",""},
            {"ORANGE","ALL","4","100","1045","999","1048","475","1053","100","1103","","","",""},
            {"ORANGE","ALL","4","100","1105","999","1108","475","1113","100","1123","","","",""},
            {"ORANGE","ALL","4","100","1125","999","1128","475","1133","100","1143","","","",""},
            {"ORANGE","ALL","4","100","1145","999","1148","475","1153","100","1203","","","",""},
            {"ORANGE","ALL","4","100","1205","999","1208","475","1213","100","1223","","","",""},
            {"ORANGE","ALL","4","100","1225","999","1228","475","1233","100","1243","","","",""},
            {"ORANGE","ALL","4","100","1245","999","1248","475","1253","100","1303","","","",""},
            {"ORANGE","ALL","4","100","1305","999","1308","475","1313","100","1323","","","",""},
            {"ORANGE","ALL","4","100","1325","999","1328","475","1333","100","1343","","","",""},
            {"ORANGE","ALL","4","100","1345","999","1348","475","1353","100","1403","","","",""},
            {"ORANGE","ALL","4","100","1405","999","1408","475","1413","100","1423","","","",""},
            {"ORANGE","ALL","4","100","1425","999","1428","475","1433","100","1443","","","",""},
            {"ORANGE","ALL","4","100","1445","999","1448","475","1453","100","1503","","","",""},
            {"ORANGE","ALL","4","100","1505","999","1508","475","1513","100","1523","","","",""},
            {"ORANGE","ALL","4","100","1525","999","1528","475","1533","100","1543","","","",""},
            {"ORANGE","ALL","4","100","1545","999","1548","475","1553","100","1603","","","",""},
            {"ORANGE","ALL","4","100","1605","999","1608","475","1613","100","1623","","","",""},
            {"ORANGE","ALL","4","100","1625","999","1628","475","1633","100","1643","","","",""},

            {"ORANGE","ALL","4","100","1655","999","1658","475","1703","100","1713","","","",""},
            {"ORANGE","ALL","4","100","1715","999","1718","475","1723","100","1733","","","",""},
            {"ORANGE","ALL","3","100","1735","999","1738","475","1743","","","","","",""},

            {"ORANGE","MTH","4","100","2001","999","2003","475","2008","BART","2013","","","",""},
            {"ORANGE","MTH","4","100","2033","999","2035","475","2040","BART","2045","","","",""},

            {"ORANGE","FRI","4","100","1901","999","1903","475","1908","BART","1913","","","",""},
            {"ORANGE","FRI","4","100","1933","999","1935","475","1940","BART","1945","","","",""},


            {"RED","ALL","4","CalTrain","0715","100","0720","999","0721","475","0725","","","",""},
            {"RED","ALL","5","475","0727","CalTrain","0750","100","0755","999","0756","475","0800","",""},
            {"RED","ALL","5","475","0802","CalTrain","0815","100","0820","999","0821","475","0825","",""},
            {"RED","ALL","5","475","0827","CalTrain","0850","100","0855","999","0856","475","0900","",""},
            {"RED","ALL","5","475","0902","CalTrain","0910","100","0915","999","0916","475","0920","",""},
            {"RED","ALL","5","475","0922","CalTrain","0930","100","0935","999","0936","475","0940","",""},
            {"RED","ALL","5","475","0942","CalTrain","0950","100","0955","999","0956","475","1002","",""},
            {"RED","MTH","4","100","1600","999","1602","CalTrain","1610","100","1618","","","",""},
            {"RED","MTH","4","100","1620","999","1622","CalTrain","1630","100","1638","","","",""},
            {"RED","MTH","4","100","1640","999","1642","CalTrain","1650","100","1658","","","",""},
            {"RED","MTH","4","100","1701","999","1703","CalTrain","1711","100","1719","","","",""},
            {"RED","MTH","4","100","1720","999","1722","CalTrain","1730","100","1738","","","",""},
            {"RED","MTH","4","100","1740","999","1742","CalTrain","1750","100","1756","","","",""},
            {"RED","MTH","4","100","1758","999","1800","CalTrain","1808","100","1815","","","",""},
            {"RED","MTH","4","100","1817","999","1819","CalTrain","1827","100","1832","","","",""},
            {"RED","MTH","4","100","1833","999","1835","CalTrain","1843","100","1850","","","",""},
            {"RED","MTH","3","100","1904","999","1906","CalTrain","1914","","","","","",""},
            {"RED","FRI","4","100","1504","999","1506","CalTrain","1514","100","1522","","","",""},
            {"RED","FRI","4","100","1524","999","1526","CalTrain","1534","100","1554","","","",""},
            {"RED","FRI","4","100","1556","999","1558","CalTrain","1606","100","1618","","","",""},
            {"RED","FRI","4","100","1620","999","1622","CalTrain","1630","100","1638","","","",""},
            {"RED","FRI","4","100","1640","999","1642","CalTrain","1650","100","1658","","","",""},
            {"RED","FRI","4","100","1701","999","1703","CalTrain","1711","100","1718","","","",""},
            {"RED","FRI","4","100","1720","999","1722","CalTrain","1730","100","1759","","","",""},
            {"RED","FRI","4","100","1801","999","1803","CalTrain","1811","100","1814","","","",""},
            {"RED","FRI","3","100","1816","999","1818","CalTrain","1826","","","","","",""},
            {"RED","FRI","3","100","1908","999","1910","CalTrain","1918","","","","","",""},

            // Adding timetable for the combinations (getting on at the last stop and sitting through the loop back)
            
            {"ORANGE","ALL","2","475","1033","999","1048","","","","","","","",""},
            {"ORANGE","ALL","2","475","1053","999","1108","","","","","","","",""},
            {"ORANGE","ALL","2","475","1113","999","1128","","","","","","","",""},
            {"ORANGE","ALL","2","475","1133","999","1148","","","","","","","",""},
            {"ORANGE","ALL","2","475","1153","999","1208","","","","","","","",""},
            {"ORANGE","ALL","2","475","1213","999","1228","","","","","","","",""},
            {"ORANGE","ALL","2","475","1233","999","1248","","","","","","","",""},
            {"ORANGE","ALL","2","475","1253","999","1308","","","","","","","",""},
            {"ORANGE","ALL","2","475","1313","999","1328","","","","","","","",""},
            {"ORANGE","ALL","2","475","1333","999","1348","","","","","","","",""},
            {"ORANGE","ALL","2","475","1353","999","1408","","","","","","","",""},
            {"ORANGE","ALL","2","475","1413","999","1428","","","","","","","",""},
            {"ORANGE","ALL","2","475","1433","999","1448","","","","","","","",""},
            {"ORANGE","ALL","2","475","1453","999","1508","","","","","","","",""},
            {"ORANGE","ALL","2","475","1513","999","1528","","","","","","","",""},
            {"ORANGE","ALL","2","475","1533","999","1548","","","","","","","",""},
            {"ORANGE","ALL","2","475","1553","999","1608","","","","","","","",""},
            {"ORANGE","ALL","2","475","1613","999","1628","","","","","","","",""},


        };


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {            
                SPWeb web = SPContext.Current.Web;

                ddlEnd.SelectedIndex = 1;

                /* Load cookies */
                /* If this is the first time then need to catch any NULLs */

                HttpCookie reqCookies = Request.Cookies["shuttleInfo"];
                if (reqCookies != null) {
                    ddlStart.SelectedValue = reqCookies["sourceName"].ToString();
                    ddlEnd.SelectedValue = reqCookies["destinationName"].ToString();
                } else {
                    HttpCookie newCookies = new HttpCookie("shuttleInfo");
                    newCookies["sourceName"] = ddlStart.SelectedValue;
                    newCookies["destinationName"] = ddlEnd.SelectedValue;
                    newCookies.Expires = DateTime.Now.AddDays(30d);
                    Response.Cookies.Add(newCookies);
                }
                //* this sets the values */
                showSchedule();

            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {

            
        }

        // Add the property
        public VisualWebPart1 ParentWebPart
        {
            get;
            set;
        }

        protected void startListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            showSchedule();
        }

        protected void endtListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            showSchedule();
        }


        protected void showSchedule()
        {
            /* We need to search for startListBox in each of the items in the array schedule that are later than currentTime and when we find something read along each item to see if
             * endLisBox is also there. If it is then this is a valid schedule to show.
             */

            int numberOfResultsFound = 0;   // Number of good results found
           currentTime = DateTime.Now; // Subtract 3 hours because of the SharePoint location in EST)
           

            //Create a temporary location for the results
            times[] goodTimes = new times[100];
            
            // Update cookies
            try
            {
                HttpCookie userInfo = Request.Cookies["shuttleInfo"];
                userInfo["sourceName"] = ddlStart.SelectedValue;
                userInfo["destinationName"] = ddlEnd.SelectedValue;
                Response.Cookies.Set(userInfo);
            }
            catch { }
            

            /* Work out minutes since midnight now */
            int minutessincemidnight = (currentTime.Hour * 60) + currentTime.Minute;

            /* Work out the day of the week */
            string dayToday = "MTH";

            if (DateTime.Now.DayOfWeek== DayOfWeek.Friday) dayToday = "FRI";
            if (DateTime.Now.DayOfWeek== DayOfWeek.Saturday) dayToday = "WE";
            if (DateTime.Now.DayOfWeek== DayOfWeek.Sunday) dayToday = "WE";

            string[,] results = new string[4, 2];

            /* Get the start and end destinations */
            string startloc="";
            string endloc="";

            if (ddlStart.Text == "999 Brannan") startloc = "999";
            if (ddlStart.Text == "475 Brannan") startloc = "475";
            if (ddlStart.Text == "100 Potrero") startloc = "100";
            if (ddlStart.Text == "BART") startloc = "BART";
            if (ddlStart.Text == "CalTrain") startloc = "CalTrain";
            if (ddlStart.Text == "5th Street Lot") startloc = "5th";
            if (ddlStart.Text == "8th Street Lot") startloc = "8th";
            if (ddlStart.Text == "Ferry") startloc = "Ferry";
            if (ddlStart.Text == "One Market") startloc = "One";
            if (ddlStart.Text == "Transbay") startloc = "Transbay";
            if (ddlEnd.Text == "999 Brannan") endloc = "999";
            if (ddlEnd.Text == "475 Brannan") endloc = "475";
            if (ddlEnd.Text == "100 Potrero") endloc = "100";
            if (ddlEnd.Text == "BART") endloc = "BART";
            if (ddlEnd.Text == "CalTrain") endloc = "CalTrain";
            if (ddlEnd.Text == "5th Street Lot") endloc = "5th";
            if (ddlEnd.Text == "8th Street Lot") endloc = "8th";
            if (ddlEnd.Text == "Ferry") endloc = "Ferry";
            if (ddlEnd.Text == "One Market") endloc = "One";
            if (ddlEnd.Text == "Transbay") endloc = "Transbay";

            /* For TESTING PURPOSES */
            //dayToday = "WE";
            //minutessincemidnight = 8*60;



            // Loop around every schedule
            for (int i=0; i<= schedule.GetUpperBound(0);i++)
            {
                string routeColor = schedule[i,0];
                string routeDays = schedule[i,1];
                string numberOfStops = schedule[i,2];
                string [,] thisRoute = new string[6,2];

                // Pull the stops and times into an array
                int y = 0;
                for (int x = 3; x <= 13; x=x+2)
                {
                    thisRoute[y, 0] = schedule[i, x];
                    thisRoute[y, 1] = schedule[i, x+1];
                    y++;
                }

                
                // If days match
                int foundstartmatch=0;
                int foundstarttime = 0;
                int foundendtime = 0;
                if ((dayToday != "WE" && routeDays=="ALL") || (dayToday != "WE" && routeDays == dayToday)) {
                    for (int z = 0; z <= 5; z++)
                    {
                        // If we havent found a start yet and locations match - capture start time
                        if (thisRoute[z, 0] == startloc && foundstartmatch == 0)
                        {
                            foundstartmatch = 1;
                            foundstarttime = (Convert.ToInt32(thisRoute[z, 1].Substring(0, 2))) * 60 + (Convert.ToInt32(thisRoute[z, 1].Substring(2, 2)));

                        }
                        // If we found a start and have now found a stop get the end time
                        if (foundstartmatch == 1 && thisRoute[z, 0] == endloc)
                        {
                            foundendtime = (Convert.ToInt32(thisRoute[z, 1].Substring(0, 2))) * 60 + (Convert.ToInt32(thisRoute[z, 1].Substring(2, 2)));

                            // We've found a start and an end - check the time
                            if (foundstarttime > minutessincemidnight)
                            {
                                goodTimes[numberOfResultsFound] = new times();
                                // Its good so write it to good times
                                goodTimes[numberOfResultsFound].start = foundstarttime;
                                goodTimes[numberOfResultsFound].color = routeColor;
                                goodTimes[numberOfResultsFound].end = foundendtime;
                                numberOfResultsFound++;
                            }
                        } // end of looping on found start/end time                    
                    } // End of looping through this lines schedule
                } // End of condition mfor matching day
            } // End of looping through all schedules

            // Now we need to find the three most recent start times
            int currentlowesttime=9998; // Used to track which is the current time that's lowest
            int lastlowesttime = 0; // Used to track the last time which was lowest
            for (int eachline = 0; eachline <= 2; eachline++)
            {
                string showStart = "";
                string showEnd = "";
                string showColor = "BLACK";
                for (int x = 0; x < numberOfResultsFound; x++)
                {
                    // Loop through each of the results
                    if (goodTimes[x].start < currentlowesttime && goodTimes[x].start > lastlowesttime)
                    {
                        int thehours;
                        int theminutes;

   
                        
                        thehours = Math.DivRem(goodTimes[x].start, 60, out theminutes);
                        DateTime timestamp1 = new DateTime(1999,1,13,thehours,theminutes,0);
                        showStart = timestamp1.ToString("hh:mm tt");
                        showStart = showStart + " " + (goodTimes[x].start - minutessincemidnight).ToString() + " min";
                        currentlowesttime = goodTimes[x].start;

                        thehours = Math.DivRem(goodTimes[x].end, 60, out theminutes);

                        DateTime timestamp2 = new DateTime(1999, 1, 13, thehours, theminutes, 0);
                        showEnd = timestamp2.ToString("hh:mm tt");
                        showEnd = showEnd + " " + (goodTimes[x].end - minutessincemidnight).ToString() + " min";
                        showColor = goodTimes[x].color;
                    }

                }
                lastlowesttime = currentlowesttime;
                currentlowesttime = 9998;   // reset for next comparison

                
                Color theforecolor = new Color();
                if (showColor == "RED") theforecolor = Color.Red;
                if (showColor == "BLACK") theforecolor = Color.Black;
                if (showColor == "BLUE") theforecolor = Color.Blue;
                if (showColor == "GREEN") theforecolor = Color.Green;
                if (showColor == "ORANGE") theforecolor = Color.DarkOrange;

                // Guard against the same stop start time
                if (ddlEnd.Text == ddlStart.Text)
                {
                    showStart = "";
                    showEnd = "";
                }

                if (eachline == 0)
                {
                    StartTB1.Text = showStart;
                    EndTB1.Text = showEnd;
                    StartTB1.ForeColor = theforecolor;
                    EndTB1.ForeColor = theforecolor;
 
                }
                if (eachline == 1)
                {
                    StartTB2.Text = showStart;
                    EndTB2.Text = showEnd;
                    StartTB2.ForeColor = theforecolor;
                    EndTB2.ForeColor = theforecolor;

                }
                if (eachline == 2)
                {
                    StartTB3.Text = showStart;
                    EndTB3.Text = showEnd;
                    StartTB3.ForeColor = theforecolor;
                    EndTB3.ForeColor = theforecolor;
                }

            }

        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            int currentstart = ddlStart.SelectedIndex;
            ddlStart.SelectedIndex = ddlEnd.SelectedIndex;
            ddlEnd.SelectedIndex = currentstart;
            showSchedule();
        } // End of ShowSchedule
    }

}   

