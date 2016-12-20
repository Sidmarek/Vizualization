﻿using System.Collections.Generic;
//using System.Web.UI.WebControls;
using System.Drawing;
using System;
using MVCtutorial.Controllers;
using System.Reflection;

namespace MVCtutorial.Graph.Models
{
    public class LangDef
    {
        public string LangAbbreviation; //Abreviation = "shortcut" 
    }

    public static class LangDefinition
    {
        public static List<LangDef> LangDefList;
        ///
        public static string Find(string lang)
        {
            foreach (LangDef LangDef in LangDefList)
            {
                if (LangDef.LangAbbreviation.Contains(lang))
                {
                    return LangDef.LangAbbreviation;
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lang">lang which you want to add</param>
        /// <param name="position">Optional parameter</param>
        public static void Add(string lang, int position = 0)
        {
            if (position == 0)
            {
                LangDefList.Add(new LangDef() { LangAbbreviation = lang });
            }
            else
            {
                LangDefList.Insert(position, new LangDef() { LangAbbreviation = lang });
            }
        }
    }

    public class TableDef
    {
        public string shortName;
        public int Idx;
        public string tabName;
    }

    public static class TableDefinition
    {
        public static List<TableDef> TableDefList = new List<TableDef>();
        // public static List 
        public static string Find(int ConnNo, string TabName)
        {
            foreach (TableDef TableDef in TableDefList)
            {
                if (TableDef.Idx == ConnNo & TableDef.tabName.Contains(TabName))
                {
                    return TableDef.shortName;
                }
            }
            return null;
        }
        public static string Add(int ConnNo, string TabName)
        {
            int subscoreIdx;
            try {
                subscoreIdx = TabName.LastIndexOf("_");
            } catch (ArgumentNullException e) {
                throw new Exception(e.Message);
            }
            string shortedName = TabName.Substring(subscoreIdx+1);
            TableDefList.Add(new TableDef() { shortName = shortedName, Idx = ConnNo, tabName = TabName });
            return shortedName;
        }
    }

    public class Values
    {
        public int idx;
        public string[] langTexts;
    }

    public class TextlistDef
    {
        public string textlist;
        public List<Values> values;   
    }

    public static class TextlistDefinition
    {
        public static List<TextlistDef> TextlistDefList = new List<TextlistDef>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textlist"></param>
        /// <returns></returns>
        public static string Find(string textlist)
        {
            foreach (TextlistDef TextlistDef in TextlistDefList) {
                if (TextlistDef.textlist.Contains(textlist))
                {
                    return TextlistDef.textlist;
                }
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="textsArray"></param>
        /// <param name="Idxs"></param>
        /// <returns></returns>
        public static string Add(string name,List<string[]> textsArray, List<int> Idxs)
        {
            List<Values> tempValues = new List<Values>();
            
            for (int i = 0;i<textsArray.Count;i++)//First row is name row - textlist name 
            {
                tempValues.Add(new Values() { idx= Idxs[i], langTexts= textsArray[i]});
            }
            TextlistDefList.Add(new TextlistDef() { textlist = name, values = tempValues });
            return name;
        }
        public static bool  UpdateName(string oldName, string newName) {
            foreach (TextlistDef TextlistDef in TextlistDefList)
            {
                if (TextlistDef.textlist.Contains(oldName))
                {
                    TextlistDef.textlist = newName;
                    return true;
                }
            }
            return false;
        }
    }

    public class ColumnTextlist {
        public string column;
        public string TextlistName;
    }

    public class ColumnTextlistDefine {
        public static List<ColumnTextlist> ColumnTextlistList = new List<ColumnTextlist>();

        public static string FindtextlistName(string column)
        {
            foreach (ColumnTextlist ColumntextlistInstance in ColumnTextlistList)
            {
                if (ColumntextlistInstance.column.Contains(column))
                {
                    return ColumntextlistInstance.TextlistName;
                }
            }
            return null;
        }
        public static string Findcolumn(string TextlistName)
        {
            foreach (ColumnTextlist ColumntextlistInstance in ColumnTextlistList)
            {
                if (ColumntextlistInstance.TextlistName.Contains(TextlistName))
                {
                    return ColumntextlistInstance.column;
                }
            }
            return null;
        }
        public static void Add(string column, string TextlistName) {
            ColumnTextlistList.Add(new ColumnTextlist() { column = column, TextlistName = TextlistName });
        }
    }
    public class NameDef {
        public string table;
        public string column;
        public List<string> fullNames;
        public List<string> units;
    }
    public class NameDefinition {
        public static List<NameDef> NameDefList = new List<NameDef>();
        public static string Find(string column)
        {
            foreach ( NameDef NameDef in NameDefList)
            {
                if (NameDef.column.Contains(column))
                {
                    return NameDef.column;
                }
            }
            return null;
        }

        public static string Add(string ascolumn, List<string> asfullNames, List<string> asunits, string astable = null)
        {         
            NameDefList.Add(new NameDef() { table = astable, column = ascolumn, fullNames = asfullNames, units  = asunits});
            return ascolumn;
        }
    }

    public class Const
    {
        public static readonly string[]  Separ_Space = { " ", ".", ""};
    }

    public class CSigMultitext
    {
        public string type = "multitext";
        public string TableName;
        public string Column;
        public Color Color;
        public string textlist;
        CSigMultitext(string asTabDefName, string asColumn, Color acColor, string asTextListDef)
        {
            TableName = asTabDefName;
            Column = asColumn;
            Color = acColor;
            textlist = asTextListDef;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="separ_names_string">First row of that </param>
        /// <param name="separ_cfg_string"></param>
        /// <returns></returns>
        public static CSigMultitext FromIni(string[] separ_cfg_string) {
            string TableDefName, Column, TableName, textlist;
            int ConnectionStringNumber;
            Color Color;

            ConnectionStringNumber = int.Parse(separ_cfg_string[1]);
            Column = separ_cfg_string[2];
            TableName = separ_cfg_string[3];
            TableDefName = TableDefinition.Find(ConnectionStringNumber, TableName);
            if (TableDefName == null)
            {
                TableDefinition.Add(ConnectionStringNumber, TableName);
            }
            Color = Color.FromArgb(int.Parse(separ_cfg_string[5]), int.Parse(separ_cfg_string[6]), int.Parse(separ_cfg_string[7]));

            textlist = ColumnTextlistDefine.FindtextlistName(separ_cfg_string[2]);
            if (textlist == null) {

            }

            return new CSigMultitext(TableDefName, Column, Color, textlist);
         }
    }

    public class CSignal
    {
        public string type = "analog";
        public string SignalName;
        public string TableName;
        public int Decimal;
        public Color Color;
        CSignal(string asSigName, string asTabDefName, int aiDecimal, Color acColor)
        {
            SignalName = asSigName;
            TableName = asTabDefName;
            Decimal = aiDecimal;
            Color = acColor;
            CField.AddSignal(this);
        }

        public static CSignal FromIni(string[] separ_cfg_string)
        {
            int ConnectionStringNumber;
            string SignalName, TableName, TableDefName;
            int Decimal;
            Color Color;         
            // priklad:   Signal=3:iWMU_Temp  arBF_norm 1 255,0,0
            //            0      1 2          3         4 5   6 7

            ConnectionStringNumber =  int.Parse(separ_cfg_string[1]);
            SignalName = separ_cfg_string[2];
            TableName = separ_cfg_string[3];
            TableDefName = TableDefinition.Find(ConnectionStringNumber, TableName);
            if (TableDefName == null )
            {
                TableDefName = TableDefinition.Add(ConnectionStringNumber, TableName);
            }   
            Decimal = int.Parse(separ_cfg_string[4]);
            Color = Color.FromArgb(int.Parse(separ_cfg_string[5]), int.Parse(separ_cfg_string[6]), int.Parse(separ_cfg_string[7]));
            
            return new CSignal(SignalName, TableDefName, Decimal, Color);
        }

        static public CSignal FromJson(string sJson)
        {
            return null;
        }

        public string ToJson()
        {
            int i = 0;
            int position = 0;
            string[] paramNames = new string[typeof(CSignal).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Length];
            string[] paramValues = new string[typeof(CSignal).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Length];
            // {"type":"analog", "table":"norm", "column":"diFlourHopper_Mass", "decimal":3, "color":#88FF00, "coef":0.001 }
            foreach (FieldInfo FI in typeof(CSignal).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {                
                string param = FI.Name.ToLower();
                object obj = FI.GetValue(this);
                string paramValue = obj.ToString();
                paramNames[i] = param;
                paramValues[i] = paramValue;
                i++;
            }
            string json = "";
            i = 0;
            foreach (string s in paramNames)
            {
                json.Insert(position, s);
                json.Insert(position+1, ":");
                position += s.Length+1;
                i++;
            }           
            return "Not Implemented yet";
        }
    }

    public class CField
    {
        public static readonly int minY = 0;
        public int maxY;
        public int relSize;
        public static List<CSignal> SigList = new List<CSignal>();
        public static List<CSigMultitext> SigMultiList = new List<CSigMultitext>();
        CField(int maximalY, int realSize) {
            maxY = maximalY;
            relSize = realSize;
            CView.AddField(this);
        }

        public static CField FromIni(string[] separ_string)
        {
            int maximalY, realSize;
            maximalY = int.Parse(separ_string[1]);
            realSize = int.Parse(separ_string[2]);
            return new CField(maximalY, realSize);
        }

        public static void AddSignal(CSignal aSig)
        {            
            SigList.Add(aSig);
        }
        public static void AddSignalMultitext(CSigMultitext aSigMulti)
        {
            SigMultiList.Add(aSigMulti);
        }
    }

    public class CView
    {
        public static List<string> Names;
        public static List<CField> FieldList = new List<CField>();

        public static CView FromIni(string[] separ_string)
        {
            string s;
            //Cylcle starts on two beacause of skiping position of word View and number of view (ex. 3)
            for (int i=2;i<separ_string.Length;i++) {
                s = separ_string[i-1];
                Names.Add(s);
            }
            return new CView();
        }

        public static void AddField(CField CFieldInstatance)
        {
            FieldList.Add(CFieldInstatance);
        }
    }

    public class CIniFile {
        public static List<CView> ViewList = new List<CView>();
        public static void AddView(CView CViewInstance)
        {
            ViewList.Add(CViewInstance);
        }
    }

    /// <summary>
    ///     CfgStructure defines Graph config cfg structure
    /// </summary>

    ///     NamesStructure defines Graph config  names structure
    /// </summary>
}