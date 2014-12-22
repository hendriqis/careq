using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using QIS.Data.Core.Dal;

namespace QIS.Careq.Data.Service
{
    public static partial class BusinessLayer
    {
        #region GetUserMenuList
        public static List<GetUserMenuList> GetUserMenuList(String moduleID, Int32 roleID, Int32 userID, Int32 loginRoleID, Int32 loginUserID)
        {
            List<GetUserMenuList> result = new List<GetUserMenuList>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetUserMenuList));
                ctx.CommandText = "GetUserMenuList";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("p_ModuleID", moduleID);
                ctx.Add("p_RoleID", roleID);
                ctx.Add("p_UserID", userID);
                ctx.Add("p_LoginRoleID", loginRoleID);
                ctx.Add("p_LoginUserID", loginUserID);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetUserMenuList)helper.IDataReaderToObject(reader, new GetUserMenuList()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region GetUserRoleMenuList
        public static List<GetUserRoleMenuList> GetUserRoleMenuList(String moduleID, Int32 roleID, Int32 loginRoleID, Int32 loginUserID)
        {
            List<GetUserRoleMenuList> result = new List<GetUserRoleMenuList>();
            IDbContext ctx = DbFactory.Configure();
            try
            {
                DbHelper helper = new DbHelper(typeof(GetUserRoleMenuList));
                ctx.CommandText = "GetUserRoleMenuList";
                ctx.CommandType = CommandType.StoredProcedure;
                //Add Parameter
                ctx.Add("p_ModuleID", moduleID);
                ctx.Add("p_RoleID", roleID);
                ctx.Add("p_LoginRoleID", loginRoleID);
                ctx.Add("p_LoginUserID", loginUserID);
                //Get DataReader
                using (IDataReader reader = DaoBase.GetDataReader(ctx))
                    while (reader.Read())
                        result.Add((GetUserRoleMenuList)helper.IDataReaderToObject(reader, new GetUserRoleMenuList()));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region GetMaxCertificationNo
        public static string GetMaxCertificationNo(Int32 EventID, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "GetMaxCertificationNo";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@EventID", EventID));
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@Result";
            param.SqlDbType = SqlDbType.VarChar;
            param.Size = 30;
            param.Direction = ParameterDirection.Output;

            ctx.Command.Parameters.Add(param);

            try
            {
                DaoBase.ExecuteNonQuery(ctx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (IsCtxNull)
                    ctx.Close();
            }

            return (string)param.Value;
        }
        #endregion
        #region GenerateEventCode
        public static string GenerateEventCode(Int32 TrainingID, String EventYear, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "GenerateEventCode";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@TrainingID", TrainingID));
            ctx.Command.Parameters.Add(new SqlParameter("@EventYear", EventYear));
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@Result";
            param.SqlDbType = SqlDbType.VarChar;
            param.Size = 30;
            param.Direction = ParameterDirection.Output;

            ctx.Command.Parameters.Add(param);

            try
            {
                DaoBase.ExecuteNonQuery(ctx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (IsCtxNull)
                    ctx.Close();
            }

            return (string)param.Value;
        }
        #endregion
        #region GenerateInquiryCode
        public static string GenerateInquiryCode(String InquiryYear, IDbContext ctx = null)
        {
            bool IsCtxNull = false;
            if (ctx == null)
            {
                IsCtxNull = true;
                ctx = DbFactory.Configure();
            }
            ctx.CommandText = "GenerateInquiryCode";
            ctx.CommandType = CommandType.StoredProcedure;
            ctx.Command.Parameters.Add(new SqlParameter("@InquiryYear", InquiryYear));
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@Result";
            param.SqlDbType = SqlDbType.VarChar;
            param.Size = 30;
            param.Direction = ParameterDirection.Output;

            ctx.Command.Parameters.Add(param);

            try
            {
                DaoBase.ExecuteNonQuery(ctx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (IsCtxNull)
                    ctx.Close();
            }

            return (string)param.Value;
        }
        #endregion
    }
}