using BenNHControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace GUI.UI
{
    public partial class MainForm : FormEX
    {

        private static MainForm formInstance;
        public static MainForm GetIntance
        {
            get
            {
                if (formInstance != null)
                {
                    return formInstance;
                }
                else
                {
                    formInstance = new MainForm();
                    return formInstance;
                }
            }
        }


        public object lockObj = new object();
        public bool formSwitchFlag = false;
        public static int trans_choice = 1;
        public int physio_choice = 0;

        /// <summary>
        /// 子窗体界面单例元素
        /// </summary>
        public static FormInit formInit = null;
        public static Form1 form1 = null;


        /// <summary>
        /// 当前显示窗体
        /// </summary>
        private System.Windows.Forms.Form currentForm;


        /// <summary>
        /// 构造方法
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            //主窗体容器打开
            this.IsMdiContainer = true;

            this.TitleText = "生理信号检测";

            //实例化子窗体界面
            formInit = FormInit.GetIntance;
            form1 = Form1.GetIntance;

            //初始化按钮
            this.initButton();

        }


        /// <summary>
        /// 解决闪烁问题
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private bool initButton()
        {
            try
            {
                this.button1.BackColor = Color.White;

            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="frm"></param>
        public void ShowForm(System.Windows.Forms.Panel panel, System.Windows.Forms.Form frm)
        {
            lock (this)
            {
                try
                {
                    if (this.currentForm != null && this.currentForm == frm)
                    {
                        return;
                    }
                    else if (this.currentForm != null)
                    {
                        if (this.ActiveMdiChild != null)
                        {
                            this.ActiveMdiChild.Hide();
                        }
                    }
                    this.currentForm = frm;
                    frm.TopLevel = false;
                    frm.MdiParent = this;
                    panel.Controls.Clear();
                    panel.Controls.Add(frm);
                    frm.Show();
                    frm.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.Refresh();
                    foreach (Control item in frm.Controls)
                    {
                        item.Focus();
                        break;
                    }
                }
                catch (System.Exception ex)
                {
                    //
                }
            }
        }


        private void Trans_Change(object sender, EventArgs e)
        {
            switch (physio_choice) {
                case 0:
                    return;
                case 1:
                    button1_Click(sender, e);
                    return;

            }
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            physio_choice = 1;
            try
            {
                this.initButton();
                this.button1.BackColor = Color.FromArgb(255,250,205);
                this.label1.Text = "Electrocardiograph";
                Monitor.Enter(this.lockObj);
                if (!formSwitchFlag)
                {
                    formSwitchFlag = true;
                    
                    form1 = Form1.GetIntance;
                    this.ShowForm(pnlCenter,form1);
                    formSwitchFlag = false;
                }
                else
                {
                    return;
                }
            }
            catch (System.Exception ex)
            {
                //
            }
            finally
            {
                Monitor.Exit(this.lockObj);
            }
        }
        

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            physio_choice = 0;
            try
            {
                this.initButton();
                this.label1.Text = "";
                Monitor.Enter(this.lockObj);
                if (!formSwitchFlag)
                {
                    formSwitchFlag = true;
                    this.ShowForm(pnlCenter, formInit);
                    formSwitchFlag = false;
                }
                else
                {
                    return;
                }
            }
            catch (System.Exception ex)
            {
                //
            }
            finally
            {
                Monitor.Exit(this.lockObj);
            }
        }
    }
}
