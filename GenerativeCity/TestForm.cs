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
        int widthOfStruct = 5;
        int heightOfStruct = 5;
        int widthOfCity = 128;
        int heightOfCity = 128;
        City city;
        string initialFormTitle;

        int timerCount = 0;
        SolidBrush purpleBrush = new SolidBrush(Color.Purple);
        SolidBrush brownBrush = new SolidBrush(Color.Brown);
        SolidBrush greenBrush = new SolidBrush(Color.Green);
        SolidBrush yellowBrush = new SolidBrush(Color.Yellow);



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
            CityStructure structureType;
            for (int i = 0; i < cityMap.XSize; i++)
            {
                for (int j = 0; j < cityMap.YSize; j++)
                {
                    structureType = cityMap[i, j];
                    if (structureType.GetType() == typeof(Empty))
                    {
                        graphics.FillRectangle(greenBrush, i*widthOfStruct, j*heightOfStruct, widthOfStruct, heightOfStruct);
                    }
                    if (structureType.GetType() == typeof(House))
                    {
                        graphics.FillRectangle(brownBrush, i*widthOfStruct, j*heightOfStruct, widthOfStruct, heightOfStruct);
                    }
                    if (structureType.GetType() == typeof(CommercialBuilding))
                    {
                        graphics.FillRectangle(purpleBrush, i*widthOfStruct, j*heightOfStruct, widthOfStruct, heightOfStruct);
                    }
                    if (structureType.GetType() == typeof(HighRise))
                    {
                        graphics.FillRectangle(yellowBrush, i * widthOfStruct, j * heightOfStruct, widthOfStruct, heightOfStruct);
                    }
                }
            }
        }
    }
}
