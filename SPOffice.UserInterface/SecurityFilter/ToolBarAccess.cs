using SAMTool.DataAccessObject.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserInterface.Models;

namespace SPOffice.UserInterface.SecurityFilter
{
    public class ToolBarAccess
    {
        public ToolboxViewModel SetToolbarAccess(ToolboxViewModel toolbar, Permission _permission)
        {
            try
            {

                if (_permission.SubPermissionList != null)
                {
                    toolbar.ApproveBtn = setAccess(toolbar.ApproveBtn, _permission);
                    toolbar.deletebtn = setAccess(toolbar.deletebtn, _permission);                    
                }

                return toolbar;
            }
            catch (Exception)
            {

                return toolbar;
            }

        }

        private ToolBoxStructure setAccess(ToolBoxStructure btn, Permission _permission)
        {


            if (_permission.SubPermissionList.Exists(s => s.Name == btn.SecurityObject) == false || _permission.SubPermissionList.First(s => s.Name == btn.SecurityObject).AccessCode.Contains("R"))
            {
                btn.HasAccess = true;

            }
            else
            {
                btn.HasAccess = false;
                btn.DisableReason = "Access Denied";
            }

            return btn;
        }

    }
}