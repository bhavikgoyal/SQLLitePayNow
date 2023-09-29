using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace Demo_projects.Models
{
    public class Billing
    {
        [Key]
        public int BillNo { get; set; }
        public int CustomertNo { get; set;}
        public DateTime Date { get; set;}
        public string Amount { get; set;}
        public bool IsPaid { get; set;}
    }
}
