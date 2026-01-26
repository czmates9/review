using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fask.Server.Interfaces.Classes;
using System.Data;
using System.Web.UI.WebControls;

namespace Fask.Server.Interfaces.WebControl
{
    public interface IWebControl
    {
        //Object getControl(int index);

        List<TreeNode> getActions(User uzivatel);

        Object getAction(TreeNode selectedAction, System.Web.UI.Page page);
    }
}
