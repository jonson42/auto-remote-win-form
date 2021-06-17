using AutoSeleniumFromExcel.Utils;
using Newtonsoft.Json;
using selenium_remote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSeleniumFromExcel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
                InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int width = Int32.Parse(txtWidth.Text);
            int height = Int32.Parse(txtHeight.Text);
            int numberThread = Int32.Parse(txtThreadNbr.Text);
            List<Thread> lstThreads = new List<Thread>();
            
            Excell excel = new Excell("Config.xlsx");
            var dict = excel.ReadFile("DataAuto");
            var dataForRun = excel.ReadFile("DataRun");
            var listSearch = GetListFirstColumn(excel.ReadFile("KeySearch"));
            var listData = new List<List<List<string>>>();
            if (numberThread != 1)
            {
                listData = SplitList.SplitListProcess(dataForRun, numberThread);
                SplitList.AddHeaderForList(listData);
            }
            else
            {
                listData.Add(dataForRun);
            }
            for(int i=0;i<Int32.Parse(txtLoop.Text);i++){
                int threadNum = 0;
                foreach (var item in listData)
                {
                    Thread th = new Thread(() => { MainFunction(item, dict, listSearch, threadNum, width, height); });
                    th.Name = "Thread" + threadNum.ToString();
                    lstThreads.Add(th);
                    threadNum++;
                }
                foreach (Thread th in lstThreads)
                    th.Start();
                //MessageBox.Show("Run successfully !");
            }

        }

        private List<string> GetListFirstColumn(List<List<string>> list)
        {
            var result = new List<string>();
            foreach(var item in list)
            {
                result.Add(item[0]);
            }
            return result;
        }
        private void MainFunction(List<List<string>> dataForRun,List<List<string>> dict,List<string> listSearch,int threadNum,int width,int height)
        {
            int x = 0;
            int y = 0;
            //if (threadNum == 1)
            //{
            //    x = 500;
            //    y = 500;
            //}
            //if (threadNum == 2)
            //{
            //    x = 500;
            //    y = 0;
            //}
            //if (threadNum == 3)
            //{
            //    x = 0;
            //    y = 500;
            //}
            var dictValue = FirstBuildDictionaryValue(dataForRun);
            for (int i = 1; i < dataForRun.Count; i++)
            {
                
                dictValue = ChangeDictionaryBuild(dataForRun, dictValue, i);
                var proxy = "";
                dictValue.TryGetValue("Proxy", out proxy);
                Robot robot = new Robot("",x,y,width,height,proxy);
                if(robot.StartWithDictionNary(dict, dictValue, listSearch)){
                    var timeWait = ConvertMinutesToMilliseconds(Int32.Parse(txtTimeWait.Text));
                    Thread.Sleep(timeWait);
                    robot.CloseRobot();
                }
                
            }
        }

        private Dictionary<string, string> FirstBuildDictionaryValue(List<List<string>> input)
        {
            var result = new Dictionary<string, string>();
            foreach (var item in input[0])
            {
                if (item != null)
                {
                    result.Add(item.Trim(), "");
                }
            }
            return result;
        }
        private Dictionary<string, string> ChangeDictionaryBuild(List<List<string>> input,Dictionary<string,string> dictData,int lineForcus)
        {
            var column = 0;
            foreach(var item in input[lineForcus])
            {
                if (item != null)
                {
                    dictData[input[0][column]] = item;
                }
                
                column++;
            }
            return dictData;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbOption.Text = "Album";
            txtLoop.Text = 1.ToString();
            txtThreadNbr.Text = 1.ToString();
            txtTimeWait.Text = 5.ToString();
            txtHeight.Text = 700.ToString();
            txtWidth.Text = 700.ToString();
        }

        private void txtTimeWait_TextChanged(object sender, EventArgs e)
        {

        }
        public static int ConvertMinutesToMilliseconds(int minutes)
        {
            return (int)TimeSpan.FromMinutes(minutes).TotalMilliseconds;
        }

        private void txtThreadNbr_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
