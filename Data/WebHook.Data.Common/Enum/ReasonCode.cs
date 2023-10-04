using System.ComponentModel;

namespace WebHook.Data.Common.Enum
{
    public enum ReasonCode
    {
        #region Lý do chậm lấy hàng 

        [Description("Nhà cung cấp (NCC) hẹn lấy vào ca tiếp theo")]
        LyDoChamLayHang_NCCHenLayCaTiepTheo = 100, //	Nhà cung cấp (NCC) hẹn lấy vào ca tiếp theo

        [Description("GHTK không liên lạc được với NCC")]
        LyDoChamLayHang_KhongLienLacDuocNCC = 101, //	GHTK không liên lạc được với NCC

        [Description("NCC chưa có hàng")]
        LyDoChamLayHang_NCCChuaCoHang = 102, //	NCC chưa có hàng

        [Description("NCC đổi địa chỉ")]
        LyDoChamLayHang_NCCDoiDiaChi = 103, //	NCC đổi địa chỉ

        [Description("NCC hẹn ngày lấy hàng")]
        LyDoChamLayHang_NCCHenNgayLayHang = 104, //	NCC hẹn ngày lấy hàng

        [Description("GHTK quá tải, không lấy kịp")]
        LyDoChamLayHang_GHTKQuaTai = 105, //	GHTK quá tải, không lấy kịp

        [Description("Do điều kiện thời tiết, khách quan")]
        LyDoChamLayHang_DoDieuKienThoiTiet = 106, //	Do điều kiện thời tiết, khách quan

        [Description("Lý do khác")]
        LyDoChamLayHang_LyDoKhac = 107, //	Lý do khác
        #endregion

        #region Lý do không lấy được hàng

        [Description("Địa chỉ ngoài vùng phục vụ")]

        LyDoKhongLayDuocHang_DiaChiNgoaiVungPhuSong = 110, //	Địa chỉ ngoài vùng phục vụ

        [Description("Hàng không nhận vận chuyển")]
        LyDoKhongLayDuocHang_HangKhongNhanVanChuyen = 111, //	Hàng không nhận vận chuyển

        [Description("NCC báo hủy")]
        LyDoKhongLayDuocHang_NCCBaoHuy = 112, //	NCC báo hủy

        [Description("NCC hoãn/không liên lạc được 3 lần")]
        LyDoKhongLayDuocHang_KhongLienLacDuocNCC = 113, //	NCC hoãn/không liên lạc được 3 lần

        [Description("Lý do khác")]
        LyDoKhongLayDuocHang_LyDoKhac = 114, //	Lý do khác

        [Description("Đối tác hủy đơn qua API")]
        LyDoKhongLayDuocHang_DoiTacHuyDonQuaAPI = 115, //	Đối tác hủy đơn qua API

        #endregion

        #region Lý do chậm giao hàng
        [Description("GHTK quá tải, giao không kịp")]
        LyDoChamGiaoHang_GHTKQuaTai = 120, //	GHTK quá tải, giao không kịp

        [Description("Người nhận hàng hẹn giao ca tiếp theo")]
        LyDoChamGiaoHang_NguoiNhanHenGiaoCaTiep = 121, //	Người nhận hàng hẹn giao ca tiếp theo

        [Description("Không gọi được cho người nhận hàng")]
        LyDoChamGiaoHang_KhongGoiDuocChoNguoiNhanHang = 122, //	Không gọi được cho người nhận hàng

        [Description("Người nhận hàng hẹn ngày giao")]
        LyDoChamGiaoHang_NguoiNhanHangHenNgayGiao = 123, //	Người nhận hàng hẹn ngày giao

        [Description("Người nhận hàng chuyển địa chỉ nhận mới")]
        LyDoChamGiaoHang_NguoiNhanHangChuyenDiaChiNhanMoi = 124, //	Người nhận hàng chuyển địa chỉ nhận mới

        [Description("Địa chỉ người nhận sai, cần NCC check lại")]
        LyDoChamGiaoHang_DiaChiNguoiNhanSai = 125, //	Địa chỉ người nhận sai, cần NCC check lại

        [Description("Do điều kiện thời tiết, khách quan")]
        LyDoChamGiaoHang_DoDieuKienThoiTiet = 126, //	Do điều kiện thời tiết, khách quan

        [Description("Lý do khác")]
        LyDoChamGiaoHang_LyDoKhac = 127, //	Lý do khác

        [Description("Đối tác hẹn thời gian giao hàng")]
        LyDoChamGiaoHang_DoiTacHenThoiGianGiaoHang = 128, //	Đối tác hẹn thời gian giao hàng

        [Description("Không tìm thấy hàng")]
        LyDoChamGiaoHang_KhongTimThayHang = 129, //	Không tìm thấy hàng

        [Description("SĐT người nhận sai, cần NCC check lại")]
        LyDoChamGiaoHang_SaiSoDienThoaiNguoiNhan = 1200, //	SĐT người nhận sai, cần NCC check lại
        #endregion

        #region Lý do không giao được hàng

        [Description("Người nhận không đồng ý nhận sản phẩm")]
        KhongGiaoDuocHang_NguoiNhanKhongNhan = 130, //	Người nhận không đồng ý nhận sản phẩm

        [Description("Không liên lạc được với KH 3 lần")]
        KhongGiaoDuocHang_KhongLienLacDuocVoiKhachHang = 131, //	Không liên lạc được với KH 3 lần

        [Description("Không liên lạc được với KH 3 lần")]
        KhongGiaoDuocHang_KhachHangHenGiaoLaiQuaBaLan = 132, //	KH hẹn giao lại quá 3 lần

        [Description("Shop báo hủy đơn hàng")]
        KhongGiaoDuocHang_ShopBaoHuyDon = 133, //	Shop báo hủy đơn hàng

        [Description("Lý do khác")]
        KhongGiaoDuocHang_LyDoKhac = 134, //	Lý do khác

        [Description("Đối tác hủy đơn qua API")]
        KhongGiaoDuocHang_DoiTacHuyDonQuaAPI = 135, //	Đối tác hủy đơn qua API

        [Description("NCC hẹn trả ca sau")]
        DelayTraHang_NCCHenTraCaSau = 140, //	NCC hẹn trả ca sau

        [Description("Không liên lạc được với NCC")]
        DelayTraHang_KhongLienLacDuocNCC = 141, //	Không liên lạc được với NCC

        [Description("NCC không có nhà")]
        DelayTraHang_NCCKhongCoNha = 142, //	NCC không có nhà

        [Description("NCC hẹn ngày trả")]
        DelayTraHang_NCCHenNgayTra = 143, //	NCC hẹn ngày trả

        [Description("Lý do khác")]
        DelayTraHang_LyDoKhac = 144, //	Lý do khác
        #endregion
    }
}
