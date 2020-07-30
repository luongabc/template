namespace TAMS.Libs
{

    // Trạng thái của Hội đồng
    public enum CouncilStatus
    {
        RUN = 0,
        CLOSED = 1
    }

    // Trạng thái đánh giá kết quả ứng viên
    public enum ResultDetailState
    {
        KEM = 0, 
        TRUNGBINH = 1,
        TOT = 2,
        KHA = 3
    }

    // Trạng thái đ kết quả ứng viên
    public enum ResultState
    {
        DAT = 1,
        XEMXET = 2,
        LOAI = 0
    }

    // Trạng thái Candicate
    public enum CandicateState
    {
        ChuaThamGia = 0, // Chưa tham gia, chưa đến
        DaDiemDanh = 1, // Đã điểm danh
        DangPhongVan = 2, // Đang phỏng vấn
        DaPhongVan = 3 // Đã phỏng vấn
    }

    // Trạng thái đợt tuyển dụng
    public enum RecruitmentPlanStatus
    {
        Draff = 0,   // Nháp
        Plan = 1, // Kế hoạch
        Recruitment = 2, // Đang diễn ra
        Closed = 3 // Đóng
    }

    // Trạng thái Phiếu Import
    public enum ImportCVStatus
    {
        Draff = 0,
        Error = 1,
        Success = 2,
        ErrorTransaction = 3,
        NoRecord = 4,
    }

    // Trạng thái import từng dòng dữ liệu
    public enum ImportCVItemStatus
    {
        Success = 0,
        Error = 1,
        DuplicateIdentification = 2
    }

    // Trạng thái các bước Ứng tuyển trong Apply
    public enum ApplyStatus
    {
        Submit = 1,
        Filter= 2,
        Accept = 21,
        Exam = 3,
        Interview = 4,
        Appraise = 41,
        Offered = 5,
        Hired = 6,
        Rejected = 7,
        
    }
    /// <summary>
    /// Trạng thái đẩy từ FrontEnd, Import của Apply
    /// </summary>
    public enum ApplyState
    {
        New = 0, // Chờ duyệt
        Seen = 1, // Đã xem
        Rejected = 2, // Loại
        Approved = 3 // Duyệt
    }

    // Trạng thái Introduction: Quảng bá job
    public enum IntroductionStatus
    {
        Draff = 0,
        UnSent = 1,
        Sent = 2,
        Error = 3,
    }

    // Loại ứng viên tiềm năng, ứng viên Danh sách đen
    public enum ProfileType
    {
        Normal = 0,
        Blacklist = 1,
        Potential = 2
    }

    // Ứng viên nội bộ, ứng viên bên ngoài
    public enum ProfileCategory
    {
        Exterior = 0, 
        Internal = 1
    }

    // Trạng thái hôn nhân
    public enum MaritalType
    {
        Single = 0,
        Married = 1
    }

    // Loại thao tác
    public enum TypeAction
    {
        Read = 1,
        Add = 2,
        Edit = 3,
        Delete = 4
    }

    // Đơn vị tiền tệ
    public enum SalaryUnit
    {
        VND = 1,
        USD = 2
    }

    // Loại Tính lương theo tháng, năm
    public enum SalaryType
    {
        Month = 1,
        Year = 2
    }

    // Loại Tin tuyển dụng nội bộ, thị trường
    public enum JobType
    {
        Public = 0, // Thị trường
        Private = 1, // Nội bộ
        Scholarship = 2
    }

    public enum ScholarshipType
    {
        Student = 0, // Sinh viên đăng ký
        School = 1, // Nhà trường đăng  ký
        Both = 2 // Cả hai
    }

    // Trạng thái của Tin tuyển dụng
    public enum JobStatus
    {
        Draff = 1,
        Publish = 2,
        Closed = 4,
        Hot = 8,
        Delete = 16,
        Waiting = 32
    }

    // Gender
    public enum Gender
    {
        Male = 0,
        FeMale = 1,
        Other = 2
    }

    //
    public enum SchoolType
    {
        In = 0, // Trong nước
        Out = 1 // Ngoài nước
    }

    public enum EventStatus
    {
        Draff = 0,
        Publish = 1
    }

    public enum UserStatus
    {
        Deactive = 0,
        Active = 1
    }

    public enum CategoryType
    {
        Project = 1,
        Product = 2,
        Articles = 3,
        WareHouses = 4
    }

    public enum ImageType
    {
        Project = 1,
        Product = 2,
        WareHouses = 3
    }

    public enum ProductType
    {
        Sell = 1,
        Rent = 2,
        SellHouse = 3
    }

    public enum UnitPrice
    {
        ThoaThuan = 1,
        Trieu = 2,
        Ty = 3,
        TramNghinM2 = 4,
        TrieuM2 = 5,
        TramNghinThang = 6,
        TrieuThang = 7,
        TramNghinM2Thang = 8,
        TrieuM2Thang = 9,
        NghinM2Thang = 10
    }

    //Mức độ câu hỏi
    public enum QuestionLevel
    {
        EASY = 1, //DỄ
        MEDIUM = 2, //VỪA
        HARD = 3, //KHÓ
        VERY_HARD = 4 //SIÊU KHÓ
    }

    //Loại câu hỏi
    public enum KindOfQuestion
    {
        YES_NO_QUESTION = 1, // CÂU HỎI ĐÚNG SAI
        SINGLE_CHOICE_QUESTION = 2, // CÂU HỎI 1 LỰA CHỌN
        MULTIPLE_CHOICE_QUESTION = 3, // CÂU HỎI NHIỀU LỰA CHỌN
        MATCHING_QUESTION = 4, //CÂU HỎI NỐI
        COMPONENT_POINT_QUESTION = 5, // CÂU HỎI ĐIỂM THÀNH PHẦN
        CONSTRUCTED_QUESTION = 6 // CÂU HỎI TỰ LUẬN
    }

    // Trạng thái bản ghi (thi online)
    public enum State
    {
        ACTIVE = 1,
        INACTIVE = 2
    }

    // Trạng thái gửi mail
    /// <summary>
    /// KienCT: Email được đẩy vào bảng EmailLog  ở trạng thái Chờ gửi. Sau đó có 1 service khác quét vào Bảng này để gửi Message vào RabbitMQ để gửi
    /// Lúc đó trạng thái sẽ chuyển sang Send - Đã gửi.
    /// </summary>
    public enum EmailStatus
    {
        Pendding = 0,       // Chưa gửi
        Success = 1,        // gửi thành công
        Error = 2,          // Gửi lỗi
        //Resending = 3,      // Gửi lại
        Send = 5,           // Đã vào hàng đợi gửi mail Đang gửi
        Cancel = 6          // Hủy 
    }

    // loại email
    public enum EmailType
    {
        DangKiTaiKhoan = 0,
        KhoiPhucMatKhau = 1, 
        Ungtuyen = 2,               // gửi người dùng khi ứng tuyển từ frontend
        QuangBaViecLam = 3,         // giới thiệu Tin tuyển dụng
        HoiDongPhongVan = 4,        // gửi thành viên hội đông phỏng vấn thông tin liên quan tới hội đồng
        MoiPhongVan = 5,            // mời ứng viên tới tham gia dự tuyển / phỏng vấn
        MoiNhanViec = 6,            // email offer 
        LichDiLam = 7,              // email nhắc lịch ứng viên ngày bắt đầu đi làm
        TiepNhanNhanSu = 8,         // email gửi tới chuyên viên tuyển dụng lịch tiếp nhận nhân sự mới
        ThuCamOn = 9,               // gửi ứng viên không pass
        ThongBaoHangNgay = 10,      // gửi mail hàng ngày thông tin tổng hợp cho chuyên viên tuyển dụng
        ViecLamPhuHop = 11,         // gửi mail cho người dùng đăng kí nhận e-mail giới thiệu việc làm phù hợp
        PhanCongSangLoc = 12,       // gửi mail phân công sàng lọc hồ sơ
    }

    // Trạng thái của cấu trúc, đề thi, người thi
    public enum StatusStructure
    {
        Draft = 1, // Trạng thái soạn thảo
        Confirm = 2, // Trạng thái xác nhận ( có thể thi )
        Close = 3 // Trạng thái đóng ( kết thúc đợt thi, đóng đợt thi, đề thi )
    }

    // Loại của Ứng tuyển
    public enum ApplyType
    {
        Apply = 1,
        Favorite = 2,
        CVSent = 3,
        HocBong = 4,
        Fresher = 5,
        ThucTapSinh = 6
    }

    /// <summary>
    /// Loại hội đồng tuyển dụng
    /// 2020-03-11 Dùng luôn cho Đợt tuyển dụng => Thực hiện tuyển dụng
    /// </summary>
    public enum RecruitmentCouncilType
    {
        Taptrung = 1,
        Phancap = 2,
        Kethop = 3
    }

    /// <summary>
    /// Loại nhóm quyền
    /// </summary>
    public enum RoleGroupType
    {
        QuanTri = 1,
        HoiDongTuyenDung = 2,
        HoiDongThiTuyen = 3, // Tạm thời bỏ
        HoiDongPhongVan = 4,
        ToGiupViec = 5
    }

    public enum PheDuyetStatus
    {
        Nhap = 0,
        ChoPheDuyet = 1,
        DaPhDuyet = 2,
        TuChoi = 3

    }

    // Trạng thái Kế hoạch năm
    public enum KeHoachNamStatus
    {
        Draff = 0,   // Nháp
        Plan = 1, // Kế hoạch
        Recruitment = 2, // Đang diễn ra
        Closed = 3 // Đóng
    }


    /// <summary>
    /// Các enum định nghĩa các bảng Lưu Log
    /// </summary>
    public enum CommonLogTableId
    {
        RecruitmentPlan = 1, // Đợt tuyển dụng
        Profile = 2, // Hồ sơ
        Apply = 3, // Ứng tuyển
        RP_PLAN_CANDICATE = 4, // Thông tin ứng viên trong đợt, hội đồng, trạng thái sàng lọc, tham gia
        NegotiateCandidate = 5, // Tiếp nhận ứng viên
        Job = 6, // Việc làm
    }

    /// <summary>
    /// Các enum định nghĩa các Action tác động
    /// </summary>
    public enum CommonLogTypeId
    {
        Create = 1,
        Update = 2,
        Delete = 3
    }

    /// <summary>
    /// Trạng thái update từ Apply sang Profile
    /// </summary>
    public enum ApplyIsCheck
    {
        ChuaUpdate = 0,
        DaUpdate = 1
    }
    
    /// <summary>
    /// Loại bảng tương ứng ObjectType trong MultiEmail và MultiPhone
    /// </summary>
    public enum MultiObjectType
    {
        Apply = 1,
        Profile = 2
    }

    public enum MessageConfigType
    {
        Email_SMS = 0,
        SMS = 1,
        Email = 2
    }
}
