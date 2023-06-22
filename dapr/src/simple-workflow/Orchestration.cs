using Dapr.Workflow;
using simple_model;

namespace simple_workflow
{
    public class Orchestration : Workflow<ParkingFacility, string>
    {
        public async override Task<string> RunAsync(WorkflowContext context, ParkingFacility input)
        {
            var declare = await context.CallActivityAsync<string>(
                nameof(DeclareToAuthorityActivity),
                input);

            var archive = await context.CallActivityAsync<string>(
                nameof(ArchiveActivity),
                input);

            List<string> wfOutput = new List<string>
            { declare, archive };

            return string.Join(" /// ", wfOutput);
        }

    }
}
