using System.ComponentModel;

namespace WebHook.Data.Common.Enum
{
    public enum DeliveryStatus
    {
        [Description("Hủy đơn hàng")]
        HuyDonHang = -1, //Hủy đơn hàng

        [Description("Chưa tiếp nhận")]
        ChuaTiepNhan = 1, //Chưa tiếp nhận

        [Description("Đã tiếp nhận")]
        DaTiepNhan = 2, //Đã tiếp nhận

        [Description("Đã lấy hàng/Đã nhập kho")]
        DaLayHang = 3, //Đã lấy hàng/Đã nhập kho

        [Description("Đã điều phối giao hàng/Đang giao hàng")]
        DangGiaoHang = 4, //Đã điều phối giao hàng/Đang giao hàng

        [Description("Đã giao hàng/Chưa đối soát")]
        DaGiaoHang = 5, //Đã giao hàng/Chưa đối soát

        [Description("Đã đối soát")]
        DaDoiSoat = 6, //Đã đối soát

        [Description("Không lấy được hàng")]
        KhongLayDuocHang = 7, //Không lấy được hàng

        [Description("Hoãn lấy hàng")]
        HoanLayHang = 8, //Hoãn lấy hàng

        [Description("Không giao được hàng")]
        KhongGiaoDuocHang = 9, //Không giao được hàng

        [Description("Delay giao hàng")]
        DelayGiaoHang = 10, //Delay giao hàng

        [Description("Đã đối soát công nợ trả hàng")]
        DaDoiSoatCongNoTraHang = 11, //Đã đối soát công nợ trả hàng

        [Description("Đã điều phối lấy hàng/Đang lấy hàng")]
        DaDieuPhoiLayHang = 12, //Đã điều phối lấy hàng/Đang lấy hàng

        [Description("Đơn hàng bồi hoàn")]
        DonHangBoiHoan = 13, //Đơn hàng bồi hoàn

        [Description("Đang trả hàng (COD cầm hàng đi trả)")]
        DangTraHang = 20, //Đang trả hàng (COD cầm hàng đi trả)

        [Description("Đã trả hàng (COD đã trả xong hàng)")]
        DaTraHang = 21, //Đã trả hàng (COD đã trả xong hàng)

        [Description("Shipper báo đã lấy hàng")]
        ShipperDaLayHang = 123, //Shipper báo đã lấy hàng

        [Description("Shipper (nhân viên lấy/giao hàng) báo không lấy được hàng")]
        ShipperKhongLayDuocHang = 127, //Shipper (nhân viên lấy/giao hàng) báo không lấy được hàng

        [Description("Shipper báo delay lấy hàng")]
        ShipperDelayLayHang = 128, //Shipper báo delay lấy hàng

        [Description("Shipper báo đã giao hàng")]
        ShipperDaGiaoHang = 45, //Shipper báo đã giao hàng

        [Description("Shipper báo không giao được giao hàng")]
        ShipperKhongGiaoDuocHang = 49, //Shipper báo không giao được giao hàng

        [Description("Shipper báo delay giao hàng")]
        ShipperDelayGiaoHang = 410, //Shipper báo delay giao hàng
    }
}
