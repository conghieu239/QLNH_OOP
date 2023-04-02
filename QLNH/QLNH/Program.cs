using System;
using System.ComponentModel.Design;
using System.Net.Security;

namespace QLNH
{
    public class QLNH
    {
        public long DoanhThu { get; set; }
        public int SLNhanVien { get; set; }
        public Dictionary<string, double> KH { get; set; }

        public double ThuongPhanTram = 0.05;

    } 
    //Quan ly nhan vien
    public class NhanVien: QLNH
    {

        public string Name { get; set; }
        public string ID { get; set; }
        public string Phone { get; set; }
        public int WorkingHour { get; set; }
        public NhanVien() { }

        public NhanVien(string Name, string ID, string Phone, int WorkingHour)
        {
            this.Name = Name;
            this.ID = ID;
            this.Phone = Phone;
            this.WorkingHour = WorkingHour;
        }

        public virtual double TinhLuong()
        {
            return 0;
        }

        public virtual double TinhThuong()
        {
            return 0;
        }

    }

    delegate double Tinh(long b, double c, int d);

    class NhanVienPartTime : NhanVien
    {
        public NhanVienPartTime() { }

        static double TinhTien(long b, double c, int d)
        {
            double sum = (b / d) * c;
            return sum;
        }

        private int Luong = 20000;
        public override double TinhLuong()
        {
            return Luong * WorkingHour;
        }

        public override double TinhThuong()
        {
            Tinh a = new Tinh(TinhTien);
            double Thuong = a(DoanhThu, ThuongPhanTram, SLNhanVien);


            return Thuong;
        }
    }
    class NhanVienFullTime : NhanVien
    {
        public NhanVienFullTime() { }
        static double TinhTien(long b, double c, int d)
        {
            double sum = (b / d) * c;
            return sum;
        }

        private int Luong = 35000;
        public override double TinhLuong()
        {
            return Luong * WorkingHour;
        }

        public override double TinhThuong()
        {
            Tinh a = new Tinh(TinhTien);
            double Thuong = a(DoanhThu, ThuongPhanTram, SLNhanVien);

            return Thuong;
        }
    }
    delegate double TinhQL(int a ,long b, double c, int d);
    class QuanLy : NhanVien
    {
        public QuanLy() { }
        static double TinhTien(int a, long b, double c, int d)
        {
            double sum = a + (b / d) * c;
            return sum;
        }

        private int Luong = 70000;
        private int ThuongCung = 1000000;

        public override double TinhLuong()
        {
            return Luong * WorkingHour;
        }

        public override double TinhThuong()
        {
            TinhQL a = new TinhQL(TinhTien);
            double Thuong = a(ThuongCung, DoanhThu, ThuongPhanTram, SLNhanVien);

            return Thuong;
        }
    }
    //Quan ly nhap kho
    abstract class QLNK<T>
    {
        public List<long> GiaNL;

        public List<T> SL;

        public List<string> TenNL;
        public abstract T ChiPhi();

    }
    //Quanlynhabep
    class QLNB<T> : QLNK<T>
    {
        public override T ChiPhi()
        {
            T sum = (dynamic) 0;
            for (int i = 0; i < GiaNL.Count; i++)
            {
                sum += (dynamic)GiaNL[i];
            }
            return sum;
        }
    }
    //Quanlyquaybar
    class QLQB<T> : QLNK<T>
    {
        public override T ChiPhi()
        {
            dynamic sum = 0;
            for (int i = 0; i < GiaNL.Count; i++)
            {
                sum += (dynamic)GiaNL[i] * SL[i];
            }
            return sum;
        }
    }
    //Quan ly khach hang
    class KhachHang: QLNH
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int NamSinh { get; set; }    
        public int SoLanAn { get; set; }
        public int SoTienDaTra { get; set; }

        public KhachHang() { }

