using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QIS.Careq.Web.Common
{
    public class Constant
    {
        public static string DEFAULT_EMAIL_TEMPLATE_TEXT = "EmailTemplate.txt";
        public static string DEFAULT_EMAIL_CONFIRMATION_TEXT = "EmailConfirmation.txt";
        
        public static class FormatCode
        {
            public const string COMPANY = "C*****";
            public const string MEMBER = "********";
        }

        public static class GridViewPageSize
        {
            public const int GRID_MASTER = 18;
            public const int GRID_MATRIX = 10;
        }

        public static class DefaultValueEntry
        {
            public const string DATE_NOW = "@DateNow";
            public const string TIME_NOW = "@TimeNow";
        }

        public static class FormatString
        {
            public const string DATE_PICKER_FORMAT = "dd-MM-yyyy";
            public const string DATE_FORMAT_112 = "yyyyMMdd";
        }

        public static class StandardField
        {
            public const string TITLE = "TITLE";
            public const string PENGIRIM = "PENGIRIM";
            public const string VISIT_TYPE = "TPVISIT";
            public const string PENJAMIN_BAYAR = "JMBAYAR";
            public const string SHIFT = "SHIFT";
        }

        public static class TransactionCode
        {
            public const string REGISTRATION = "001";
            public const string APPOINTMENT = "002";
            public const string CHARGES = "003";
            public const string TEST_ORDER = "004";
            public const string JOB_ORDER = "005";
            public const string EXTERNAL_JOB_ORDER = "006";
            public const string CONSULT_VISIT = "008";
            public const string PURCHASE_REQUEST = "034";
            public const string PURCHASE_ORDER = "037";
            public const string GOODS_RECEIVED = "040";
            public const string GOODS_RETURN = "043";
            public const string DISTRIBUTION = "050";
            public const string GOODS_REQUEST = "053";
            public const string TRANSFER_ORDER = "057";
            public const string TRANSFER_ORDER_RECEIVED = "059";
            public const string CONSUMPTION = "073";
            public const string STOCK_INITIALIZATION = "078";
            public const string STOCK_ISSUE_IN = "079";
            public const string STOCK_ISSUE_OUT = "082";
            public const string STOCK_TAKING = "085";
            public const string PRODUCTION = "086";
            public const string PRESCRIPTION_SERVICE = "091";
            public const string DISCHARGE_PRESCRIPTION = "092";
            public const string PRESCRIPTION_RETURN = "094";
            public const string DIRECT_PURCHASE = "095";
            public const string REFERRAL_ORDER = "098";
            public const string PRB_INFO = "101";
            public const string INVOICE = "106";
            public const string REIMBURSEMENT = "111";
            public const string ASSET_MAINTENANCE_ORDER = "116";
            public const string ASSET_MAINTENANCE_REALIZATION = "121";
            public const string PACKAGE_JOB_ORDER = "131";
            public const string ASSET_MOVEMENT = "126";



            public const string RAWAT_INAP = "RI";
            public const string RAWAT_JALAN = "RJ";
            public const string PASIEN = "PT";
            public const string TRANSAKSI_RAWAT_JALAN = "004";
        }

        public static class SettingParameter
        {
            public const string DEFAULT_EMAIL_ADDRESS = "PAR0001";
            public const string DEFAULT_EMAIL_SMTP = "PAR0002";
            public const string DEFAULT_EMAIL_PORT = "PAR0003";
            public const string REGISTRATION_TYPE_BY_EMAIL = "PAR0004";
            public const string INFORMATION_SOURCE_BY_EMAIL = "PAR0005";
            public const string DEFAULT_PASSWORD = "PAR0006";
        }

        public static class ReportCode
        {
            public const string COMPANY = "R000001";
            public const string TRAINER = "R000002";
            public const string TRAINING = "R000003";
            public const string LOB_CLASSIFICATION = "R000004";
            public const string VENUE = "R000005";
            public const string REGION = "R000006";
            public const string EVENT = "R000007";
            public const string MEMBER = "R000008";
        }

        public static class MenuCode
        {
            public const string COMPANY = "PQ1101";
            public const string MEMBER = "PQ1102";
            public const string REGION = "PQ1103";

            public const string TRAINING = "PQ1201";
            public const string TRAINER = "PQ1202";
            public const string VENUE = "PQ1203";
            public const string EMPLOYEE = "PQ1204";

            public const string ZIPCODES = "PQ1901";
            public const string OCCUPATION = "PQ1902";
            public const string CANNED_TEXT = "PQ1903";
            public const string LOB_CLASSIFICATION = "PQ1904";

            public const string CREATE_EVENT = "PQ2100";
            public const string GENERATE_EVENT_INVITATION = "PQ2200";
            public const string EVENT_INVITATION_CONFIRMATION = "PQ2300";
            public const string EVENT_REGISTRATION = "PQ2400";
            public const string UPDATE_REGISTRATION_STATUS = "PQ2500";
            public const string CLOSE_EVENT = "PQ2600";
            public const string EVENT_HISTORY = "PQ2700";
            public const string EVENT_CONFIRMATION_SEND_MAIL = "PQ2800";

            public const string LEADS_REGISTRATION = "PQ3100";
            public const string LEADS_TRACKING_LOG_ENTRY = "PQ3200";
            public const string CHANGE_LEAD_STATUS = "PQ3300";

            public const string INQUIRY_REGISTRATION = "PQ4100";
            public const string INQUIRY_TRACKING_LOG_ENTRY = "PQ4200";

            public const string APPLICATION = "PQ7101";
            public const string MODULE = "PQ7102";
            public const string MENU = "PQ7103";
            public const string STANDARD_CODE = "PQ7104";
            public const string SETTING_PARAMETER = "PQ7105";
            public const string FILTER_PARAMETER = "PQ7106";
            public const string REPORT_CONFIGURATION = "PQ7107";

            public const string USER_ROLES = "PQ7201";
            public const string USER_ACCOUNTS = "PQ7202";

            public const string DATA_MIGRATION_CONFIGURATION = "PQ7301";
            public const string DATA_MIGRATION = "PQ7302";

            public const string RESTORE_DATA_CONFIGURATION = "PQ7401";
            public const string RESTORE_DATA = "PQ7400";

            public const string TRANSFER_CONTACT_INFORMATION_TO_OUTLOOK_CONTACTS = "PQ8100";
            public const string UPLOAD_EVENT_PARTICIPANT_FROM_EXCEL = "PQ8200";
            public const string MAILING_LABEL_LIST = "PQ8300";

            public const string REPORT_MEMBER_LIST = "PQ9100";
            public const string PIVOT_REPORTING = "PQ9900";
        }

        public static class ConstantDate
        {
            public const string DEFAULT_NULL = "01-01-1900";
        }

        public static class LeadProcessType 
        {
            public const string PUBLIC_EVENT = "X154^001";
            public const string PROJECT = "X154^002";
            public const string OTHER = "X154^993";
        }

        public static class LeadStatus
        {
            public const string OPENED = "X150^001";
            public const string CLOSED = "X150^002";
            public const string DELETED = "X150^003";
        }

        public static class LeadSourceType 
        {
            public static string OTHER = "X151^999";
        }

        #region Standard Code
        public static class StandardCode
        {
            public const string NATIONALITY = "0212";
            public const string PROVINCE = "0347";
            public const string TRAINING_TYPE = "X001";
            public const string COMPANY_DEPARTMENT = "X003";
            public const string OCCUPATION_LEVEL = "X004";
            public const string SALUTATION = "X006";
            public const string TITLE = "X007";
            public const string SUFFIX = "X008";
            public const string MEMBER_STATUS = "X009";
            public const string REGISTRATION_STATUS = "X010";
            public const string COMPANY_TYPE = "X011";
            public const string COUNTY_OF_ORIGIN = "X012";
            public const string COMPANY_CERTIFICATION = "X013";
            public const string HOLIDAY_GREETINGS = "X014";
            public const string REGISTRATION_TYPE = "X015";
            public const string INFORMATION_SOURCE = "X016";
            public const string TEMPLATE_TEXT_GROUP = "X032";
            public const string VALUE_TYPE = "X103";
            public const string FILTER_PARAMETER_TYPE = "X108";
            public const string REPORT_TYPE = "X140";
            public const string DATA_SOURCE_TYPE = "X141";
            public const string LEAD_SOURCE_TYPE = "X151";
            public const string ACTIVITY_TYPE = "X153";
            public const string LEAD_PROCESS_TYPE = "X154";
        }

        public static class RegistrationStatus
        {
            public const string OPEN = "X010^001";
            public const string WAITING_FOR_CONFIRMATION = "X010^002";
            public const string CONFIRMED = "X010^003";
            public const string CHECKED_IN = "X010^004";
            public const string NO_SHOW = "X010^005";
            public const string CANCELLED = "X010^006";
            public const string VOID = "X010^007";
        }

        public static class FilterParameterType
        {
            public const string COMBO_BOX = "X108^001";
            public const string CHECK_LIST = "X108^002";
            public const string DATE = "X108^003";
            public const string PAST_PERIOD = "X108^004";
            public const string UPCOMING_PERIOD = "X108^005";
            public const string FREE_TEXT = "X108^006";
        }

        public static class ControlType
        {
            public const string TEXT_BOX = "X103^001";
            public const string COMBO_BOX = "X103^002";
            public const string RADIO_BUTTON = "X103^003";
            public const string CHECK_BOX = "X103^004";
        }

        public static class DataSourceType
        {
            public const string VIEW = "X141^001";
            public const string STORED_PROCEDURE = "X141^002";
        }

        public static class EventStatus
        {
            public const string OPENED = "X150^001";
            public const string CLOSED = "X150^002";
            public const string DELETED = "X150^003";
        }
        #endregion

    }
}
