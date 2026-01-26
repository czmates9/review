using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fask.Module.Inside.Inside_nicety
{
    public class StubUtil
    {
        public static dataModelColumn createColumn(String fieldName)
        {
            dataModelColumn column = new dataModelColumn();
            column.fieldName = fieldName;
            return column;
        }

        public static dmFilterCondition prepareFilter(String fieldName)
        {
            dmFilterCondition userCond = new dmFilterCondition();
            userCond.column = (createColumn(fieldName));
            return userCond;
        }

        public static dmFilterCondition createFilter(String fieldName, docId docId)
        {
            dmFilterCondition userCond = prepareFilter(fieldName);
            if (docId != null)
            {
                userCond.intValues = new long?[] { docId.id };
            }
            return userCond;
        }

        public static dmFilterCondition createFilter(dmFilterCondition children)
        {
            dmFilterCondition rootFilter = new dmFilterCondition();
            rootFilter.children = new dmFilterCondition[] { children };
            return rootFilter;
        }

        public static dmSorting prepareSorting(String fieldName)
        {
            dmSorting userSorting = new dmSorting();
            userSorting.column = createColumn(fieldName);
            return userSorting;
        }
    }
}
