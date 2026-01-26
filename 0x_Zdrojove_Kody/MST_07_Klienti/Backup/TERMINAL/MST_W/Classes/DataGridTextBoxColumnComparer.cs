using System.Collections.Generic;
using System.Windows.Forms;

namespace Fask.MST_W.Classes
{
    public class DataGridColumnStyleComparer : IComparer<DataGridColumnStyle>
    {
        private Comparer<int> intComparer = Comparer<int>.Default;
        private List<string> comparation = new List<string>();

        private DataGridColumnStyleComparer()
        {
        }

        public DataGridColumnStyleComparer(List<string> comparation)
        {
            this.comparation = comparation;
        }

        #region IComparer<DataGridColumnStyle> Members

        public int Compare(DataGridColumnStyle x, DataGridColumnStyle y)
        {
            if (x == null && y == null)
                return 0;
            else if (x == null)
                return -1;
            else if (y == null)
                return 1;

            int x_index = -1;
            int y_index = -1;

            if (comparation.Contains(x.MappingName))
                x_index = comparation.IndexOf(x.MappingName);

            if (comparation.Contains(y.MappingName))
                y_index = comparation.IndexOf(y.MappingName);

            return intComparer.Compare(x_index, y_index);
        }

        #endregion
    }
}
