using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAMS.Libs
{
    public static class Const
    {
        public const string VNPTHRMAPI_CHITIETHSNS = "/api/hrm/tuyendung/chitiethsns";
        public const string VNPTHRMAPI_PHONGBAN = "/api/hrm/tuyendung/phongban";
        public const string VNPTHRMAPI_HSNS = "/api/hrm/tuyendung/hsns";
        public const string VNPTHRMAPI_SYNCPROFILE = "/api/hrm/sync_employee_after_recruitment";
        public const string _DATE_FORMAT_ddMMyyyy = "ddMMyyyy";
        public const string DepartmentGroup_Tapdoan = "1.TAPDOAN";
        public const string DepartmentGroup_TongCongTy = "2.TONGCONGTY/CONGTY";
        public const string DepartmentGroup_VienThongTinh = "3.VIENTHONGTINH";
        public const string DepartmentGroup_TTKD = "4.TTKD";
        public const string LangVI = "vi";
        public const string LangEN = "en";
        public const string _DOT_TXT = ".txt";
        public const string _DATETIME_FORMAT = "dd/MM/yyyy HH:mm:ss";
        public const int PAGESIZE = 15;
        public const string ERROR_FOREINKEY = "Không xóa được do Đối tượng đang được sử dụng!";
        public static string log_dir = Helper.GetAppSettingByKey("log_dir");

        public const string MemberCouncilType_UyVien = "UV";
        public const string MemberCouncilType_ThuKy = "TK";
        public const string MemberCouncilType_ChuTich = "CT";

        public const string LinkGetFile = "/getfile"; 

        public class JobStatusItem
        {
            public JobStatusItem(int id, string name)
            {
                this.Id = id;
                this.Name = name;
            }
            public int Id { get; set; }
            public string Name { get; set; }
        };

        public static List<JobStatusItem> ListJobStatus = new List<JobStatusItem>()
        {
            new JobStatusItem(JobStatus.Draff.ToInt(0), "Nháp"),
            new JobStatusItem(JobStatus.Publish.ToInt(0),  "Đăng tin"),
            new JobStatusItem(JobStatus.Closed.ToInt(0),  "Dừng đăng tin"),
            new JobStatusItem(JobStatus.Waiting.ToInt(0),  "Chờ duyệt"),
            //new JobStatusItem(JobStatus.Hot.ToInt(0),  "Tin Hot"),
        };

        public static string GetJobStatusName(int status)
        {
            string statusName = string.Empty;
            //JobStatusItem jobStatusItem = ListJobStatus.Where(x => x.Id == status).FirstOrDefault();
            //if (jobStatusItem != null)
            //{
            //    statusName = jobStatusItem.Name;
            //}
            

            if ((status & JobStatus.Closed.ToInt(0)) == JobStatus.Closed.ToInt(0))
            {
                statusName += "<span class=\"label label-primary Closed\">Dừng đăng tin</span>";
                return statusName;
            }
            if ((status & JobStatus.Publish.ToInt(0)) == JobStatus.Publish.ToInt(0))
            {
                statusName += "<span class=\"label label-success Publish\">Đăng tin</span>";
            }
            if ((status & JobStatus.Draff.ToInt(0)) == JobStatus.Draff.ToInt(0))
            {
                statusName += "<span class=\"label label-danger Draff\">Nháp</span>";
            }
            if ((status & JobStatus.Waiting.ToInt(0)) == JobStatus.Waiting.ToInt(0))
            {
                statusName += "<span class=\"label label-warning Waiting\">Chờ duyệt</span>";
                return statusName;
            }
            //if ((status & JobStatus.Hot.ToInt(0)) == JobStatus.Hot.ToInt(0))
            //{
            //    statusName += "<span class=\"label label-danger Hot\">Hot</span>";
            //}
            return statusName;
        }


        public class SalaryTypeItem
        {
            public SalaryTypeItem(int id, string name)
            {
                this.Id = id;
                this.Name = name;
            }
            public int Id { get; set; }
            public string Name { get; set; }
        };

        public static List<SalaryTypeItem> ListSalaryType = new List<SalaryTypeItem>()
        {
            new SalaryTypeItem(SalaryType.Month.ToInt(0), "Tháng"),
            new SalaryTypeItem(SalaryType.Year.ToInt(0),  "Năm")
        };

        public static string GetSalaryTypeName(int id)
        {
            string salaryTypeName = string.Empty;
            SalaryTypeItem salaryTypeItem = ListSalaryType.Where(x => x.Id == id).FirstOrDefault();
            if (salaryTypeItem != null)
            {
                salaryTypeName = salaryTypeItem.Name;
            }
            return salaryTypeName;
        }

        public class SalaryUnitItem
        {
            public SalaryUnitItem(int id, string name)
            {
                this.Id = id;
                this.Name = name;
            }
            public int Id { get; set; }
            public string Name { get; set; }
        };

        public static List<SalaryUnitItem> ListSalaryUnit = new List<SalaryUnitItem>()
        {
            new SalaryUnitItem(SalaryUnit.VND.ToInt(0), "Triệu VNĐ"),
            new SalaryUnitItem(SalaryUnit.USD.ToInt(0),  "USD")
        };

        public static string GetSalaryUnitName(int id)
        {
            string salaryUnitName = string.Empty;
            SalaryUnitItem salaryItem = ListSalaryUnit.Where(x => x.Id == id).FirstOrDefault();
            if (salaryItem != null)
            {
                salaryUnitName = salaryItem.Name;
            }
            return salaryUnitName;
        }

        public static string GetEventStatusName(int status)
        {
            string statusName = string.Empty;
            switch (status)
            {
                case (int)EventStatus.Draff:
                    statusName =  "<span class=\"label label-primary\">Nháp</span>";
                    break;
                case (int)EventStatus.Publish:
                    statusName = "<span class=\"label label-success\">Hoạt động</span>";
                    break;
                default:
                    break;
            }
            return statusName;
        }

        public static string GetPheDuyetStatusName(int status)
        {
            string statusName = string.Empty;
            switch (status)
            {
                case (int)PheDuyetStatus.Nhap:
                    statusName = "<span class=\"label label-default\">Nháp</span>";
                    break;
                case (int)PheDuyetStatus.ChoPheDuyet:
                    statusName = "<span class=\"label label-info\">Chờ phê duyệt</span>";
                    break;
                case (int)PheDuyetStatus.DaPhDuyet:
                    statusName = "<span class=\"label label-success\">Đã phê duyệt</span>";
                    break;
                case (int)PheDuyetStatus.TuChoi:
                    statusName = "<span class=\"label label-danger\">Từ chối</span>";
                    break;
                default:
                    break;
            }
            return statusName;
        }
    }
    
}
