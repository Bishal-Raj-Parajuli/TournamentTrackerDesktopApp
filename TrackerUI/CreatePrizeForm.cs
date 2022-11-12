using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        public CreatePrizeForm()
        {
            InitializeComponent();
        }

        private void createPrizeBtn_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                PrizeModel model = new PrizeModel(
                    placeNameTextBox.Text,
                    placeNumberTextBox.Text,
                    prizeAmountTextBox.Text,
                    prizePercentageTextBox.Text);

                foreach(IDataConnection db in GlobalConfig.Connections)
                {
                    db.CreatePrize(model);
                }
                placeNameTextBox.Text = "";
                placeNumberTextBox.Text = "";
                prizeAmountTextBox.Text = "0";
                prizePercentageTextBox.Text = "0";
            }
            else
            {
                MessageBox.Show("This form data is Invlaid !!");
            }
        }

        private bool ValidateForm()
        {
            bool output = true;
            int placeNumber = 0;
            bool placeNumberValidNumber = int.TryParse(placeNumberTextBox.Text, out placeNumber);

            if (!placeNumberValidNumber)
            {
                output = false;
            }
            if(placeNumber < 1)
            {
                output = false;
            }
            if(placeNameTextBox.Text.Length == 0)
            {
                output = false;
            }

            decimal prizeAmount = 0;
            int prizePercentage = 0;

            bool prizeAmountValid = decimal.TryParse(prizeAmountTextBox.Text, out prizeAmount);
            bool prizePercentageValid = int.TryParse(prizePercentageTextBox.Text, out prizePercentage);

            if (!prizeAmountValid || !prizePercentageValid)
            {
                output = false;

            }
            if (prizeAmount <=0 && prizePercentage <= 0)
            {
                output = false;
            }
            if(prizePercentage < 0 || prizePercentage > 100)
            {
                output = false;
            }

            return output;
        }
    }
}
