using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demining
{
    public partial class BombButton : UserControl
    {
        public BombButton()
        {
            InitializeComponent();
        }

        private void BombButton_Load(object sender, EventArgs e)
        {
            gdi = CreateGraphics();
        }
        Graphics gdi;
        public bool m_IsBoom = false;//是否是雷
        public int m_nBoomAmount = 0;//周围雷的个数
        //public int 
        private void BombButton_Paint(object sender, PaintEventArgs e)
        {
            DrawBtn();
        }
        public int m_nState=1;
        public void SetState(int state)
        {
            m_nState = state;
            DrawBtn();
        }
        private void DrawBtn()
        {
            switch (m_nState)
            {
                case 1://默认的状态
                    {
                        DrawDefault();
                        break;
                    }
                case 2://空白的状态
                    {
                        DrawNull();
                        break;
                    }
                case 3://翻开的状态
                    {
                        DrawNumber();
                        break;
                    }
                case 4://旗子标记
                    {
                        DrawMark();
                        break;
                    }
            }
            
        }
        private void DrawDown()
        {
            Bitmap bmpTmp = new Bitmap(Size.Width, Size.Height);
            Graphics gdiCache = Graphics.FromImage(bmpTmp);
            gdiCache.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), 1, 1, Size.Width, Size.Height);
            gdiCache.DrawRectangle(new Pen(Color.FromArgb(0xcc, 0xcc, 0xcc), 1), 0, 0, Size.Width - 1, Size.Height - 1);
            gdi.DrawImage(bmpTmp, 0, 0);
        }
        private void DrawNumber()
        {
            Bitmap bmpTmp = new Bitmap(Size.Width, Size.Height);
            Graphics gdiCache = Graphics.FromImage(bmpTmp);
            gdiCache.FillRectangle(new SolidBrush(Color.FromArgb(0xee, 0xe5, 0xdf)), 0, 0, Size.Width, Size.Height);
            gdiCache.DrawRectangle(new Pen(Color.FromArgb(0xcc, 0xcc, 0xcc), 1), 0, 0, Size.Width - 1, Size.Height - 1);
            if (m_IsBoom)
            {
                gdiCache.DrawString("*", new Font("Arial", 18), new SolidBrush(Color.Red), 3, 3);
            }
            else
            {
                if(m_nBoomAmount!=0)
                    gdiCache.DrawString(m_nBoomAmount.ToString(), new Font("Arial", 12), new SolidBrush(Color.Black), 3, 3);
            }
            gdi.DrawImage(bmpTmp, 0, 0);
        }
        private void DrawDefault()
        {
            Bitmap bmpTmp = new Bitmap(Size.Width, Size.Height);
            Graphics gdiCache = Graphics.FromImage(bmpTmp);
            gdiCache.FillRectangle(new SolidBrush(Color.FromArgb(0xd9, 0xd9, 0xd9)), 0, 0, Size.Width, Size.Height);
            gdiCache.DrawRectangle(new Pen(Color.FromArgb(0xcc, 0xcc, 0xcc), 1), 0, 0, Size.Width - 1, Size.Height - 1);
            gdi.DrawImage(bmpTmp, 0, 0);
        }
        private void DrawNull()
        {
            Bitmap bmpTmp = new Bitmap(Size.Width, Size.Height);
            Graphics gdiCache = Graphics.FromImage(bmpTmp);
            gdiCache.FillRectangle(new SolidBrush(Color.FromArgb(255, 255, 255)), 0, 0, Size.Width, Size.Height);
            gdiCache.DrawRectangle(new Pen(Color.FromArgb(0xcc, 0xcc, 0xcc), 1), 0, 0, Size.Width - 1, Size.Height - 1);
            gdi.DrawImage(bmpTmp, 0, 0);
        }
        private void DrawMark()
        {
            Bitmap bmpTmp = new Bitmap(Size.Width, Size.Height);
            Graphics gdiCache = Graphics.FromImage(bmpTmp);
            gdiCache.FillRectangle(new SolidBrush(Color.FromArgb(0xd9, 0xd9, 0xd9)), 0, 0, Size.Width, Size.Height);
            gdiCache.DrawRectangle(new Pen(Color.FromArgb(0xcc, 0xcc, 0xcc), 1), 0, 0, Size.Width - 1, Size.Height - 1);
            gdiCache.DrawString("★", new Font("Arial", 12), new SolidBrush(Color.Red), 0, 3);
            gdi.DrawImage(bmpTmp, 0, 0);
        }
        struct pos
        {
            public int x;
            public int y;
        }
        public Point m_Pos=new Point();
       
    }
}
