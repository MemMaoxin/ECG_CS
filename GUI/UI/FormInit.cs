using System;
using System.Windows.Forms;
using System.IO.Ports;
using System.Timers;
using System.Text;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.IO;
//using System.Net.Http;
using System.Linq;

namespace GUI.UI
{
    public partial class FormInit : Form
    {
        private static FormInit formInstance;
        public static FormInit GetIntance
        {
            get
            {
 /*                               if (formInstance != null)
                                {
                                    return formInstance;
                                }
                                else
                                {
                                    formInstance = new Form1();
                                    return formInstance;
                                }*/
                formInstance = new FormInit();
                return formInstance;
            }
        }

        public FormInit()
        {
            InitializeComponent();
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

    }
}
