using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using QIS.Careq.Data.Service;
using QIS.Careq.Web.Common;
using System.Text;

namespace QIS.Careq.Web.CommonLibs
{
    public partial class DataLayerGenerator : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                List<SysObjects> lstTable = BusinessLayer.GetSysObjectsList("type = 'U' ORDER BY name ASC");
                Methods.SetComboBoxField<SysObjects>(cboTable, lstTable, "Name", "ObjectID");

                List<SysObjects> lstView = BusinessLayer.GetSysObjectsList("type = 'V' ORDER BY name ASC");
                Methods.SetComboBoxField<SysObjects>(cboView, lstView, "Name", "ObjectID");
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            int objectID = Convert.ToInt32(cboTable.SelectedValue);
            List<SysColumns> lstColumns = BusinessLayer.GetSysColumnsList("OBJECT_ID = " + objectID);

            string tableName = txtTableName.Text;
            string oldTableName = cboTable.SelectedItem.Text;
            List<String> listPK = BusinessLayer.GetSysColumnsPKList(tableName);

            String listMember = "";
            String listField = "";
            String paramPK = "";
            String passParamPK = "";
            foreach (SysColumns col in lstColumns)
            {
                string colType = col.Type;
                string identity = "";
                if (col.IsIdentity)
                    identity = ", IsIdentity = true";
                string nullable = "";
                if (col.IsNullable)
                {
                    nullable = ", IsNullable = true";
                    if (colType.Contains("Int"))
                        colType += "?";
                }
                listMember += string.Format("private {0} _{1};<br>", colType, col.Name);

                string primaryKey = "";
                if (listPK.Contains(col.Name))
                {
                    primaryKey = ", IsPrimaryKey = true";
                    if (paramPK != "")
                        paramPK += ", ";
                    paramPK += string.Format("{0} {1}", col.Type, col.Name);

                    if (passParamPK != "")
                        passParamPK += ", ";
                    passParamPK += string.Format("{0}", col.Name);
                }
                listField += string.Format("[Column(Name = \"{0}\", DataType = \"{1}\"{2}{3}{4})]<br>", col.Name, col.Type, primaryKey, identity, nullable);
                listField += string.Format("public {1} {0}<br>{{<br>get {{ return _{0}; }}<br>set {{ _{0} = value; }}<br>}}<br>", col.Name, colType);

            }

            String result = "";
            result = String.Format("#region {0}<br>[Serializable]<br>[Table(Name = \"{1}\")]<br>public class {0} : DbDataModel<br>{{<br>", tableName, oldTableName);
            result += listMember + "<br>";
            result += listField;
            result += "}<br><br>";

            //Dao
            String listStringPK = "";
            string ctxPK = "";
            foreach (String pk in listPK)
            {
                listStringPK += string.Format("private const string p_{0} = \"@p_{0}\";<br>", pk);
                ctxPK += string.Format("_ctx.Add(p_{0}, {0});<br>", pk);
            }

            result += string.Format("public class {0}Dao<br>{{<br>private readonly IDbContext _ctx = DbFactory.Configure();<br>private readonly DbHelper _helper = new DbHelper(typeof({0}));<br>private bool _isAuditLog = false;<br>", tableName);
            result += listStringPK;

            result += string.Format("public {0}Dao(){{ }}<br>", tableName);
            result += string.Format("public {0}Dao(IDbContext ctx)<br>{{<br>_ctx = ctx;<br>}}<br>", tableName);

            result += string.Format("public {0} Get({1})<br>{{<br>_ctx.CommandText = _helper.GetRecord();<br>{2}DataRow row = DaoBase.GetDataRow(_ctx);<br>return (row == null) ? null : ({0})_helper.DataRowToObject(row, new {0}());<br>}}<br>", tableName, paramPK, ctxPK);

            result += string.Format("public int Insert({0} record)<br>{{<br>_helper.Insert(_ctx, record, _isAuditLog);<br>return DaoBase.ExecuteNonQuery(_ctx);<br>}}<br>", tableName);

            result += string.Format("public int Update({0} record)<br>{{<br>_helper.Update(_ctx, record, _isAuditLog);<br>return DaoBase.ExecuteNonQuery(_ctx, true);<br>}}<br>", tableName);

            result += string.Format("public int Delete({1})<br>{{<br>{0} record;<br>if (_ctx.Transaction == null)<br>record = new {0}Dao().Get({2});<br>else<br>record = Get({2});<br>_helper.Delete(_ctx, record, _isAuditLog);<br>return DaoBase.ExecuteNonQuery(_ctx);<br>}}<br>", tableName, paramPK, passParamPK);
            result += "}<br>#endregion";
            divResult.InnerHtml = result;




