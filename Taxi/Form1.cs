using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Taxi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            DateTime startDateTime = this.dateTimePicker1.Value;
            DateTime endDateTime = this.dateTimePicker2.Value;
            TimeSpan travelTime = endDateTime - startDateTime;
            DateTime discountStartDateStartTime = startDateTime.Date.AddHours(8);
            DateTime discountStartDateEndTime = startDateTime.Date.AddHours(18);
            DateTime discountEndDateStartTime = endDateTime.Date.AddHours(8);
            DateTime discountEndDateEndTime = endDateTime.Date.AddHours(18);
            TimeSpan dateDifference = endDateTime.Date - startDateTime.Date;
            decimal KmTravelled = this.numericUpDown1.Value;

            Console.WriteLine(discountStartDateStartTime);
            Console.WriteLine(discountStartDateEndTime);
            Console.WriteLine(discountEndDateStartTime);
            Console.WriteLine(discountEndDateEndTime);

            // display distance travelled
            DataGridViewRow KmTravelledRow = (DataGridViewRow)this.dataGridView1.Rows[0].Clone();
            KmTravelledRow.Cells[0].Value = "Distance travelled (km)";
            KmTravelledRow.Cells[1].Value = KmTravelled;
            this.dataGridView1.Rows.Add(KmTravelledRow);

            // display average speed
            if (travelTime.TotalHours > 0)
            {
                DataGridViewRow averageSpeedRow = (DataGridViewRow)this.dataGridView1.Rows[0].Clone();
                averageSpeedRow.Cells[0].Value = "Avg. speed (km/h)";
                averageSpeedRow.Cells[1].Value = KmTravelled/ Convert.ToDecimal(travelTime.TotalHours);
                this.dataGridView1.Rows.Add(averageSpeedRow);
            }

            // if startDateTime and endDateTime are the on the same day.
            if (dateDifference.TotalDays == 0)
            {
                DateTime discountStartTime;
                if (startDateTime < discountStartDateStartTime) {
                    discountStartTime = discountStartDateStartTime;
                }
                else
                {
                    discountStartTime = startDateTime;
                }

                DateTime discountEndTime;
                if (endDateTime < discountStartDateEndTime) {
                    discountEndTime = endDateTime;
                }
                else
                {
                    discountEndTime = discountStartDateEndTime;
                }
                TimeSpan intersection = discountEndTime - discountStartTime;
                double discountMinutes = Math.Round(intersection.TotalMinutes);
                double normalMinutes = Math.Round((travelTime - intersection).TotalMinutes);

                DataGridViewRow DiscountTimeRow = (DataGridViewRow)this.dataGridView1.Rows[0].Clone();
                DiscountTimeRow.Cells[0].Value = "Discount time (minutes)";
                DiscountTimeRow.Cells[1].Value = discountMinutes;
                this.dataGridView1.Rows.Add(DiscountTimeRow);

                DataGridViewRow NormalTimeRow = (DataGridViewRow)this.dataGridView1.Rows[0].Clone();
                NormalTimeRow.Cells[0].Value = "Normal time (minutes)";
                NormalTimeRow.Cells[1].Value = normalMinutes;
                this.dataGridView1.Rows.Add(NormalTimeRow);

                DataGridViewRow DiscountPriceRow = (DataGridViewRow)this.dataGridView1.Rows[0].Clone();
                DiscountPriceRow.Cells[0].Value = "Discount time price (EUR)";
                DiscountPriceRow.Cells[1].Value = discountMinutes * 0.25;
                this.dataGridView1.Rows.Add(DiscountPriceRow);

                DataGridViewRow NormalPriceRow = (DataGridViewRow)this.dataGridView1.Rows[0].Clone();
                NormalPriceRow.Cells[0].Value = "Normal time price (EUR)";
                NormalPriceRow.Cells[1].Value = normalMinutes * 0.45;
                this.dataGridView1.Rows.Add(NormalPriceRow);

                double totalPrice = (double)KmTravelled + discountMinutes * 0.25 + normalMinutes * 0.45;
                bool startedOnWeekend = false;
                Console.WriteLine(startDateTime.Hour);
                if (
                    startDateTime.DayOfWeek == DayOfWeek.Friday && startDateTime.Hour >= 22 ||
                    startDateTime.DayOfWeek == DayOfWeek.Saturday ||
                    startDateTime.DayOfWeek == DayOfWeek.Sunday ||
                    startDateTime.DayOfWeek == DayOfWeek.Monday && startDateTime.Hour <= 7
                )
                {
                    totalPrice = totalPrice * 1.15;
                    startedOnWeekend = true;
                }

                DataGridViewRow WeekendRow = (DataGridViewRow)this.dataGridView1.Rows[0].Clone();
                WeekendRow.Cells[0].Value = "Started on weekend?";
                WeekendRow.Cells[1].Value = startedOnWeekend;
                this.dataGridView1.Rows.Add(WeekendRow);

                DataGridViewRow TotalPriceRow = (DataGridViewRow)this.dataGridView1.Rows[0].Clone();
                TotalPriceRow.Cells[0].Value = "Total time price (EUR)";
                TotalPriceRow.Cells[1].Value = totalPrice;
                this.dataGridView1.Rows.Add(TotalPriceRow);
            }
            
            /*if (endDateTime.Hour > 8 && endDateTime.Hour < 18)
            {
                Console.WriteLine(endDateTime.TimeOfDay);
            }*/
        }
    }
}
