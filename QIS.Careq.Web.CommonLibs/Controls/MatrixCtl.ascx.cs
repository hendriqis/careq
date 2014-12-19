using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QIS.Careq.Web.Common.UI;
using QIS.Careq.Data.Service;
using QIS.Careq.Web.Common;
using DevExpress.Web.ASPxCallbackPanel;
using QIS.Data.Core.Dal;
using System.Collections;

namespace QIS.Careq.Web.CommonLibs.Controls
{
    public partial class MatrixCtl : BaseViewPopupCtl
    {
        #region Company Certification
        private void InitializeCompanyCertification(string queryString)
        {
            lblHeader.InnerText = "Company";

            Company module = BusinessLayer.GetCompany(Convert.ToInt32(queryString));
            txtHeader.Text = string.Format("{0} - {1}", module.CompanyCode, module.CompanyName);

            List<StandardCode> lstAvailable = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND StandardCodeID NOT IN (SELECT GCCompanyCertification FROM CompanyCertification WHERE CompanyID = {1}) AND IsDeleted = 0 ORDER BY StandardCodeName", Constant.StandardCode.COMPANY_CERTIFICATION, queryString));
            List<vCompanyCertification> lstSelected = BusinessLayer.GetvCompanyCertificationList(string.Format("CompanyID = {0} ORDER BY CompanyCertification ASC", queryString));

            ListAvailable = (from p in lstAvailable
                             select new CMatrix { IsChecked = false, ID = p.StandardCodeID.ToString(), Name = p.StandardCodeName }).ToList();

            ListSelected = (from p in lstSelected
                            select new CMatrix { IsChecked = false, ID = p.GCCompanyCertification.ToString(), Name = p.CompanyCertification }).ToList();
        }

