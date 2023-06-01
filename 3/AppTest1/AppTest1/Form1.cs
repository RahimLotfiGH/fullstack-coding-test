using AppTest1.Helpers;
using AppTest1.Nodes;
using AppTest1.Utlities;

namespace AppTest1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var treeArray = SampleFeedHelper.GetNodes();

            var root = treeArray.ConvertToTree();

            richTextBox1.Text = root.ObjectSerialize();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            var root = SampleFeedHelper.CreateTree();

            var nodes = root.ConvertNodeFromTree();

            richTextBox1.Text = nodes.ObjectSerialize();

        }










    }
}