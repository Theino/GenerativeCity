using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenerativeCity
{
    public partial class formTest : Form
    {
        Graphics graphics;
        int widthOfStruct = 2;
        int heightOfStruct = 2;
        int widthOfCity = 128;
        int heightOfCity = 128;
        City city;
        string initialFormTitle;

        int timerCount = 0;
        

        public formTest()
        {
            InitializeComponent();
            graphics = panelDrawing.CreateGraphics();
            city = new City(widthOfCity, heightOfCity);
            initialFormTitle = this.Text;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            city.Step();
            drawMap(city.cityMap);
            timerCount++;
            this.Text = initialFormTitle + " " + timerCount.ToString();
        }

        private void drawMap(CityMap cityMap)
        {
            for (int i = 0; i < cityMap.XSize; i++)
            {
                for (int j = 0; j < cityMap.YSize; j++)
                {
                    CityStructure structureType = cityMap[i, j];
                    if (structureType.GetType() == typeof(Empty))
                    {
                        graphics.FillRectangle(new SolidBrush(Color.Green), i*widthOfStruct, j*heightOfStruct, widthOfStruct, heightOfStruct);
                    }
                    if (structureType.GetType() == typeof(House))
                    {
                        graphics.FillRectangle(new SolidBrush(Color.Brown), i*widthOfStruct, j*heightOfStruct, widthOfStruct, heightOfStruct);
                    }
                    if (structureType.GetType() == typeof(CommercialBuilding))
                    {
                        graphics.FillRectangle(new SolidBrush(Color.Purple), i*widthOfStruct, j*heightOfStruct, widthOfStruct, heightOfStruct);
                    }
                }
            }
        }
    }
}
