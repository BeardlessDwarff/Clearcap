using System;

namespace PdfGenerator
{
    public class Financials
    {
        public int BesteedbaarInkomenPerMaand { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Income Income { get; set; }
        public Expenses Expenses { get; set; }
        public MaandelijksTotaal MaandelijksTotaal { get; set; }
    }
}