        private bool SaveCompanyCertification(string queryString, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                Int32 companyID = Convert.ToInt32(queryString);
                CompanyCertificationDao entityDao = new CompanyCertificationDao(ctx);
                foreach (ProceedEntity row in ListProceedEntity)
                {
                    if (row.Status == ProceedEntity.ProceedEntityStatus.Add)
                    {
                        CompanyCertification entity = new CompanyCertification();
                        entity.GCCompanyCertification = row.ID;
                        entity.CompanyID = companyID;
                        entityDao.Insert(entity);
                    }
                    else
                        entityDao.Delete(companyID, row.ID);
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Member Greetings
        private void InitializeMemberGreetings(string queryString)
        {
            lblHeader.InnerText = "Member";

            vMember module = BusinessLayer.GetvMemberList(string.Format("MemberID = {0}", queryString))[0];
            txtHeader.Text = module.MemberName;

            List<StandardCode> lstAvailable = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND StandardCodeID NOT IN (SELECT GCGreetingType FROM MemberGreetings WHERE MemberID = {1}) AND IsDeleted = 0 ORDER BY StandardCodeName", Constant.StandardCode.HOLIDAY_GREETINGS, queryString));
            List<vMemberGreetings> lstSelected = BusinessLayer.GetvMemberGreetingsList(string.Format("MemberID = {0} ORDER BY GreetingType ASC", queryString));

            ListAvailable = (from p in lstAvailable
                             select new CMatrix { IsChecked = false, ID = p.StandardCodeID.ToString(), Name = p.StandardCodeName }).ToList();

            ListSelected = (from p in lstSelected
                            select new CMatrix { IsChecked = false, ID = p.GCGreetingType.ToString(), Name = p.GreetingType }).ToList();
        }

        private bool SaveMemberGreetings(string queryString, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                Int32 memberID = Convert.ToInt32(queryString);
                MemberGreetingsDao entityDao = new MemberGreetingsDao(ctx);
                foreach (ProceedEntity row in ListProceedEntity)
                {
                    if (row.Status == ProceedEntity.ProceedEntityStatus.Add)
                    {
                        MemberGreetings entity = new MemberGreetings();
                        entity.GCGreetingType = row.ID;
                        entity.MemberID = memberID;
                        entityDao.Insert(entity);
                    }
                    else
                        entityDao.Delete(memberID, row.ID);
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Module Menu
        private void InitializeModuleMenu(string queryString)
        {
            lblHeader.InnerText = "Module";

            Module module = BusinessLayer.GetModule(queryString);
            txtHeader.Text = string.Format("{0} - {1}", module.ModuleID, module.ModuleName);

            List<MenuMaster> ListAvailableMenu = BusinessLayer.GetMenuMasterList(string.Format("ModuleID != '{0}' OR ModuleID IS NULL ORDER BY MenuCaption ASC", queryString));
            List<MenuMaster> ListSelectedMenu = BusinessLayer.GetMenuMasterList(string.Format("ModuleID = '{0}' ORDER BY MenuCaption ASC", queryString));

            ListAvailable = (from p in ListAvailableMenu
                             select new CMatrix { IsChecked = false, ID = p.MenuID.ToString(), Name = p.MenuCaption }).ToList();

            ListSelected = (from p in ListSelectedMenu
                            select new CMatrix { IsChecked = false, ID = p.MenuID.ToString(), Name = p.MenuCaption }).ToList();
        }

        private bool SaveModuleMenu(string queryString, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                string ModuleID = queryString;
                MenuMasterDao entityDao = new MenuMasterDao(ctx);
                foreach (ProceedEntity row in ListProceedEntity)
                {
                    if (row.Status == ProceedEntity.ProceedEntityStatus.Add)
                    {
                        Int32 MenuID = Convert.ToInt32(row.ID);
                        MenuMaster entity = entityDao.Get(MenuID);
                        entity.ModuleID = ModuleID;
                        entityDao.Update(entity);
                    }
                    else
                    {
                        Int32 MenuID = Convert.ToInt32(row.ID);
                        MenuMaster entity = entityDao.Get(MenuID);
                        entity.ModuleID = null;
                        entityDao.Update(entity);
                    }
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Training Certification
        private void InitializeTrainingCertification(string queryString)
        {
            lblHeader.InnerText = "Training";

            Training module = BusinessLayer.GetTraining(Convert.ToInt32(queryString));
            txtHeader.Text = string.Format("{0} - {1}", module.TrainingCode, module.TrainingName);

            List<StandardCode> lstAvailable = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND StandardCodeID NOT IN (SELECT GCCompanyCertification FROM TrainingCertification WHERE TrainingID = {1}) AND IsDeleted = 0 ORDER BY StandardCodeName", Constant.StandardCode.COMPANY_CERTIFICATION, queryString));
            List<vTrainingCertification> lstSelected = BusinessLayer.GetvTrainingCertificationList(string.Format("TrainingID = {0} ORDER BY CompanyCertification ASC", queryString));

            ListAvailable = (from p in lstAvailable
                             select new CMatrix { IsChecked = false, ID = p.StandardCodeID.ToString(), Name = p.StandardCodeName }).ToList();

            ListSelected = (from p in lstSelected
                            select new CMatrix { IsChecked = false, ID = p.GCCompanyCertification.ToString(), Name = p.CompanyCertification }).ToList();
        }

        private bool SaveTrainingCertification(string queryString, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                Int32 trainingID = Convert.ToInt32(queryString);
                TrainingCertificationDao entityDao = new TrainingCertificationDao(ctx);
                foreach (ProceedEntity row in ListProceedEntity)
                {
                    if (row.Status == ProceedEntity.ProceedEntityStatus.Add)
                    {
                        TrainingCertification entity = new TrainingCertification();
                        entity.GCCompanyCertification = row.ID;
                        entity.TrainingID = trainingID;
                        entityDao.Insert(entity);
                    }
                    else
                        entityDao.Delete(trainingID, row.ID);
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Training Department
        private void InitializeTrainingDepartment(string queryString)
        {
            lblHeader.InnerText = "Training";

            Training module = BusinessLayer.GetTraining(Convert.ToInt32(queryString));
            txtHeader.Text = string.Format("{0} - {1}", module.TrainingCode, module.TrainingName);

            List<StandardCode> lstAvailable = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND StandardCodeID NOT IN (SELECT GCDepartment FROM TrainingDepartment WHERE TrainingID = {1}) AND IsDeleted = 0 ORDER BY StandardCodeName", Constant.StandardCode.COMPANY_DEPARTMENT, queryString));
            List<vTrainingDepartment> lstSelected = BusinessLayer.GetvTrainingDepartmentList(string.Format("TrainingID = {0} ORDER BY Department ASC", queryString));

            ListAvailable = (from p in lstAvailable
                             select new CMatrix { IsChecked = false, ID = p.StandardCodeID.ToString(), Name = p.StandardCodeName }).ToList();

            ListSelected = (from p in lstSelected
                            select new CMatrix { IsChecked = false, ID = p.GCDepartment.ToString(), Name = p.Department }).ToList();
        }

        private bool SaveTrainingDepartment(string queryString, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                Int32 trainingID = Convert.ToInt32(queryString);
                TrainingDepartmentDao entityDao = new TrainingDepartmentDao(ctx);
                foreach (ProceedEntity row in ListProceedEntity)
                {
                    if (row.Status == ProceedEntity.ProceedEntityStatus.Add)
                    {
                        TrainingDepartment entity = new TrainingDepartment();
                        entity.GCDepartment = row.ID;
                        entity.TrainingID = trainingID;
                        entityDao.Insert(entity);
                    }
                    else
                        entityDao.Delete(trainingID, row.ID);
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region Training Occupation
        private void InitializeTrainingOccupation(string queryString)
        {
            lblHeader.InnerText = "Training";

            Training module = BusinessLayer.GetTraining(Convert.ToInt32(queryString));
            txtHeader.Text = string.Format("{0} - {1}", module.TrainingCode, module.TrainingName);

            List<StandardCode> lstAvailable = BusinessLayer.GetStandardCodeList(string.Format("ParentID = '{0}' AND StandardCodeID NOT IN (SELECT GCOccupationLevel FROM TrainingOccupation WHERE TrainingID = {1}) AND IsDeleted = 0 ORDER BY StandardCodeName", Constant.StandardCode.OCCUPATION_LEVEL, queryString));
            List<vTrainingOccupation> lstSelected = BusinessLayer.GetvTrainingOccupationList(string.Format("TrainingID = {0} ORDER BY OccupationLevel ASC", queryString));

            ListAvailable = (from p in lstAvailable
                             select new CMatrix { IsChecked = false, ID = p.StandardCodeID.ToString(), Name = p.StandardCodeName }).ToList();

            ListSelected = (from p in lstSelected
                            select new CMatrix { IsChecked = false, ID = p.GCOccupationLevel.ToString(), Name = p.OccupationLevel }).ToList();
        }

        private bool SaveTrainingOccupation(string queryString, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                Int32 trainingID = Convert.ToInt32(queryString);
                TrainingOccupationDao entityDao = new TrainingOccupationDao(ctx);
                foreach (ProceedEntity row in ListProceedEntity)
                {
                    if (row.Status == ProceedEntity.ProceedEntityStatus.Add)
                    {
                        TrainingOccupation entity = new TrainingOccupation();
                        entity.GCOccupationLevel = row.ID;
                        entity.TrainingID = trainingID;
                        entityDao.Insert(entity);
                    }
                    else
                        entityDao.Delete(trainingID, row.ID);
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region UserRoleSelectUser
        private void InitializeUserRoleSelectUser(string queryString)
        {
            lblHeader.InnerText = GetLabel("UserRole");

            UserRole ur = BusinessLayer.GetUserRole(Convert.ToInt32(queryString));
            txtHeader.Text = ur.RoleName;

            string filterExpression = string.Format("UserID NOT IN (SELECT UserID FROM UserInRole WHERE RoleID = {0})", queryString);
            if (AppSession.UserLogin.RoleID > 1)
                filterExpression += " AND UserID NOT IN (SELECT UserID FROM UserInRole WHERE RoleID = 1) AND IsDeleted = 0";
            else if (AppSession.UserLogin.UserID > 1)
                filterExpression += " AND UserID != 1 AND IsDeleted = 0";
            List<User> lstAvailable = BusinessLayer.GetUserList(filterExpression);

            filterExpression = string.Format("RoleID = {0}", queryString);
            if (AppSession.UserLogin.RoleID > 1)
                filterExpression += " AND UserID NOT IN (SELECT UserID FROM UserInRole WHERE RoleID = 1)";
            else if (AppSession.UserLogin.UserID > 1)
                filterExpression += " AND UserID != 1";
            List<vUserInRole> lstSelected = BusinessLayer.GetvUserInRoleList(filterExpression);

            ListAvailable = (from p in lstAvailable
                             select new CMatrix { IsChecked = false, ID = p.UserID.ToString(), Name = p.UserName }).ToList();

            ListSelected = (from p in lstSelected
                            select new CMatrix { IsChecked = false, ID = p.UserID.ToString(), Name = p.UserName }).ToList();
        }

        private bool SaveUserRoleSelectUser(string queryString, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                int RoleID = Convert.ToInt32(queryString);
                UserInRoleDao entityDao = new UserInRoleDao(ctx);
                foreach (ProceedEntity row in ListProceedEntity)
                {
                    if (row.Status == ProceedEntity.ProceedEntityStatus.Add)
                    {
                        UserInRole entity = new UserInRole();
                        entity.RoleID = RoleID;
                        entity.UserID = Convert.ToInt32(row.ID);
                        entityDao.Insert(entity);
                    }
                    else
                        entityDao.Delete(Convert.ToInt32(row.ID), RoleID);
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion
        #region UserSelectUserRole
        private void InitializeUserSelectUserRole(string queryString)
        {
            lblHeader.InnerText = GetLabel("UserName");

            User ur = BusinessLayer.GetUser(Convert.ToInt32(queryString));
            txtHeader.Text = ur.UserName;

            string filterExpression = string.Format("RoleID NOT IN (SELECT RoleID FROM UserInRole WHERE UserID = {0}) AND IsDeleted = 0", queryString);
            if (AppSession.UserLogin.UserID > 1)
                filterExpression += " AND RoleID != 1";
            List<UserRole> lstAvailable = BusinessLayer.GetUserRoleList(filterExpression);

            filterExpression = string.Format("UserID = {0}", queryString);
            if (AppSession.UserLogin.UserID > 1)
                filterExpression += " AND RoleID != 1";
            List<vUserInRole> lstSelected = BusinessLayer.GetvUserInRoleList(filterExpression);

            ListAvailable = (from p in lstAvailable
                             select new CMatrix { IsChecked = false, ID = p.RoleID.ToString(), Name = p.RoleName }).ToList();

            ListSelected = (from p in lstSelected
                            select new CMatrix { IsChecked = false, ID = p.RoleID.ToString(), Name = p.RoleName }).ToList();
        }

        private bool SaveUserSelectUserRole(string queryString, ref string errMessage)
        {
            IDbContext ctx = DbFactory.Configure(true);
            bool result = false;
            try
            {
                int UserID = Convert.ToInt32(queryString);
                UserInRoleDao entityDao = new UserInRoleDao(ctx);
                foreach (ProceedEntity row in ListProceedEntity)
                {
                    if (row.Status == ProceedEntity.ProceedEntityStatus.Add)
                    {
                        UserInRole entity = new UserInRole();
                        entity.UserID = UserID;
                        entity.RoleID = Convert.ToInt32(row.ID);
                        entityDao.Insert(entity);
                    }
                    else
                        entityDao.Delete(UserID, Convert.ToInt32(row.ID));
                }
                ctx.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                ctx.RollBackTransaction();
                result = false;
                errMessage = ex.Message;
            }
            finally
            {
                ctx.Close();
            }
            return result;
        }
        #endregion

        private void InitializeListMatrix(string type, string queryString)
        {
            switch (type)
            {
                case "CompanyCertification": InitializeCompanyCertification(queryString); break;
                case "MemberGreetings": InitializeMemberGreetings(queryString); break;
                case "ModuleMenu": InitializeModuleMenu(queryString); break;
                case "TrainingCertification": InitializeTrainingCertification(queryString); break;
                case "TrainingDepartment": InitializeTrainingDepartment(queryString); break;
                case "TrainingOccupation": InitializeTrainingOccupation(queryString); break;
                case "UserRoleSelectUser": InitializeUserRoleSelectUser(queryString); break;
                case "UserSelectUserRole": InitializeUserSelectUserRole(queryString); break;
            }
        }

        private bool SaveMatrix(string type, string queryString, ref string errMessage)
        {
            switch (type)
            {
                case "CompanyCertification": return SaveCompanyCertification(queryString, ref errMessage);
                case "MemberGreetings": return SaveMemberGreetings(queryString, ref errMessage);
                case "ModuleMenu": return SaveModuleMenu(queryString, ref errMessage);
                case "TrainingCertification": return SaveTrainingCertification(queryString, ref errMessage);
                case "TrainingDepartment": return SaveTrainingDepartment(queryString, ref errMessage);
                case "TrainingOccupation": return SaveTrainingOccupation(queryString, ref errMessage);
                case "UserRoleSelectUser": return SaveUserRoleSelectUser(queryString, ref errMessage);
                case "UserSelectUserRole": return SaveUserSelectUserRole(queryString, ref errMessage);
            }
            return false;    
        }



        protected int PageCountAvailable = 1;
        protected int PageCountSelected = 1;

        public override void InitializeDataControl(string param)
        {
            ListProceedEntity.Clear();
            hdnParam.Value = param;

            string type = param.Split('|')[0];
            string[] temp = param.Split('|').Skip(1).ToArray();
            string queryString = String.Join("|", temp);

            InitializeListMatrix(type, queryString);

            BindGridAvailable(1, true, ref PageCountAvailable);
            BindGridSelected(1, true, ref PageCountSelected);
        }

        #region Available
        private void BindGridAvailable(int pageIndex, bool isCountPageCount, ref int pageCount, List<string> listCheckedAvailable = null)
        {
            List<CMatrix> lstEntity = ListAvailable.Where(p => p.Name.Contains(hdnAvailableSearchText.Value)).ToList();
            if (isCountPageCount)
            {
                pageCount = Helper.GetPageCount(lstEntity.Count, Constant.GridViewPageSize.GRID_MATRIX);
            }
            List<CMatrix> lst = lstEntity.Skip((pageIndex - 1) * 10).Take(10).ToList();
            foreach (CMatrix mtx in lst)
            {
                if (listCheckedAvailable != null && listCheckedAvailable.Contains(mtx.ID.ToString()))
                {
                    mtx.IsChecked = true;
                    listCheckedAvailable.Remove(mtx.ID.ToString());
                }
                else
                    mtx.IsChecked = false;
            }

            grdAvailable.DataSource = lst;
            grdAvailable.DataBind();
        }

        protected void cbpMatrixAvailable_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            List<string> listCheckedAvailable = hdnCheckedAvailable.Value.Split(';').ToList();
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    string[] newCheckedAvailable = param[2].Split(';');
                    foreach (string a in newCheckedAvailable)
                    {
                        if (a != "")
                            listCheckedAvailable.Add(a);
                    }

                    BindGridAvailable(Convert.ToInt32(param[1]), false, ref pageCount, listCheckedAvailable);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridAvailable(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpCheckedAvailable"] = string.Join(";", listCheckedAvailable.ToArray());
        }
        #endregion

        #region Selected
        private void BindGridSelected(int pageIndex, bool isCountPageCount, ref int pageCount, List<string> listCheckedSelected = null)
        {
            List<CMatrix> lstEntity = ListSelected.Where(p => p.Name.Contains(hdnSelectedSearchText.Value)).ToList();
            if (isCountPageCount)
            {
                pageCount = Helper.GetPageCount(lstEntity.Count, Constant.GridViewPageSize.GRID_MATRIX);
            }
            List<CMatrix> lst = lstEntity.Skip((pageIndex - 1) * 10).Take(10).ToList();
            foreach (CMatrix mtx in lst)
            {
                if (listCheckedSelected != null && listCheckedSelected.Contains(mtx.ID.ToString()))
                {
                    mtx.IsChecked = true;
                    listCheckedSelected.Remove(mtx.ID.ToString());
                }
                else
                    mtx.IsChecked = false;
            }

            grdSelected.DataSource = lst;
            grdSelected.DataBind();
        }

        protected void cbpMatrixSelected_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            int pageCount = 1;
            string result = "";
            List<string> listCheckedSelected = hdnCheckedSelected.Value.Split(';').ToList();
            if (e.Parameter != null && e.Parameter != "")
            {
                string[] param = e.Parameter.Split('|');
                if (param[0] == "changepage")
                {
                    string[] newCheckedSelected = param[2].Split(';');
                    foreach (string a in newCheckedSelected)
                    {
                        if (a != "")
                            listCheckedSelected.Add(a);
                    }

                    BindGridSelected(Convert.ToInt32(param[1]), false, ref pageCount, listCheckedSelected);
                    result = "changepage";
                }
                else // refresh
                {
                    BindGridSelected(1, true, ref pageCount);
                    result = "refresh|" + pageCount;
                }
            }

            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
            panel.JSProperties["cpCheckedSelected"] = string.Join(";", listCheckedSelected.ToArray());

        }
        #endregion


        protected void cbpMatrixProcess_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
        {
            string[] param = e.Parameter.Split('|');
            string result = param[0] + "|";
            if (param[0] == "rightAll")
            {
                List<CMatrix> lst = ListAvailable.Where(p => p.Name.Contains(hdnAvailableSearchText.Value)).ToList();
                foreach (CMatrix row in lst)
                {
                    ListSelected.Add(row); 

                    ProceedEntity obj = ListProceedEntity.FirstOrDefault(p => p.ID == row.ID.ToString());
                    if (obj != null)
                        ListProceedEntity.Remove(obj);
                    else
                    {
                        ProceedEntity proceedEntity = new ProceedEntity();
                        proceedEntity.ID = row.ID.ToString();
                        proceedEntity.Status = ProceedEntity.ProceedEntityStatus.Add;
                        ListProceedEntity.Add(proceedEntity);
                    }
                }
                ListSelected = ListSelected.OrderBy(p => p.Name).ToList();
                ListAvailable.RemoveAll(x => x.Name.Contains(hdnAvailableSearchText.Value));
            }
            else if (param[0] == "right")
            {
                List<string> listCheckedAvailable = hdnCheckedAvailable.Value.Split(';').ToList();
                string[] newCheckedAvailable = param[1].Split(';');
                foreach (string a in newCheckedAvailable)
                {
                    if (a != "")
                        listCheckedAvailable.Add(a);
                }

                foreach (string value in listCheckedAvailable)
                {
                    if (value != "")
                    {
                        ProceedEntity obj = ListProceedEntity.FirstOrDefault(p => p.ID == value.ToString());
                        if (obj != null)
                            ListProceedEntity.Remove(obj);
                        else
                        {
                            ProceedEntity proceedEntity = new ProceedEntity();
                            proceedEntity.ID = value.ToString();
                            proceedEntity.Status = ProceedEntity.ProceedEntityStatus.Add;
                            ListProceedEntity.Add(proceedEntity);
                        }

                        CMatrix removeObj = ListAvailable.FirstOrDefault(p => p.ID.ToString() == value);
                        if (removeObj != null)
                        {
                            ListSelected.Add(removeObj);
                            ListAvailable.Remove(removeObj);
                        }
                    }
                }

                ListSelected = ListSelected.OrderBy(p => p.Name).ToList();
            }
            else if (param[0] == "left")
            {
                List<string> listCheckedSelected = hdnCheckedSelected.Value.Split(';').ToList();
                string[] newCheckedSelected = param[1].Split(';');
                foreach (string a in newCheckedSelected)
                {
                    if (a != "")
                        listCheckedSelected.Add(a);
                }

                foreach (string value in listCheckedSelected)
                {
                    if (value != "")
                    {
                        ProceedEntity obj = ListProceedEntity.FirstOrDefault(p => p.ID == value.ToString());
                        if (obj != null)
                            ListProceedEntity.Remove(obj);
                        else
                        {
                            ProceedEntity proceedEntity = new ProceedEntity();
                            proceedEntity.ID = value.ToString();
                            proceedEntity.Status = ProceedEntity.ProceedEntityStatus.Remove;
                            ListProceedEntity.Add(proceedEntity);
                        }

                        CMatrix removeObj = ListSelected.FirstOrDefault(p => p.ID.ToString() == value);
                        if (removeObj != null)
                        {
                            ListAvailable.Add(removeObj);
                            ListSelected.Remove(removeObj);
                        }
                    }
                }

                ListAvailable = ListAvailable.OrderBy(p => p.Name).ToList();
            }
            else if (param[0] == "leftAll")
            {
                List<CMatrix> lst = ListSelected.Where(p => p.Name.Contains(hdnSelectedSearchText.Value)).ToList();
                foreach (CMatrix row in lst)
                {
                    ListAvailable.Add(row);

                    ProceedEntity obj = ListProceedEntity.FirstOrDefault(p => p.ID == row.ID.ToString());
                    if (obj != null)
                        ListProceedEntity.Remove(obj);
                    else
                    {
                        ProceedEntity proceedEntity = new ProceedEntity();
                        proceedEntity.ID = row.ID.ToString();
                        proceedEntity.Status = ProceedEntity.ProceedEntityStatus.Remove;
                        ListProceedEntity.Add(proceedEntity);
                    }
                }
                ListAvailable = ListAvailable.OrderBy(p => p.Name).ToList();
                ListSelected.RemoveAll(x => x.Name.Contains(hdnSelectedSearchText.Value));
            }
            else if (param[0] == "save")
            {
                string errMessage = "";
                string paramTemp = hdnParam.Value;

                string type = paramTemp.Split('|')[0];
                string[] temp = paramTemp.Split('|').Skip(1).ToArray();
                string queryString = String.Join("|", temp);

                if (SaveMatrix(type, queryString, ref errMessage))
                    result += "success";
                else
                    result += "fail|" + errMessage;
            }
            ASPxCallbackPanel panel = sender as ASPxCallbackPanel;
            panel.JSProperties["cpResult"] = result;
        }

        private const string SESSION_NAME_SELECTED_ENTITY = "SelectedEntity";
        private const string SESSION_NAME_AVAILABLE_ENTITY = "AvailableEntity";
        private const string SESSION_PROCEED_ENTITY = "ProceedEntity";

        #region Matrix       
        public static List<CMatrix> ListSelected
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_NAME_SELECTED_ENTITY] == null) HttpContext.Current.Session[SESSION_NAME_SELECTED_ENTITY] = new List<CMatrix>();
                return (List<CMatrix>)HttpContext.Current.Session[SESSION_NAME_SELECTED_ENTITY];
            }
            set
            {
                HttpContext.Current.Session[SESSION_NAME_SELECTED_ENTITY] = value;
            }
        }
        public static List<CMatrix> ListAvailable
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_NAME_AVAILABLE_ENTITY] == null) HttpContext.Current.Session[SESSION_NAME_AVAILABLE_ENTITY] = new List<CMatrix>();
                return (List<CMatrix>)HttpContext.Current.Session[SESSION_NAME_AVAILABLE_ENTITY];
            }
            set
            {
                HttpContext.Current.Session[SESSION_NAME_AVAILABLE_ENTITY] = value;
            }
        }

        private static List<ProceedEntity> ListProceedEntity
        {
            get
            {
                if (HttpContext.Current.Session[SESSION_PROCEED_ENTITY] == null) HttpContext.Current.Session[SESSION_PROCEED_ENTITY] = new List<ProceedEntity>();
                return (List<ProceedEntity>)HttpContext.Current.Session[SESSION_PROCEED_ENTITY];
            }
            set
            {
                HttpContext.Current.Session[SESSION_PROCEED_ENTITY] = value;
            }
        }

        public static List<CMatrix> SelectListAvailableEntity()
        {
            return ListAvailable;
        }
        public static List<CMatrix> SelectListSelectedEntity()
        {
            return ListSelected;
        }
        #endregion
    }
}