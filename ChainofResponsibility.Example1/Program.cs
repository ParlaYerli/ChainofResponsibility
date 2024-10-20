using System;

namespace ChainofResponsibility.Example1
{
    // Abstract Handler
    public abstract class ComplaintHandler
    {
        protected ComplaintHandler nextHandler;

        public void SetNextHandler(ComplaintHandler nextHandler)
        {
            this.nextHandler = nextHandler;
        }

        public abstract void HandleRequest(string complaint);
    }

    // Concrete Handler 1: Support Team
    public class SupportTeamHandler : ComplaintHandler
    {
        public override void HandleRequest(string complaint)
        {
            if (complaint == "Basit sorun")
            {
                Console.WriteLine("Support: Sorunu çözdüm.");
            }
            else if (nextHandler != null)
            {
                nextHandler.HandleRequest(complaint);
            }
        }
    }

    // Concrete Handler 2: Analysis Team
    public class AnalysisTeamHandler : ComplaintHandler
    {
        public override void HandleRequest(string complaint)
        {
            if (complaint == "Orta düzey sorun")
            {
                Console.WriteLine("AnalysisTeam: Sorunu çözdüm.");
            }
            else if (nextHandler != null)
            {
                nextHandler.HandleRequest(complaint);
            }
        }
    }

    // Concrete Handler 3: Developer Team
    public class DeveloperTeamHandler : ComplaintHandler
    {
        public override void HandleRequest(string complaint)
        {
            if (complaint == "Ciddi sorun")
            {
                Console.WriteLine("Developer: Sorunu çözdüm.");
            }
            else
            {
                Console.WriteLine("Developer: Sorunu çözemedik.");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var support = new SupportTeamHandler();
            var analysis = new AnalysisTeamHandler();
            var developer = new DeveloperTeamHandler();

            support.SetNextHandler(analysis);
            analysis.SetNextHandler(developer);

            string complaint = "Orta düzey sorun";
            support.HandleRequest(complaint);

            //string seriousComplaint = "Ciddi sorun";
            //support.HandleRequest(seriousComplaint);
        }
    }
}
