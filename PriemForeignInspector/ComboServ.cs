using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace PriemForeignInspector
{
    public class ComboServ
    {
        public const string NO_VALUE = "NO_VALUE";
        public const string ALL_VALUE = "ALL_VALUE";
        public const string DISPLAY_NO_VALUE = "нет";
        public const string DISPLAY_ALL_VALUE = "все";
        
        public enum ComboValues
        {
            noValue, allValue
        }

        public static void FillCombo(ComboBox cb, List<KeyValuePair<string, string>> lstValues, bool hasNo, bool hasAll)
        {
            if (cb == null)
                return;

            if (hasNo)
                if (lstValues != null && !lstValues.Contains(new KeyValuePair<string, string>(NO_VALUE, DISPLAY_NO_VALUE)))
                    lstValues.Insert(0, new KeyValuePair<string, string>(NO_VALUE, DISPLAY_NO_VALUE));
            if (hasAll)
                if (lstValues != null && !lstValues.Contains(new KeyValuePair<string, string>(ALL_VALUE, DISPLAY_ALL_VALUE)))
                    lstValues.Insert(0, new KeyValuePair<string, string>(ALL_VALUE, DISPLAY_ALL_VALUE));
                        
            var source = lstValues;

            if (source == null || source.Count() == 0)
            {
                lstValues = new List<KeyValuePair<string, string>>();
                lstValues.Insert(0, new KeyValuePair<string, string>(NO_VALUE, DISPLAY_NO_VALUE));
                source = lstValues;
            }
                       
            cb.DataSource = new BindingSource(source, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }

        public static void FillCombo(ListBox cb, List<KeyValuePair<string, string>> lstValues, bool hasNo, bool hasAll)
        {
            if (cb == null)
                return;

            if (hasNo)
                if (lstValues != null && !lstValues.Contains(new KeyValuePair<string, string>(NO_VALUE, DISPLAY_NO_VALUE)))
                    lstValues.Insert(0, new KeyValuePair<string, string>(NO_VALUE, DISPLAY_NO_VALUE));
            if (hasAll)
                if (lstValues != null && !lstValues.Contains(new KeyValuePair<string, string>(ALL_VALUE, DISPLAY_ALL_VALUE)))
                    lstValues.Insert(0, new KeyValuePair<string, string>(ALL_VALUE, DISPLAY_ALL_VALUE));

            var source = lstValues;

            if (source == null || source.Count() == 0)
            {
                lstValues = new List<KeyValuePair<string, string>>();
                lstValues.Insert(0, new KeyValuePair<string, string>(NO_VALUE, DISPLAY_NO_VALUE));
                source = lstValues;
            }

            cb.DataSource = new BindingSource(source, null);
            cb.DisplayMember = "Value";
            cb.ValueMember = "Key";
        }

        public static void FillCombo(ComboBox cb, List<string> lstValues, bool hasNo, bool hasAll)
        {
            if (cb == null)
                return;

            if (hasNo)
                if (!lstValues.Contains(DISPLAY_NO_VALUE))
                    lstValues.Insert(0, DISPLAY_NO_VALUE);
            if (hasAll)
                if (!lstValues.Contains(DISPLAY_ALL_VALUE))
                    lstValues.Insert(0, DISPLAY_ALL_VALUE);

            var source = lstValues;

            if (source.Count() == 0)
                return;

            cb.DataSource = source; 
            cb.SelectedIndex = 0;
        }
        
        //public static void FillCombo(ComboBox cb, List<string> lstValues)
        //{
        //    if (cb == null)
        //        return;
            
        //    var source = lstValues;

        //    if (source.Count() == 0)
        //        return;

        //    cb.DataSource = new BindingSource(source, null);
        //    cb.DisplayMember = "Value";
           
        //    cb.SelectedIndex = 0;
        //}

        public static string GetComboId(ComboBox cb)
        {
            if (cb.SelectedValue == null)
                return null;
            else if (cb.SelectedValue.ToString() == NO_VALUE)
                return null;
            else if (cb.SelectedValue.ToString() == ALL_VALUE)
                return string.Empty;
            else
            {
                if (cb.SelectedValue is KeyValuePair<string, string>)
                    return ((KeyValuePair<string, string>)cb.SelectedValue).Key.ToString();
                else
                    return cb.SelectedValue.ToString();
            }
        }

        public static void SetComboId(ComboBox cb, string val)
        {
            cb.SelectedIndex = -1;

            if (val == null) 
                cb.SelectedValue = NO_VALUE;
            else if (val.Trim() == string.Empty)
                cb.SelectedValue = ALL_VALUE; 
            else
                cb.SelectedValue = val;
            
            //if (cb.SelectedItem == null)
            //    cb.SelectedIndex = 0;
        }

        public static void SetComboId(ComboBox cb, int? val)
        {
            //cb.SelectedIndex = -1;

            if (val == null)
            {
                cb.SelectedValue = NO_VALUE;
                cb.SelectedIndex = -1;
            }
            else
                cb.SelectedValue = val.ToString();

            cb.Refresh();
        }

        public static void SetComboId(ComboBox cb, long? val)
        {
            //cb.SelectedIndex = -1;

            if (val == null)
            {
                cb.SelectedValue = NO_VALUE;
                cb.SelectedIndex = -1;
            }
            else
                cb.SelectedValue = val.ToString();

            cb.Refresh();
        }

        public static void SetComboId(ComboBox cb, Guid? val)
        {
            cb.SelectedIndex = -1;

            if (val == null)
                cb.SelectedValue = NO_VALUE;
            else
                cb.SelectedValue = val.ToString();
        }

        public static int? GetComboIdInt(ComboBox cb)
        {
            string ret = GetComboId(cb);
            if (string.IsNullOrEmpty(ret))
                return null;
            int i;
            if (int.TryParse(ret, out i))
                return i;
            else
                return null;  
        }

        public static Guid? GetComboIdGuid(ComboBox cb)
        {
            string sRet = GetComboId(cb);
            if (string.IsNullOrEmpty(sRet))
                return null;
            Guid gRet = Guid.Empty;
            if (Guid.TryParse(sRet, out gRet))
                return gRet;
            else
                return null;
        }

        public static List<KeyValuePair<string, string>> GetBoolFilter()
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            lst.Add(new KeyValuePair<string,string>("0", "Нет"));
            lst.Add(new KeyValuePair<string,string>("1", "Да"));

            return lst;
        }

        public static bool IsNullOrEmpty(ComboBox cb)
        {
            if (string.IsNullOrEmpty(GetComboId(cb)))
                return true;
            else
                return false;
        }

        public static List<string> GetComboId(ListBox cb)
        {
            if (cb.SelectedItems.Count == 0)
                return null;
            else if (cb.SelectedValue.ToString() == NO_VALUE)
                return null;
            else if (cb.SelectedValue.ToString() == ALL_VALUE)
                return null;
            else
            {
                List<string> Val = new List<string>();
                foreach (int SelInd in cb.SelectedIndices)
                {
                    if (cb.Items[SelInd] is KeyValuePair<string, string>)
                        Val.Add(((KeyValuePair<string, string>)cb.Items[SelInd]).Key.ToString());
                    else
                        Val.Add(cb.Items[SelInd].ToString());
                }
                return Val;
            }
        }
        public static void SetComboId(ListBox cb, List<string> str)
        {
            foreach (string val in str)
                if (val == null)
                    cb.SelectedValue = NO_VALUE;
                else if (val.Trim() == string.Empty)
                    cb.SelectedValue = ALL_VALUE;
                else
                    cb.SelectedValue=val;
        }
    }
}
