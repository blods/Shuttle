using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.Extensions;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;

namespace DolbyShuttle.VisualWebPart1
{
    
    [ToolboxItemAttribute(false)]
    public class VisualWebPart1 : WebPart
    {
        // Visual Studio might automatically update this path when you change the Visual Web Part project item.
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/DolbyShuttle/Dolby Shuttle/VisualWebPart1UserControl.ascx";

        // Add property to store the pointer to the CSV file DolbyShuttle timetable
        
        [Personalizable(), WebBrowsable,WebDisplayName("Timetable File"),Category("Shuttle Configuration")]
        public string TimetableFile { get; set; }
 
        protected override void CreateChildControls()
        {
            VisualWebPart1UserControl control = (VisualWebPart1UserControl)Page.LoadControl(_ascxPath);
            control.ParentWebPart = this;
            Controls.Add(control);

 
        }


    }
}
