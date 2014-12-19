using System;
using QIS.Data.Core.Dal;

/***************************************************************************
 * $Archive: $
 * $Workfile: $
 * $Author: $
 * $Date: $
 * $Modtime: $  
 * $Revision: $
 ***************************************************************************/
namespace QIS.Careq.Data.Service
{
    #region GetUserMenuList
    [Serializable]
    [Table(Name = "GetUserMenuList")]
    public class GetUserMenuList
    {
        private Int32 _MenuID;
        private String _MenuCode;
        private String _ModuleID;
        private String _MenuCaption;
        private Int32 _RoleID;
        private String _CRUDModeUserRole;
        private Int32 _UserID;
        private Int32 _Level;
        private String _CRUDModeUser;

        [Column(Name = "MenuID", DataType = "Int32")]
        public Int32 MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }

        [Column(Name = "MenuCode", DataType = "String")]
        public String MenuCode
        {
            get { return _MenuCode; }
            set { _MenuCode = value; }
        }

        [Column(Name = "ModuleID", DataType = "String")]
        public String ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }

        [Column(Name = "MenuCaption", DataType = "String")]
        public String MenuCaption
        {
            get { return _MenuCaption; }
            set { _MenuCaption = value; }
        }

        [Column(Name = "RoleID", DataType = "Int32")]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        /// <summary>
        /// CRUDModeUserRole untuk menyimpan CRUDMode dari UserRole ybs. Jika Mode false, visible akan false.
        /// Untuk Visibility
        /// </summary>
        [Column(Name = "CRUDModeUserRole", DataType = "String")]
        public String CRUDModeUserRole
        {
            get { return _CRUDModeUserRole; }
            set { _CRUDModeUserRole = value; }
        }

        [Column(Name = "UserID", DataType = "Int32")]
        public Int32 UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        /// <summary>
        /// CRUDModeUser untuk menyimpan CRUDMode dari UserRole ybs
        /// </summary>
        [Column(Name = "CRUDModeUser", DataType = "String")]
        public String CRUDModeUser
        {
            get
            {
                if (_CRUDModeUser.Length > 0)
                    return _CRUDModeUser;
                return "------";
            }
            set { _CRUDModeUser = value; }
        }

        public Boolean CREATE
        {
            get { return CRUDModeUser.Contains("C"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[0] = "C";
                else
                    arr[0] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean READ
        {
            get { return CRUDModeUser.Contains("R"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[1] = "R";
                else
                    arr[1] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean UPDATE
        {
            get { return CRUDModeUser.Contains("U"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[2] = "U";
                else
                    arr[2] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean DELETE
        {
            get { return CRUDModeUser.Contains("D"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[3] = "D";
                else
                    arr[3] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean PRINT
        {
            get { return CRUDModeUser.Contains("P"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[4] = "P";
                else
                    arr[4] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean APPROVE
        {
            get { return CRUDModeUser.Contains("A"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[5] = "A";
                else
                    arr[5] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean VOID
        {
            get { return CRUDModeUser.Contains("V"); }
            set
            {
                string[] arr = CRUDModeUser.Split('-');
                if (value)
                    arr[6] = "V";
                else
                    arr[6] = "";
                CRUDModeUser = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean ENABLED
        {
            get { return CRUDModeUser.Contains("R"); }
        }

        public Boolean CVISIBLE
        {
            get { return _CRUDModeUserRole.Contains("C"); }
        }
        public Boolean RVISIBLE
        {
            get { return _CRUDModeUserRole.Contains("R"); }
        }
        public Boolean UVISIBLE
        {
            get { return _CRUDModeUserRole.Contains("U"); }
        }
        public Boolean DVISIBLE
        {
            get { return _CRUDModeUserRole.Contains("D"); }
        }
        public Boolean PVISIBLE
        {
            get { return _CRUDModeUserRole.Contains("P"); }
        }
        public Boolean AVISIBLE
        {
            get { return _CRUDModeUserRole.Contains("A"); }
        }
        public Boolean VVISIBLE
        {
            get { return _CRUDModeUserRole.Contains("V"); }
        }
    }
    #endregion
    #region GetUserRoleMenuList
    [Serializable]
    [Table(Name = "GetUserRoleMenuList")]
    public class GetUserRoleMenuList
    {
        private Int32 _ID;
        private Int32 _MenuID;
        private String _MenuCode;
        private String _ModuleID;
        private String _MenuCaption;
        private String _CRUDModeMenu;
        private Int32 _RoleID;
        private Int32 _Level;
        private String _CRUDModeUserRole;

        [Column(Name = "ID", DataType = "Int32")]
        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        [Column(Name = "MenuID", DataType = "Int32")]
        public Int32 MenuID
        {
            get { return _MenuID; }
            set { _MenuID = value; }
        }

        [Column(Name = "MenuCode", DataType = "String")]
        public String MenuCode
        {
            get { return _MenuCode; }
            set { _MenuCode = value; }
        }

        [Column(Name = "ModuleID", DataType = "String")]
        public String ModuleID
        {
            get { return _ModuleID; }
            set { _ModuleID = value; }
        }

        [Column(Name = "MenuCaption", DataType = "String")]
        public String MenuCaption
        {
            get { return _MenuCaption; }
            set { _MenuCaption = value; }
        }

        [Column(Name = "CRUDModeMenu", DataType = "String")]
        public String CRUDModeMenu
        {
            get { return _CRUDModeMenu; }
            set { _CRUDModeMenu = value; }
        }

        [Column(Name = "RoleID", DataType = "Int32")]
        public Int32 RoleID
        {
            get { return _RoleID; }
            set { _RoleID = value; }
        }

        [Column(Name = "Level", DataType = "Int32")]
        public Int32 Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        [Column(Name = "CRUDModeUserRole", DataType = "String")]
        public String CRUDModeUserRole
        {
            get
            {
                if (_CRUDModeUserRole.Length > 0)
                    return _CRUDModeUserRole;
                return "------";
            }
            set { _CRUDModeUserRole = value; }
        }

        public Boolean CREATE
        {
            get { return CRUDModeUserRole.Contains("C"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[0] = "C";
                else
                    arr[0] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean READ
        {
            get { return CRUDModeUserRole.Contains("R"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[1] = "R";
                else
                    arr[1] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean UPDATE
        {
            get { return CRUDModeUserRole.Contains("U"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[2] = "U";
                else
                    arr[2] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean DELETE
        {
            get { return CRUDModeUserRole.Contains("D"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[3] = "D";
                else
                    arr[3] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean PRINT
        {
            get { return CRUDModeUserRole.Contains("P"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[4] = "P";
                else
                    arr[4] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean APPROVE
        {
            get { return CRUDModeUserRole.Contains("A"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[5] = "A";
                else
                    arr[5] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean VOID
        {
            get { return CRUDModeUserRole.Contains("V"); }
            set
            {
                string[] arr = CRUDModeUserRole.Split('-');
                if (value)
                    arr[6] = "V";
                else
                    arr[6] = "";
                CRUDModeUserRole = arr[0] + "-" + arr[1] + "-" + arr[2] + "-" + arr[3] + "-" + arr[4] + "-" + arr[5] + "-" + arr[6];
            }
        }
        public Boolean ENABLED
        {
            get { return _CRUDModeUserRole.Contains("R"); }
        }

        public Boolean CVISIBLE
        {
            get { return _CRUDModeMenu.Contains("C"); }
        }
        public Boolean RVISIBLE
        {
            get { return _CRUDModeMenu.Contains("R"); }
        }
        public Boolean UVISIBLE
        {
            get { return _CRUDModeMenu.Contains("U"); }
        }
        public Boolean DVISIBLE
        {
            get { return _CRUDModeMenu.Contains("D"); }
        }
        public Boolean PVISIBLE
        {
            get { return _CRUDModeMenu.Contains("P"); }
        }
        public Boolean AVISIBLE
        {
            get { return _CRUDModeMenu.Contains("A"); }
        }
        public Boolean VVISIBLE
        {
            get { return _CRUDModeMenu.Contains("V"); }
        }
    }
    #endregion



    #region GetCurrentItemCost
    public partial class GetCurrentItemCost
    {
        private Decimal _Total;

        [Column(Name = "Total", DataType = "Decimal")]
        public Decimal Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
    }
    #endregion
    #region GetCurrentItemTariff
    public partial class GetCurrentItemTariff
    {
        private Decimal _Tariff;

        [Column(Name = "Tariff", DataType = "Decimal")]
        public Decimal Tariff
        {
            get { return _Tariff; }
            set { _Tariff = value; }
        }
    }
    #endregion
}