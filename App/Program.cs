using Research.Enums;
using Research.Models;
using Research.Collections;

var microsftResearch = new ResearchTeam(
    "Microsoft Research",
    "Microsoft",
    1,
    TimeFrame.Year
);

var microsftResearchCopy = (ResearchTeam)microsftResearch.DeepCopy();

Console.WriteLine(microsftResearch);

microsftResearch.Organization = "Microsoft Research";

Console.WriteLine(microsftResearchCopy);