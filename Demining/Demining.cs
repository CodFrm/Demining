using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Demining
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        struct LatticeMsg
        {
            public int x;
            public int y;
            public BombButton btn;
        }
        ArrayList m_arrLattice = new ArrayList();
        private int m_nWidth = 10, m_nHight = 10,m_nBomb=10;
        private int m_nRemain = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            m_nRemain = m_nWidth*m_nHight;
            CreateCloth(m_nWidth, m_nHight, m_nBomb);
            MessageBox.Show(m_nWidth.ToString()+"*"+m_nHight.ToString()+" "+m_nBomb.ToString()+"个雷");
        }
        private void CreateCloth(int width, int hight, int boom)//创建扫雷的界面 算法待优化
        {
            ArrayList arrBoomPos = new ArrayList();
            int i = 0;
            Random rand = new Random();//先分布好雷的坐标
            while (i++ < boom)
            {
                int nRand = rand.Next(width * hight);
                while (arrBoomPos.IndexOf(nRand) != -1)
                {
                    nRand = rand.Next(width * hight);
                }
                arrBoomPos.Add(nRand);
            }
            i = 0;
            for (; i < (width * hight); i++)//创建各个按钮并且放入雷
            {
                BombButton tmpBtn = new BombButton();
                tmpBtn.m_Pos.X = (i / hight);
                tmpBtn.m_Pos.Y = (i % hight);//对坐标计算
                tmpBtn.Size = new Size(25, 25);
                //System.Diagnostics.Debug.WriteLine(tmpBtn.m_Pos.X.ToString() + "," + tmpBtn.m_Pos.Y.ToString() + "," + i.ToString());
                tmpBtn.Left = tmpBtn.m_Pos.X * 24;
                tmpBtn.Top = tmpBtn.m_Pos.Y * 24 + 24;
                tmpBtn.MouseDown += BombButton_Down;
                tmpBtn.MouseUp += BombButton_Up;
                if (arrBoomPos.IndexOf(i) != -1)//判断这个坐标有没有雷
                {
                    tmpBtn.m_IsBoom = true;
                }
                else
                {
                    tmpBtn.m_IsBoom = false;
                }
                this.Controls.Add(tmpBtn);
                m_arrLattice.Add(tmpBtn);
            }
            for (i = 0; i < (width * hight); i++)
            {
                BombButton tmpBtn = (BombButton)m_arrLattice[i];
                if (tmpBtn.m_IsBoom)
                {
                    for (int x = tmpBtn.m_Pos.X - 1; x <= tmpBtn.m_Pos.X + 1; x++)
                    {
                        for (int y = tmpBtn.m_Pos.Y - 1; y <= tmpBtn.m_Pos.Y + 1; y++)
                        {
                            //System.Diagnostics.Debug.WriteLine(x.ToString() + "," + y.ToString() + "," + i.ToString());
                            if (x < 0 || y < 0) continue;
                            if (x > (width - 1) || y > (hight - 1)) continue;
                            int sub = ((x * hight) + y);
                            BombButton tmpBtnAmount = (BombButton)m_arrLattice[sub];
                            if (!(sub == i) && !(tmpBtnAmount.m_IsBoom))
                            {
                                tmpBtnAmount.m_nBoomAmount++;
                            }
                        }
                    }
                }
            }
        }
        protected bool m_IsLeftDown = false;
        protected bool m_IsRightDown = false;
        private void BombButton_Down(object sender, MouseEventArgs e)
        {
            if (m_isGameOver) return;
            /*if (m_IsLeftDown && e.Button == MouseButtons.Right)
            {
                //if (m_bombBtn.m_nState !=  4)
                    //m_bombBtn.SetState(1);
            }*/
            m_bombBtn = (BombButton)sender;
            System.Diagnostics.Debug.WriteLine(e.Button.ToString() + ",Down," + m_bombBtn.m_Pos.ToString());
            if (e.Button == MouseButtons.Left)
            {
                m_IsLeftDown = true;
                if (m_bombBtn.m_nState < 3)
                    m_bombBtn.SetState(2);
            }
            if (e.Button == MouseButtons.Right)
            {
                m_IsRightDown = true;
            }
            if (m_IsLeftDown && m_IsRightDown)
            {
                if (m_bombBtn.m_nState <  4)
                    SetRoundBtn(m_bombBtn, 2);
            }

            //MessageBox.Show(btn.m_nBoomAmount.ToString()+','+btn.m_IsBoom.ToString());
        }
        private void OpenMap(BombButton btn)
        {
            if (btn.m_nState != 3)
            {
                m_nRemain--;
                if (btn.m_nBoomAmount == 0)
                {
                    btn.SetState(3);

                    int x = btn.m_Pos.X, y = btn.m_Pos.Y;
                    if ((x - 1) >= 0)//开左边
                    {
                        BombButton tmpBtnLeft = (BombButton)m_arrLattice[(x - 1) * m_nHight + y];
                        OpenMap(tmpBtnLeft);
                        if ((y - 1) >= 0)//左上
                        {
                            BombButton tmpBtnLeftUp = (BombButton)m_arrLattice[(x - 1) * m_nHight + (y - 1)];
                            OpenMap(tmpBtnLeftUp);
                        }
                        if ((y + 1) < m_nHight)//左下
                        {
                            BombButton tmpBtnLeftUp = (BombButton)m_arrLattice[(x - 1) * m_nHight + (y + 1)];
                            OpenMap(tmpBtnLeftUp);
                        }
                    }
                    if ((x + 1) < m_nWidth)//开右边
                    {
                        BombButton tmpBtnRight = (BombButton)m_arrLattice[(x + 1) * m_nHight + y];
                        OpenMap(tmpBtnRight);
                        if ((y - 1) >= 0)//右上
                        {
                            BombButton tmpBtnRightUp = (BombButton)m_arrLattice[(x + 1) * m_nHight + (y - 1)];
                            OpenMap(tmpBtnRightUp);
                        }
                        if ((y + 1) < m_nHight)//右下
                        {
                            BombButton tmpBtnRightUp = (BombButton)m_arrLattice[(x + 1) * m_nHight + (y + 1)];
                            OpenMap(tmpBtnRightUp);
                        }
                    }
                    if ((y - 1) >= 0)//开上边
                    {
                        BombButton tmpBtn = (BombButton)m_arrLattice[x * m_nHight + (y - 1)];
                        OpenMap(tmpBtn);
                    }
                    if ((y + 1) < m_nHight)//开下边
                    {
                        BombButton tmpBtn = (BombButton)m_arrLattice[x * m_nHight + (y + 1)];
                        OpenMap(tmpBtn);
                    }
                }
            }
            btn.SetState(3);
        }

        BombButton m_bombBtn;
        bool m_isDown = true;
        bool m_isGameOver = false;
        private void BombButton_Up(object sender, MouseEventArgs e)
        {
            if (m_isGameOver) return;
            System.Diagnostics.Debug.WriteLine(e.Button.ToString() + ",Up," + m_bombBtn.m_Pos.ToString());
            if (m_IsLeftDown && m_IsRightDown && e.Button == MouseButtons.Right)
            {
                m_isDown = false;
                if (m_bombBtn.m_nState <= 3)
                {
                    SetRoundBtn(m_bombBtn, 1);
                    if(m_bombBtn.m_nState!=3)
                        m_bombBtn.SetState(2);
                }
            }
            if (m_IsLeftDown && !m_IsRightDown && m_isDown)
            {
                if (m_bombBtn.m_nState < 3) { 
                     if (m_bombBtn.m_IsBoom)
                    {
                        MessageBox.Show("Game Over,你踩到雷了!");
                        m_bombBtn.SetState(3);
                        //m_isGameOver = true;
                    }
                    else
                    {
                        //开图算法 碰到数字即结束
                        OpenMap(m_bombBtn);
                        if (m_nRemain == m_nBomb)
                        {
                            MessageBox.Show("赢了!");
                        }
                    }
                    
                }
                //m_bombBtn.SetState(3);
            }
            if(!m_IsLeftDown && m_IsRightDown && e.Button == MouseButtons.Right)//右键单击
            {

                m_bombBtn.SetState(m_bombBtn.m_nState == 4?1:4);
            }
            if (e.Button == MouseButtons.Left)
            {
                m_IsLeftDown = false;
                m_isDown = true;
                if (m_bombBtn.m_nState < 3)
                    m_bombBtn.SetState(1);
            }
            if (e.Button == MouseButtons.Right)
            {
                m_IsRightDown = false;
            }
        }
        private void SetRoundBtn(BombButton btn, int state)
        {
            //m_bombBtn.SetState(2);
            int i = m_bombBtn.m_Pos.X * m_nWidth + m_bombBtn.m_Pos.Y;
            for (int x = m_bombBtn.m_Pos.X - 1; x <= m_bombBtn.m_Pos.X + 1; x++)
            {
                for (int y = m_bombBtn.m_Pos.Y - 1; y <= m_bombBtn.m_Pos.Y + 1; y++)
                {
                    System.Diagnostics.Debug.WriteLine(m_bombBtn.m_Pos.X + "," + m_bombBtn.m_Pos.Y + "--" + x.ToString() + "," + y.ToString());
                    if (x < 0 || y < 0) continue;
                    if (x > (m_nWidth - 1) || y > (m_nHight - 1)) continue;
                    int sub = ((x * m_nHight) + y);
                    BombButton tmpBtnAmount = (BombButton)m_arrLattice[sub];
                    if (tmpBtnAmount.m_nState <= 2)
                        tmpBtnAmount.SetState(state);
                }
            }
        }
    }
}
