/* namespace OpenBrisk.RuntimeControllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("/report")]
    public class ReportController : Controller
    {
        private readonly IReportBuilder reportBuilder;

        public ReportController(IReportBuilder reportBuilder)
        {
            this.reportBuilder = reportBuilder;
        }

        [HttpGet]
        public string FunctionReport()
        {
            return this.reportBuilder.GetReport();
        }
    }
} */