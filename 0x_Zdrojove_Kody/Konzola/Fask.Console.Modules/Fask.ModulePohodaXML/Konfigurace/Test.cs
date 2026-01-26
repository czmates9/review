using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;

namespace Fask.ModulePohodaXML.Konfigurace
{
    class Test
    {
        #region MyRegion

        private interface IUnimplemented { }

        private Object[] currentObjects;

        private object GetUnwrappedObject(int index)
        {
            if (currentObjects == null || index < 0 || index > currentObjects.Length)
            {
                return null;
            }

            Object obj = currentObjects[index];
            if (obj is ICustomTypeDescriptor)
            {
                obj = ((ICustomTypeDescriptor)obj).GetPropertyOwner(null);
            }
            return obj;
        }

        

        #endregion

        public object[] SelectedObjects
        {
            set
            {
                try
                {

                    bool isSame = false;
                    bool classesSame = false;
                    bool showEvents = true;

                    // validate the array coming in
                    if (value != null && value.Length > 0)
                    {
                        for (int count = 0; count < value.Length; count++)
                        {
                            if (value[count] == null)
                            {
                                //throw new ArgumentException(SR.GetString(SR.PropertyGridSetNull, count.ToString(CultureInfo.CurrentCulture), value.Length.ToString(CultureInfo.CurrentCulture)));
                            }
                            else if (value[count] is IUnimplemented)
                            {
                                //throw new NotSupportedException(SR.GetString(SR.PropertyGridRemotedObject, value[count].GetType().FullName));
                            }
                        }
                    }
                    else
                    {
                        showEvents = false;
                    }

                    // make sure we actually changed something before we inspect tabs
                    if (currentObjects != null && value != null &&
                        currentObjects.Length == value.Length)
                    {
                        isSame = true;
                        classesSame = true;
                        for (int i = 0; i < value.Length && (isSame || classesSame); i++)
                        {
                            if (isSame && currentObjects[i] != value[i])
                            {
                                isSame = false;
                            }

                            Type oldType = GetUnwrappedObject(i).GetType();

                            Object objTemp = value[i];

                            if (objTemp is ICustomTypeDescriptor)
                            {
                                objTemp = ((ICustomTypeDescriptor)objTemp).GetPropertyOwner(null);
                            }
                            Type newType = objTemp.GetType();

                            // check if the types are the same.  If they are, and they 
                            // are COM objects, check their GUID's.  If they are different
                            // or Guid.Emtpy, assume the classes are different.
                            //
                            if (classesSame &&
                                (oldType != newType || oldType.IsCOMObject && newType.IsCOMObject))
                            {
                                classesSame = false;
                            }
                        }
                    }

                    if (!isSame)
                    {

                        //EnsureDesignerEventService();

                        //showEvents = showEvents && GetFlag(GotDesignerEventService);

                        //SetStatusBox("", "");

                        //ClearCachedProps();

                        // The default selected entry might still reference the previous selected 
                        // objects. Set it to null to avoid leaks.
                        //peDefault = null;

                        if (value == null)
                        {
                            currentObjects = new Object[0];
                        }
                        else
                        {
                            currentObjects = (object[])value.Clone();
                        }

                        //SinkPropertyNotifyEvents();
                        //SetFlag(PropertiesChanged, true);


                        // Since we are changing the selection, we need to make sure that the
                        // keywords for the currently selected grid entry gets removed
                        if (gridView != null)
                        {
                            // TypeResolutionService is needed to access the HelpKeyword. However,
                            // TypeResolutionService might be disposed when project is closing. We
                            // need swallow the exception in this case.
                            try
                            {
                                gridView.RemoveSelectedEntryHelpAttributes();
                            }
                            catch (COMException) { }
                        }

                        if (peMain != null)
                        {
                            peMain.Dispose();
                        }

                        // throw away any extra component only tabs
                        if (!classesSame && !GetFlag(TabsChanging) && selectedViewTab < viewTabButtons.Length)
                        {

                            Type tabType = selectedViewTab == -1 ? null : viewTabs[selectedViewTab].GetType();
                            ToolStripButton viewTabButton = null;
                            RefreshTabs(PropertyTabScope.Component);
                            EnableTabs();
                            if (tabType != null)
                            {
                                for (int i = 0; i < viewTabs.Length; i++)
                                {
                                    if (viewTabs[i].GetType() == tabType && viewTabButtons[i].Visible)
                                    {
                                        viewTabButton = viewTabButtons[i];
                                        break;
                                    }
                                }
                            }
                            SelectViewTabButtonDefault(viewTabButton);
                        }

                        // make sure we've also got events on all the objects
                        if (showEvents && viewTabs != null && viewTabs.Length > EVENTS && (viewTabs[EVENTS] is EventsTab))
                        {
                            showEvents = viewTabButtons[EVENTS].Visible;
                            Object tempObj;
                            PropertyDescriptorCollection events;
                            Attribute[] attrs = new Attribute[BrowsableAttributes.Count];
                            BrowsableAttributes.CopyTo(attrs, 0);

                            Hashtable eventTypes = null;

                            if (currentObjects.Length > 10)
                            {
                                eventTypes = new Hashtable();
                            }

                            for (int i = 0; i < currentObjects.Length && showEvents; i++)
                            {
                                tempObj = currentObjects[i];

                                if (tempObj is ICustomTypeDescriptor)
                                {
                                    tempObj = ((ICustomTypeDescriptor)tempObj).GetPropertyOwner(null);
                                }

                                Type objType = tempObj.GetType();

                                if (eventTypes != null && eventTypes.Contains(objType))
                                {
                                    continue;
                                }

                                // make sure these things are sited components as well
                                showEvents = showEvents && (tempObj is IComponent && ((IComponent)tempObj).Site != null);

                                // make sure we've also got events on all the objects
                                events = ((EventsTab)viewTabs[EVENTS]).GetProperties(tempObj, attrs);
                                showEvents = showEvents && events != null && events.Count > 0;

                                if (showEvents && eventTypes != null)
                                {
                                    eventTypes[objType] = objType;
                                }
                            }
                        }
                        ShowEventsButton(showEvents && currentObjects.Length > 0);
                        DisplayHotCommands();

                        if (currentObjects.Length == 1)
                        {
                            EnablePropPageButton(currentObjects[0]);
                        }
                        else
                        {
                            EnablePropPageButton(null);
                        }
                        OnSelectedObjectsChanged(EventArgs.Empty);
                    }


                    /*
       
                    Microsoft, hopefully this won't be a big perf problem, but it looks like we
                           need to refresh even if we didn't change the selected objects.
       
                    if (propertiesChanged) {*/
                    if (!GetFlag(TabsChanging))
                    {

                        // ReInitTab means that we should set the tab back to what is used to be for a given designer.
                        // Basically, if you select an events tab for your designer and double click to go to code, it should
                        // be the events tab when you get back to the designer.
                        //
                        // so we set that bit when designers get switched, and makes sure we select and refresh that tab
                        // when we load.
                        //
                        if (currentObjects.Length > 0 && GetFlag(ReInitTab))
                        {
                            object designerKey = ActiveDesigner;

                            // get the active designer, see if we've stashed away state for it.
                            //
                            if (designerKey != null && designerSelections != null && designerSelections.ContainsKey(designerKey.GetHashCode()))
                            {
                                int nButton = (int)designerSelections[designerKey.GetHashCode()];

                                // yep, we know this one.  Make sure it's selected.
                                //
                                if (nButton < viewTabs.Length && (nButton == PROPERTIES || viewTabButtons[nButton].Visible))
                                {
                                    SelectViewTabButton(viewTabButtons[nButton], true);
                                }
                            }
                            else
                            {
                                Refresh(false);
                            }
                            SetFlag(ReInitTab, false);
                        }
                        else
                        {
                            Refresh(true);
                        }

                        if (currentObjects.Length > 0)
                        {
                            SaveTabSelection();
                        }
                    }
                    /*}else {
                        Invalidate();
                        gridView.Invalidate();
                    //}*/
                }
                finally
                {
                    this.FreezePainting = false;
                }
            }

            get
            {
                if (currentObjects == null)
                {
                    return new object[0];
                }
                return (object[])currentObjects.Clone();
            }
        }

    }
}