        public KhachHang(string Name, int ID, int SoLanAn, int SoTienDaTra, int NamSinh) 
        {
            this.Name = Name;
            this.ID = ID;   
            this.SoLanAn = SoLanAn; 
            this.SoTienDaTra = SoTienDaTra;
            this.NamSinh = NamSinh;
        }
        public virtual void GiamGia() { }
    }
    class KhachHangThuong: KhachHang
    {
        private double MaGiamGia = 0.05;
        public KhachHangThuong() { }

        public override void GiamGia()
        {
            KH[Name] = MaGiamGia;
        }
    }
    class KhachHangVip : KhachHang
    {
        private double MaGiamGia = 0.08;
        public KhachHangVip() { }

        public override void GiamGia()
        {
            KH[Name] = MaGiamGia;
        }
    }
    class QuanLyOrder : KhachHang
    {
        public int SLkhach { get; set; }
        public int SLchongoi { get; set; }
        public QuanLyOrder() { }
        public List<string> MonAn { get; set; }
        public QuanLyOrder(List<string> MonAn)
        {
            this.SLkhach = SLkhach;
            this.SLchongoi = SLchongoi;
            for (int i = 0; i < MonAn.Count; i++)
            {
                this.MonAn[i] = MonAn[i];
            }
        }
        public void CheckBan()
        {
            try
            {
                int[] chongoi = new int[SLchongoi];
                for (int i = 1; i <= SLkhach; i++)
                    chongoi[i] = i;
            } catch (Exception e)
            {
                Console.WriteLine("So luong cho ngoi khong du");
            }
        }
        public void CheckOrder()
        {
            int nam = int.Parse(DateTime.Now.Year.ToString());
            bool kt = true;
            foreach (string i in MonAn)
            {
                if (i == "Ruou" || i == "Bia") kt = false;
            }

            if (nam - NamSinh < 18 && kt == false)
            {
                Console.WriteLine("Ban chua du 18 tuoi de uong bia hoac ruou");
            }
            else
            {
                Console.WriteLine("Ban da du tuoi de uong bia hoac ruou");
            }
        }
    }
    public interface IPhuPhi
    {
        long GetTienMatBang();

        long GetTienDienNuoc();

        long GetTienMarketing();
    }
    public class PhuPhi : IPhuPhi
    {
        private long tienMatBang;

        public long GetTienMatBang()
        {
            return tienMatBang;
        }

        public void SetTienMatBang(long value)
        {
            tienMatBang = value;
        }

        private long tienDienNuoc;

        public long GetTienDienNuoc()
        {
            return tienDienNuoc;
        }

        public void SetTienDienNuoc(long value)
        {
            tienDienNuoc = value;
        }

        private long tienMarketing;

        public long GetTienMarketing()
        {
            return tienMarketing;
        }

        public void SetTienMarketing(long value)
        {
            tienMarketing = value;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            NhanVienFullTime nhanvien = new NhanVienFullTime();
            nhanvien.DoanhThu = 500000000;
            nhanvien.SLNhanVien = 35;
            nhanvien.Name = "Nguyen Van A";
            nhanvien.ID = "001";
            nhanvien.Phone = "0123456789";
            nhanvien.WorkingHour = 200;
            double Thuong = Math.Round(nhanvien.TinhThuong(), 0); 
            Console.WriteLine("Luong va thuong cua {0} la {1} va {2}", nhanvien.Name, nhanvien.TinhLuong(), Thuong);
            QLQB<long> qlqb = new QLQB<long> ();
            qlqb.GiaNL = new List<long>();
            qlqb.GiaNL.Add(20000);
            qlqb.GiaNL.Add(10000);
            qlqb.SL = new List<long>();
            qlqb.SL.Add(100);
            qlqb.SL.Add(200);
            qlqb.TenNL = new List<string> ();
            qlqb.TenNL.Add("Bia");
            qlqb.TenNL.Add("Nuoc Ngot");
            Console.WriteLine("Chi phi mua nguyen lieu cua quay bar la {0}", qlqb.ChiPhi());
            KhachHang kh = new KhachHang ();
            kh.KH = new Dictionary<string, double>();
            kh.SoLanAn = 17;
            kh.SoTienDaTra = 10000000;
            if (kh.SoLanAn > 10 && kh.SoTienDaTra > 5000000)
            {
                KhachHangVip khv = new KhachHangVip();         
                khv.Name = "Le Van A";
                khv.ID = 1;
                khv.NamSinh = 2000;
                khv.SoLanAn = kh.SoLanAn;
                khv.SoTienDaTra = kh.SoTienDaTra;
            }
            else if (kh.SoLanAn < 10 && kh.SoTienDaTra < 5000000)
            {
                KhachHangThuong kht = new KhachHangThuong();
                kht.Name = "Le Van A";
                kht.ID = 1;
                kht.NamSinh = 2000;
                kht.SoLanAn = kh.SoLanAn;
                kht.SoTienDaTra = kh.SoTienDaTra;
            }
            QuanLyOrder qlo = new QuanLyOrder();
            qlo.SLkhach = 5;
            qlo.SLchongoi = 4;
            qlo.MonAn = new List<string>();
            qlo.MonAn.Add("Thit heo");
            qlo.MonAn.Add("Bia");
            qlo.CheckBan();
            qlo.CheckOrder();
            PhuPhi phuphi = new PhuPhi();
            phuphi.SetTienMatBang(150000000);
            phuphi.SetTienMarketing(50000000);
            phuphi.SetTienDienNuoc(20000000);
            Console.ReadKey();
        }
    }
}