            string resultBusinessLayer = "";
            resultBusinessLayer = string.Format("#region {0}<br>", tableName);
            resultBusinessLayer += string.Format("public static {0} Get{0}({1})<br>{{<br>return new {0}Dao().Get({2});<br>}}<br>", tableName, paramPK, passParamPK);

            resultBusinessLayer += string.Format("public static int Insert{0}({0} record)<br>{{<br>return new {0}Dao().Insert(record);<br>}}<br>", tableName);

            resultBusinessLayer += string.Format("public static int Update{0}({0} record)<br>{{<br>return new {0}Dao().Update(record);<br>}}<br>", tableName);

            resultBusinessLayer += string.Format("public static int Delete{0}({1})<br>{{<br>return new {0}Dao().Delete({2});<br>}}<br>", tableName, paramPK, passParamPK);

            resultBusinessLayer += string.Format("public static List< {0} > Get{0}List(string filterExpression)<br>{{<br>", tableName);
            resultBusinessLayer += string.Format("List< {0} > result = new List< {0} >();<br>", tableName);
            resultBusinessLayer += "IDbContext ctx = DbFactory.Configure();<br>try<br>{<br>";
            resultBusinessLayer += string.Format("DbHelper helper = new DbHelper(typeof({0}));<br>ctx.CommandText = helper.Select(filterExpression);<br>", tableName);
            resultBusinessLayer += "using (IDataReader reader = DaoBase.GetDataReader(ctx))<br>while (reader.Read())<br>";
            resultBusinessLayer += string.Format("result.Add(({0})helper.IDataReaderToObject(reader, new {0}()));<br>", tableName);
            resultBusinessLayer += "}<br>catch (Exception ex)<br>{<br>throw new Exception(ex.Message, ex);<br>}<br>finally<br>{<br>ctx.Close();<br>}<br>return result;<br>}<br>#endregion";

            divBusinessLayer.InnerHtml = resultBusinessLayer;
        }

        protected void btnGenerateView_Click(object sender, EventArgs e)
        {
            int objectID = Convert.ToInt32(cboView.SelectedValue);
            List<SysColumns> lstColumns = BusinessLayer.GetSysColumnsList("OBJECT_ID = " + objectID);

            string tableName = txtViewName.Text;
            string oldTableName = cboView.SelectedItem.Text;
            List<String> listPK = BusinessLayer.GetSysColumnsPKList(tableName);

            String listMember = "";
            String listField = "";
            foreach (SysColumns col in lstColumns)
            {
                listMember += string.Format("private {0} _{1};<br>", col.Type, col.Name);

                listField += string.Format("[Column(Name = \"{0}\", DataType = \"{1}\")]<br>", col.Name, col.Type);
                listField += string.Format("public {1} {0}<br>{{<br>get {{ return _{0}; }}<br>set {{ _{0} = value; }}<br>}}<br>", col.Name, col.Type);

            }

            String result = "";
            result = String.Format("#region {0}<br>[Serializable]<br>[Table(Name = \"{1}\")]<br>public class {0}<br>{{<br>", tableName, oldTableName);
            result += listMember + "<br>";
            result += listField;
            result += "}<br>#endregion";

            divResult.InnerHtml = result;




            string resultBusinessLayer = "";
            resultBusinessLayer = string.Format("#region {0}<br>", tableName);

            resultBusinessLayer += string.Format("public static List< {0} > Get{0}List(string filterExpression)<br>{{<br>", tableName);
            resultBusinessLayer += string.Format("List< {0} > result = new List< {0} >();<br>", tableName);
            resultBusinessLayer += "IDbContext ctx = DbFactory.Configure();<br>try<br>{<br>";
            resultBusinessLayer += string.Format("DbHelper helper = new DbHelper(typeof({0}));<br>ctx.CommandText = helper.Select(filterExpression);<br>", tableName);
            resultBusinessLayer += "using (IDataReader reader = DaoBase.GetDataReader(ctx))<br>while (reader.Read())<br>";
            resultBusinessLayer += string.Format("result.Add(({0})helper.IDataReaderToObject(reader, new {0}()));<br>", tableName);
            resultBusinessLayer += "}<br>catch (Exception ex)<br>{<br>throw new Exception(ex.Message, ex);<br>}<br>finally<br>{<br>ctx.Close();<br>}<br>return result;<br>}<br>#endregion";

            divBusinessLayer.InnerHtml = resultBusinessLayer;
        }
    }
}